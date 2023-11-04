using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities.Elements
{
    [RequireComponent(typeof(Mouse))]
    public class MouseDirectionChanger : MonoBehaviour
    {
        [field: SerializeField]
        public float DirChangeCooldown { get; private set; }

        private Mouse mouse;

        private void Awake()
        {
            mouse = GetComponent<Mouse>();
        }

        private float elapsedTime;
        private void FixedUpdate()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > DirChangeCooldown)
            {
                elapsedTime = 0;

                mouse.AngleDeg = Random.Range(0, 360);
            }
        }
    }
}