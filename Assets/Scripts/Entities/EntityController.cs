﻿using System.Linq;
using Assets.Scripts;
using Core.Timer;
using UnityEngine;
using Utils;
using static UnityEngine.EventSystems.EventTrigger;

namespace Entities
{
    public class EntityController : MonoBehaviour
    {
        [field: SerializeField]
        public float SpawnCooldown { get; private set; }

        [SerializeField]
        private GameObject basicMouse;
        [SerializeField]
        private GameObject fastMouse;
        [SerializeField]
        private GameObject zigzagMouse;
        [SerializeField]
        private GameObject fastZigzagMouse;
        private GameObject[] _entityPrefabs;

        [SerializeField]
        private SpriteDrawer crossDrawer;

        private float[] _entitySpawnChanceProportions;

        public float GetEntitySpawnChanceProportion(EntityType type) => 
            _entitySpawnChanceProportions[(int)type];

        public void SetEntitySpawnChanceProportion(EntityType type, float proportion) => 
            _entitySpawnChanceProportions[(int)type] = proportion;


        public float HalfFieldWidth => Camera.main.ViewportToWorldPoint(new(1, 0)).x - transform.position.x;
        public float HalfFieldHeight => Camera.main.ViewportToWorldPoint(new(0, 1)).y - transform.position.y;

        private float _elapsedTime;

        private void Awake()
        {
            Debug.Log("Info:");
            Debug.Log("Screen width: " + Screen.width);
            Debug.Log("World pos x:" + Camera.main.ViewportToWorldPoint(new(1, 0)));

            _entityPrefabs = new GameObject[4] { basicMouse, fastMouse, zigzagMouse, fastZigzagMouse };
            proportionValues = new float[4];
            _entitySpawnChanceProportions = new float[4] { 10, 2, 2, 1 };
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

            var entity = Instantiate(prefab, pos, transform.rotation, transform).GetComponent<Entity>();

            entity.AngleDeg = MathUtils.DirectionToAngle(transform.position - entity.transform.position) + 
                Mathf.Sign(Random.value - 0.5f) * (10 + Random.Range(0, 35));
        }

        private float[] proportionValues;
        private GameObject GetRndEntityFair()
        {
            if (proportionValues.All(value => value <= 0))
                for (int i = 0; i < proportionValues.Length; i++)
                    proportionValues[i] += _entitySpawnChanceProportions[i];

            var stop = Random.Range(0, proportionValues.Sum());
            int entityIndex = -1;
            float pointer = 0;
            do
            {
                entityIndex++;
                pointer += proportionValues[entityIndex];
            } while (stop > pointer);

            proportionValues[entityIndex]--;

            return _entityPrefabs[entityIndex];
        }

        private GameObject GetRndEntity()
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

        public void SpawnHorde(int mouseCount)
        {
            for (int i = 0; i < mouseCount; i++)
            {
                var prefab = GetRndEntity();
                var dir = Mathf.Sign(Random.value - 0.5f);
                var pos = new Vector2(dir * (HalfFieldWidth + Random.Range(0, 4f)), Random.Range(-HalfFieldHeight, HalfFieldHeight));
                var entity = Instantiate(prefab, pos, new Quaternion(), transform).GetComponent<Entity>();
                entity.AngleDeg = (dir + 1) / 2 * 180 + Random.Range(-10, 10);
            }
        }
    }
}
