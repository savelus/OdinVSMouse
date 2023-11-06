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
                ActivateMouseHorde,
                ActivateAllMouseDie,
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
                ActivateMouseHorde();
            if (Input.GetKeyUp(KeyCode.Alpha2))
                ActivateAllMouseDie();
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
    }
}