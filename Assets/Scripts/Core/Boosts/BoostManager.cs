using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Core.Boosts
{
    public class BoostManager : MonoBehaviour
    {
        [SerializeField]
        private int hordeSize = 60;

        [SerializeField]
        private EntityController entityController;

        [SerializeField]
        private GameObject explosionPrefab;

        private Action[] boostActions;
        private int currentBoost;

        private void Awake()
        {
            boostActions = new Action[]
            {
                ActivateEagles,
                ActivateOwls,
                ActivateMouseHorde,
                ActivateAllMouseDie,
            };
        }

        private void Update()
        {
            for (int i = 0; i < Math.Min(2, mousesToDie.Count); i++)
            {
                var mouseToDie = mousesToDie[^1];
                mousesToDie.RemoveAt(mousesToDie.Count - 1);

                if (!mouseToDie.IsDestroyed)
                {
                    Instantiate(explosionPrefab, mouseToDie.transform.position, new Quaternion());
                    mouseToDie.Die();
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
                ActivateEagles();
            if (Input.GetKeyUp(KeyCode.Alpha2))
                ActivateOwls();
            if (Input.GetKeyUp(KeyCode.Alpha3))
                ActivateMouseHorde();
            if (Input.GetKeyUp(KeyCode.Alpha4))
                ActivateAllMouseDie();
            if (Input.GetKeyUp(KeyCode.Alpha0))
                GameManager.Singleton.Timer.RemainingTime += 10;
        }

        public void ActivateNextBoost()
        {
            if (boostActions.Length > currentBoost)
            {
                boostActions[currentBoost].Invoke();
                currentBoost++;
            }
        }

        public void ActivateMouseHorde()
        {
            entityController.SpawnHorde(hordeSize);
        }

        List<Mouse> mousesToDie = new();
        public void ActivateAllMouseDie()
        {
            entityController.transform.ForEachChield(chield => {
                var mouse = chield.GetComponent<Mouse>();
                if (mouse != null)
                {
                    mousesToDie.Add(mouse);
                }
            });
        }

        public void ActivateEagles()
        {
            entityController.StawnEgles(3);
        }

        public void ActivateOwls()
        {
            entityController.StawnOwls(3);
        }
    }
}