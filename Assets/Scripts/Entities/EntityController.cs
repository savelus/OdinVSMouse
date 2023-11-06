using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

namespace Entities
{
    public class EntityController : MonoBehaviour
    {
        [field: SerializeField]
        public float SpawnCooldown { get; private set; }

        [FormerlySerializedAs("_entities")] [SerializeField] 
        private MouseConfiguration[] _mouseConfigurations;
        
        [SerializeField]
        private SpriteDrawer crossDrawer;

        public float HalfFieldWidth => Camera.main.ViewportToWorldPoint(new(1, 0)).x - transform.position.x;
        public float HalfFieldHeight => Camera.main.ViewportToWorldPoint(new(0, 1)).y - transform.position.y;

        private float _elapsedTime;

        private void Awake()
        {
            Debug.Log("Info:");
            Debug.Log("Screen width: " + Screen.width);
            Debug.Log("World pos x:" + Camera.main.ViewportToWorldPoint(new(1, 0)));
            proportionValues = new float[4];
            Entity.SpeedModifier = 0.7f;
        }

        private void Update()
        {
            //if (!GameManager.IsGameStarted) return;

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime <= SpawnCooldown / Entity.SpeedModifier) return;
            _elapsedTime = 0;

            SpawnEntity();
        }

        private void SpawnEntity()
        {
            var pos = Random.value > 0.5f ?
                new Vector3(Random.Range(-HalfFieldWidth, HalfFieldWidth), Mathf.Sign(Random.value - 0.5f) * HalfFieldHeight) :
                new Vector3(Mathf.Sign(Random.value - 0.5f) * HalfFieldWidth, Random.Range(-HalfFieldHeight, HalfFieldHeight));
            pos += transform.position;

            var prefab = GetRndEntityFair();

            var entity = Instantiate(prefab, pos, transform.rotation, transform);

            entity.AngleDeg = MathUtils.DirectionToAngle(transform.position - entity.transform.position) + 
                Mathf.Sign(Random.value - 0.5f) * (10 + Random.Range(0, 35));
        }

        private float[] proportionValues;
        private Entity GetRndEntityFair()
        {
            if (proportionValues.All(value => value <= 0))
                for (int i = 0; i < proportionValues.Length; i++)
                    proportionValues[i] += _mouseConfigurations[i].Chance;

            var stop = Random.Range(0, proportionValues.Sum());
            int entityIndex = -1;
            float pointer = 0;
            do
            {
                entityIndex++;
                pointer += proportionValues[entityIndex];
            } while (stop > pointer);

            proportionValues[entityIndex]--;

            return _mouseConfigurations[entityIndex].Mouse;
        }

        private Entity GetRndEntity()
        {
            var stop = Random.Range(0, _mouseConfigurations.Sum(x=>x.Chance));
            int i = 0;
            float pointer = 0;
            do
            {
                pointer +=  _mouseConfigurations[i].Chance;
                i++;
            } while (stop > pointer);

            return _mouseConfigurations[i - 1].Mouse;
        }

        public void SpawnHorde(int mouseCount)
        {
            for (int i = 0; i < mouseCount; i++)
            {
                var prefab = GetRndEntity();
                var dir = Mathf.Sign(Random.value - 0.5f);
                var pos = new Vector2(dir * (HalfFieldWidth + Random.Range(0, 4f)), Random.Range(-HalfFieldHeight, HalfFieldHeight));
                var entity = Instantiate(prefab, pos, new Quaternion(), transform);
                entity.AngleDeg = (dir + 1) / 2 * 180 + Random.Range(-10, 10);
            }
        }
    }

    [Serializable]
    public class MouseConfiguration
    {
        public EntityType TypeMouse;
        public Entity Mouse;
        public float Chance;
        
    }
}
