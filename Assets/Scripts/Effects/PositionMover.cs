using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class PositionMover : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        private float elapsedTime;
        
        [SerializeField]
        private float delay;

        [SerializeField]
        private Vector3 targetPosShift;
        private Vector3 initialPos;


        private void Awake()
        {
            initialPos = transform.position;
        }

        private void FixedUpdate()
        {
            var ratio = Mathf.Clamp01((elapsedTime - delay) / duration);

            transform.position = Vector3.Lerp(initialPos, initialPos + targetPosShift, ratio);

            elapsedTime += Time.deltaTime;
        }
    }
}