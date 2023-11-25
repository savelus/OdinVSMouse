using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform attackPointsHolder;

        [SerializeField] private Transform head;

        [SerializeField] private Transform cloud;
        [SerializeField] private SpriteRenderer cloudFlash;
        [SerializeField] private Color cloudFlashColor;
        [SerializeField] private float cloudFlashDuration;

        [SerializeField] private SpriteRenderer stroke;

        private Vector3 initialPos;
        private Vector3 initialCloudPos;
        private Vector2 targetPos;

        private Transform[] attackPoints;

        private void Awake()
        {
            initialPos = transform.position;
            initialCloudPos = cloud.transform.localPosition;

            var points = new List<Transform>();
            attackPointsHolder.ForEachChield(point => points.Add(point.transform));
            attackPoints = points.ToArray();
        }

        private void FixedUpdate()
        {
            cloud.transform.localPosition =
                initialCloudPos + new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * 1.2f) * 0.03f, 0);
            targetPos = initialPos + new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * 1.0f) * 0.06f, 0);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.01f);
            flashElapsedTime = Mathf.Max(0, flashElapsedTime - Time.deltaTime);
            cloudFlash.color = Color.Lerp(Color.clear, cloudFlashColor, flashElapsedTime / cloudFlashDuration);

            stroke.color =
                Color.white.WithAlpha(Mathf.Lerp(0.1f, 0.25f, (Mathf.Sin(Time.timeSinceLevelLoad * 0.8f + 1) + 1) / 2));
        }

        public Vector3 GetRndAttackPoint() => attackPoints[Random.Range(0, attackPoints.Length)].position;

        public void Shake()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var side = Mathf.Sign(Mathf.Cos(MathUtils.DirectionToAngle(mousePos - transform.position) * Mathf.Deg2Rad));
            if (side != Mathf.Sign(head.transform.localScale.x))
                head.transform.localScale = head.transform.localScale.WithX(-head.transform.localScale.x);

            head.transform.eulerAngles = head.transform.eulerAngles.WithZ(Random.Range(-5, 5));

            var angle = MathUtils.DirectionToAngle(transform.position - mousePos);
            //transform.position += new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 0.03f; //отдача
        }

        private float flashElapsedTime;

        public void FlashCloud()
        {
            flashElapsedTime = cloudFlashDuration;
        }
    }
}