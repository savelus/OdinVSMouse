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

        private Action[] boostActions;
        private int currentBoost;

        private Dictionary<BoostType, Action> boostWithActions = new Dictionary<BoostType, Action>();
        
        [SerializeField] private Dictionary<BoostType, GameObject> _imagesOnBoosts;
        private void Awake()
        {
            boostWithActions = new Dictionary<BoostType, Action>()
            {
                { BoostType.Eagle, ActivateEagles },
                { BoostType.Owl, ActivateOwls },
                { BoostType.Horde, ActivateMouseHorde },
                { BoostType.Die, ActivateAllMouseDie }
            };
            // boostActions = new Action[]
            // {
            //     /*0*/ ActivateMouseHorde, 
            //     /*1*/ ActivateAllMouseDie, 
            //     /*2*/ ActivateEagles,
            //     /*3*/ ActivateMouseHorde,
            //     /*4*/ ActivateOwls,
            //     /*5*/ ActivateMouseHorde,
            //     /*6*/ ActivateAllMouseDie,
            //     /*7*/ ActivateMouseHorde,
            //     /*8*/ ActivateAllMouseDie,
            //     /*9*/ ActivateMouseHorde,
            //     /*10*/ ActivateAllMouseDie,
            //     /*11*/ ActivateMouseHorde,
            //     
            // };
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

        public void ActivateNextBoost()
        {
            // if (boostActions.Length > currentBoost)
            // {
            //     boostActions[currentBoost].Invoke();
            //     currentBoost++;
            // }
        }

        public void ActivateBoost(BoostType type)
        {
            boostWithActions[type].Invoke();
        }
        public void ActivateMouseHorde()
        {
            entityController.SpawnHorde(hordeSize);
        }

        List<Mouse> mousesToDie = new();

        public void ActivateAllMouseDie()
        {
            entityController.transform.ForEachChield(chield =>
            {
                var mouse = chield.GetComponent<Mouse>();
                if (mouse != null)
                {
                    mousesToDie.Add(mouse);
                }
            });
        }

        public void ActivateEagles()
        {
            entityController.StawnEgles(2);
        }

        public void ActivateOwls()
        {
            entityController.StawnOwls(1);
        }
    }

    
}