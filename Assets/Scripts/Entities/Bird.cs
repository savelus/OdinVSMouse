using Entities;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Entities
{
    public class Bird : Entity
    {
        [SerializeField]
        private Transform[] attachmentPoints;
        private int SlotCount => attachmentPoints.Length;

        private float _outOfFieldIndent = 1f;
        protected override float outOfFieldIndent => _outOfFieldIndent;

        private Transform[] capturedEnemies;
        private int CountOfCapturedEnemies => capturedEnemies.Count(e => e != null);
        private int CountOfFreeSlots => capturedEnemies.Count(e => e == null);
        private int GetFirstFreeSlot() => Enumerable.Range(1, capturedEnemies.Length).FirstOrDefault(i => capturedEnemies[i - 1] == null) - 1;

        private float lastAngleChangeTime;
        protected override void OnOutOfField()
        {
            if (Time.timeSinceLevelLoad - lastAngleChangeTime > 0.5f)
            {
                lastAngleChangeTime = Time.timeSinceLevelLoad;
                var angleToCenter = MathUtils.DirectionToAngle(entityController.transform.position - transform.position);
                AngleDeg = angleToCenter + Random.Range(-45, 45);

                if (_outOfFieldIndent > 1)
                {
                    _outOfFieldIndent = 1;

                    foreach (var enemy in capturedEnemies)
                        if (!enemy.IsDestroyed())
                            enemy?.GetComponent<Entity>().Die();
                    capturedEnemies = new Transform[SlotCount];
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            capturedEnemies = new Transform[SlotCount];
        }

        private void Update()
        {
            for (int i = 0; i < SlotCount; i++)
            {
                if (capturedEnemies[i] != null)
                    capturedEnemies[i].position = Vector3.Lerp(capturedEnemies[i].position, attachmentPoints[i].position, Speed * 2 / 100);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.CompareTag(TagManager.Enemy))
            {
                var target = collision.transform.GetComponent<Entity>();
                if (!target.CanBeCaptured)
                    return;

                var freePointIndex = GetFirstFreeSlot();
                if (freePointIndex > -1)
                {
                    capturedEnemies[freePointIndex] = collision.transform;
                    target.CanBeCaptured = false;

                    if (CountOfFreeSlots == 0)
                        _outOfFieldIndent = 1.4f;
                }
            }
        }

        protected override void OnAngleChanged()
        {
            base.OnAngleChanged();

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(Mathf.Cos(AngleDeg * Mathf.Deg2Rad)), transform.localScale.y, transform.localScale.z);
        }
    }
}