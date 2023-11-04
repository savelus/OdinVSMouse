using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EntityController : MonoBehaviour
    {
        [field: SerializeField]
        public float SpawnCooldown { get; private set; }

        [SerializeField]
        private Transform field;

        [SerializeField]
        private GameObject basicMouse;
        [SerializeField]
        private GameObject fastMouse;
        [SerializeField]
        private GameObject zigzagMouse;
        [SerializeField]
        private GameObject fastZigzagMouse;
        private GameObject[] entityPrefabs;

        private float[] entitySpawnChanceProportions;
        public float GetEntitySpawnChanceProportion(EntityType type) => 
            entitySpawnChanceProportions[(int)type];
        public void SetEntitySpawnChanceProportion(EntityType type, float proportion) => 
            entitySpawnChanceProportions[(int)type] = proportion;

        public float HalfFieldWidth => field.localScale.x / 2;
        public float HalfFieldHeight => field.localScale.y / 2;

        private float elapsedTime;

        private void Awake()
        {
            //подписаться на таймер += End;
            entityPrefabs = new GameObject[4] { basicMouse, fastMouse, zigzagMouse, fastMouse };
            entitySpawnChanceProportions = new float[4] { 10, 2, 2, 1 };
        }

        private void Update()
        {
            if (isGamePlaying)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > SpawnCooldown) 
                {
                    elapsedTime = 0;
                    SpawnEntity();
                }
            }
        }

        private void SpawnEntity()
        {
            var pos = Random.value > 0.5f ?
                new Vector3(Random.Range(-HalfFieldWidth, HalfFieldWidth), Mathf.Sign(Random.value - 0.5f) * HalfFieldHeight) :
                new Vector3(Mathf.Sign(Random.value - 0.5f) * HalfFieldWidth, Random.Range(-HalfFieldHeight, HalfFieldHeight));
            pos += transform.position;

            var prefab = GetRndEntity();

            var entity = Instantiate(prefab, pos, transform.rotation, transform).GetComponent<Entity>();

            entity.AngleDeg = MathUtils.DirectionToAngle(transform.position - entity.transform.position) + 
                Mathf.Sign(Random.value - 0.5f) * (10 + Random.Range(0, 35));

            GameObject GetRndEntity()
            {
                var stop = Random.Range(0, entitySpawnChanceProportions.Sum());
                int i = 0;
                float pointer = 0;
                do
                {
                    pointer += entitySpawnChanceProportions[i];
                    i++;
                } while (stop > pointer);

                return entityPrefabs[i - 1];
            }
        }

        private bool isGamePlaying = true;
        private void End()
        {
            isGamePlaying = false;
        }
    }
}
