﻿using Data;
using UnityEngine;
using Utils;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField]
        private float initialSpeed;
        private float speed;
        public float Speed
        {
            get => speed;
            protected set
            {
                speed = value;
                UpdateDirection();
            }
        }

        [SerializeField]
        private float initialAngleDeg;
        private float angleDeg;
        public float AngleDeg
        {
            get => angleDeg;
            set
            {
                angleDeg = value;
                UpdateDirection();
                Flip();
            }
        }

        public static float SpeedModifier = 1;

        [field: SerializeField]
        public SpriteDrawer CrossDrawer { get; set; }

        private new Rigidbody2D rigidbody;
        private EntityController entityController;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            entityController = GetComponentInParent<EntityController>();

            Speed = initialSpeed;
            AngleDeg = initialAngleDeg;
            lastFlipTime = Time.timeSinceLevelLoad;
        }

        private void UpdateDirection()
        {
            rigidbody.velocity = MathUtils.AngleToDirection(angleDeg) * Speed * SpeedModifier;
        }
        
        private void FixedUpdate()
        {
            UpdateDirection();

            if (IsOutOfField(1.3f))
            {
                OnOutOfField();
            }

            bool IsOutOfField(float indent) =>
                Mathf.Abs(transform.position.x - entityController.transform.position.x) > entityController.HalfFieldWidth * indent ||
                Mathf.Abs(transform.position.y - entityController.transform.position.y) > entityController.HalfFieldHeight * indent;

            TryFlip();
        }

        private float flipCooldown = 0.2f;
        private float lastFlipTime;
        private void TryFlip()
        {
            if (Time.timeSinceLevelLoad - lastFlipTime > flipCooldown)
            {
                lastFlipTime = Time.timeSinceLevelLoad;
                Flip();
            }
        }
        private void Flip()
        {
            transform.localScale = new(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        private void OnOutOfField()
        {
            DestroySelf();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagManager.Projectile))
            {
                Hit(collision.transform);
            }
        }

        protected virtual void Hit(Transform hitter)
        {
            
        }

        public bool IsDestroyed { get; private set; }
        public virtual void Die()
        {
            if (!IsDestroyed)
            {
                var point = SpriteDrawer.TransformToScreenSpace(transform.position);
                if (new Rect(0, 0, 1, 1).Contains(point))
                    CrossDrawer?.DrawOn(point);

                DestroySelf();
                StaticGameData.KilledMouseInGame++;
            }
        }

        private void DestroySelf()
        {
            IsDestroyed = true;
            Destroy(gameObject);
        }
    }

    public enum EntityType
    {
        BasicMouse,
        FastMouse,
        ZigzagMouse,
        FastZigzagMouse
    }
}