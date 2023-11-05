using System.Linq;
using Core.Timer;
using UnityEngine;
using Utils;

namespace Entities
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

        [SerializeField] 
        private Timer timer;
        private GameObject[] _entityPrefabs;

        private float[] _entitySpawnChanceProportions;

        public float GetEntitySpawnChanceProportion(EntityType type) => 
            _entitySpawnChanceProportions[(int)type];

        public void SetEntitySpawnChanceProportion(EntityType type, float proportion) => 
            _entitySpawnChanceProportions[(int)type] = proportion;


        public float HalfFieldWidth => field.localScale.x / 2;
        public float HalfFieldHeight => field.localScale.y / 2;

        private float _elapsedTime;
        private bool _isGamePlaying = true;


        private void Awake()
        {
            timer.SubscribeOnTimerEnd(_ => End());
            _entityPrefabs = new GameObject[4] { basicMouse, fastMouse, zigzagMouse, fastZigzagMouse };
            _entitySpawnChanceProportions = new float[4] { 10, 2, 2, 1 };
        }

        private void Update()
        {
            if (!_isGamePlaying) return;
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime <= SpawnCooldown) return;
            _elapsedTime = 0;
            SpawnEntity();
        }

        public void StartGame() => 
            _isGamePlaying = true;

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
                var stop = Random.Range(0, _entitySpawnChanceProportions.Sum());
                int i = 0;
                float pointer = 0;
                do
                {
                    pointer += _entitySpawnChanceProportions[i];
                    i++;
                } while (stop > pointer);

                return _entityPrefabs[i - 1];
            }
        }

        private void End()
        {
            _isGamePlaying = false;
        }
    }
}
