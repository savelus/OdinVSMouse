using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Utils;

namespace Core.Boosts
{
    public class BoostManager : MonoBehaviour
    {
        [SerializeField] private int hordeSize = 60;

        [SerializeField] private EntityController entityController;

        [SerializeField] private GameObject explosionPrefab;

        private Action[] _boostActions;
        private int _currentBoost;
        private readonly List<Mouse> _mousesToDie = new();

        private Dictionary<BoostType, Action> _boostWithActions = new();

        private Dictionary<BoostType, GameObject> _imagesOnBoosts;

        private void Awake()
        {
            _boostWithActions = new Dictionary<BoostType, Action>()
            {
                { BoostType.Eagle, ActivateEagles },
                { BoostType.Owl, ActivateOwls },
                { BoostType.Horde, ActivateMouseHorde },
                { BoostType.Die, ActivateAllMouseDie }
            };
        }

        private void Update()
        {
            for (var i = 0; i < Math.Min(2, _mousesToDie.Count); i++)
            {
                var mouseToDie = _mousesToDie[^1];
                _mousesToDie.RemoveAt(_mousesToDie.Count - 1);

                if (mouseToDie.IsDestroyed) continue;
                Instantiate(explosionPrefab, mouseToDie.transform.position, new Quaternion());
                mouseToDie.Die();
            }

#if UNITY_EDITOR //cheets
            if (Input.GetKeyUp(KeyCode.Alpha1))
                ActivateEagles();
            if (Input.GetKeyUp(KeyCode.Alpha2))
                ActivateOwls();
            if (Input.GetKeyUp(KeyCode.Alpha3))
                ActivateMouseHorde();
            if (Input.GetKeyUp(KeyCode.Alpha4))
                ActivateAllMouseDie();
#endif
        }

        public void ActivateBoost(BoostType type) => _boostWithActions[type].Invoke();

        private void ActivateMouseHorde() => entityController.SpawnHorde(hordeSize);


        private void ActivateAllMouseDie()
        {
            entityController.transform.ForEachChield(chield =>
            {
                var mouse = chield.GetComponent<Mouse>();
                if (mouse != null) _mousesToDie.Add(mouse);
            });
        }

        private void ActivateEagles() => entityController.StawnEgles(2);

        private void ActivateOwls() => entityController.StawnOwls(1);
    }

    
}