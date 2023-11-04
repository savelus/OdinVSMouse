using Assets.Scripts.SharedUtils.Extensions;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform attackPointsHolder;

        [SerializeField]
        private Transform head;

        [SerializeField]
        private Transform cloud;

        private Vector3 initialPos;
        private Vector3 initialCloudPos;
        private Vector2 targetPos;

        private Transform[] attackPoints;

        private void Awake()
        {
            initialPos = transform.position;
            initialCloudPos = cloud.transform.position;

            var points = new List<Transform>();
                attackPointsHolder.ForEachChield(point => points.Add(point.transform));
            attackPoints = points.ToArray();
        }

        private void FixedUpdate()
        {
            cloud.transform.position = initialCloudPos + new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad) * 0.04f, 0);
            targetPos = initialPos + new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad*0.8f) * 0.015f, 0);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.01f);
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
            transform.position += new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 0f;//* 0.03f;
        }
    }
}

