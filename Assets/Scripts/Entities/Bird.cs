using Entities;
using System.Collections;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Entities
{
    public class Bird : Entity
    {
        private float _outOfFieldIndent = 1f;
        protected override float outOfFieldIndent => _outOfFieldIndent;

        private Transform capturedEnemy;

        private float lastAngleChangeTime;
        protected override void OnOutOfField()
        {
            if (Time.timeSinceLevelLoad - lastAngleChangeTime > 0.5f)
            {
                lastAngleChangeTime = Time.timeSinceLevelLoad;
                var angleToCenter = MathUtils.DirectionToAngle(entityController.transform.position - transform.position);
                AngleDeg = angleToCenter + Random.Range(-45, 45);

                if (_outOfFieldIndent > 1)
                    _outOfFieldIndent = 1;
            }
        }

        private void Update()
        {
            if (capturedEnemy != null)
                capturedEnemy.position = transform.position;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.CompareTag(TagManager.Enemy))
            {
                _outOfFieldIndent = 1.4f;
                capturedEnemy = collision.transform;
            }
        }
    }
}