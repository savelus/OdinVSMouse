using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
/*        private Vector3 initialPos;
        private Vector3 position;
        public Vector3 Position
        {
            get => transform.position;
            private set => transform.position = value;
        }*/

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
            rigidbody.velocity = MathUtils.AngleToDirection(angleDeg) * Speed;
        }
        
        private void FixedUpdate()
        {
            UpdateDirection();

            if (IsOutOfField(1.1f))
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
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TagManager.Projectile)
            {
                Hit(collision.transform);
            }
        }

        protected virtual void Hit(Transform hitter)
        {
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