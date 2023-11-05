using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Core.Boosts
{
    public class BoostController : MonoBehaviour
    {
        [SerializeField]
        private int hordeSize = 60;

        [SerializeField]
        private EntityController entityController;

        [SerializeField]
        private GameObject explosionPrefab;

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

        private void Update()
        {
            for (int i = 0; i < System.Math.Min(2, mousesToDie.Count); i++)
            {
                var mouseToDie = mousesToDie[^1];
                mousesToDie.RemoveAt(mousesToDie.Count - 1);

                mouseToDie.Die();
                Instantiate(explosionPrefab, mouseToDie.transform.position, new Quaternion());
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
                ActivateMouseHorde();
            if (Input.GetKeyUp(KeyCode.Alpha2))
                ActivateAllMouseDie();
        }
    }
}