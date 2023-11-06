using Assets.Scripts.Effects;
using Core.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton { get; private set; }

        [field: SerializeField]
        public SpriteDrawer CraterDrawer { get; private set; }

        [field: SerializeField]
        public SpriteDrawer CrossDrawer { get; private set; }

        [field: SerializeField]
        public Timer Timer { get; private set; }

        [field: SerializeField]
        public EffectManager EffectManager { get; private set; }

        public static bool IsGameStarted { get; set; }

        private void Awake()
        {
            Singleton = this;
        }
    }
}
