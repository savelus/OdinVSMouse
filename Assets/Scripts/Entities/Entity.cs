using Data;
using Effects;
using UnityEngine;
using Utils;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private float initialSpeed;
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

        [SerializeField] private float initialAngleDeg;
        private float angleDeg;

        public float AngleDeg
        {
            get => angleDeg;
            set
            {
                angleDeg = value;
                UpdateDirection();
                OnAngleChanged();
            }
        }

        public static float SpeedModifier = 1;

        [field: SerializeField] public bool CanBeCaptured { get; set; }

        protected Rigidbody2D Rigidbody;
        protected EntityController entityController;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            entityController = GetComponentInParent<EntityController>();

            Speed = initialSpeed;
            AngleDeg = initialAngleDeg;
        }

        protected virtual void UpdateDirection() =>
            Rigidbody.velocity = MathUtils.AngleToDirection(angleDeg) * (Speed + (SpeedModifier - 1) * 2);

        protected virtual float outOfFieldIndent => 1.3f;

        private void FixedUpdate()
        {
            UpdateDirection();

            if (IsOutOfField(outOfFieldIndent))
            {
                OnOutOfField();
            }

            bool IsOutOfField(float indent) =>
                Mathf.Abs(transform.position.x - entityController.transform.position.x) >
                entityController.HalfFieldWidth * indent ||
                Mathf.Abs(transform.position.y - entityController.transform.position.y) >
                entityController.HalfFieldHeight * indent;

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnAngleChanged()
        {
        }

        protected virtual void OnOutOfField()
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
                    GameManager.Singleton.CrossDrawer?.DrawOn(point);

                DestroySelf();
                StaticGameData.KilledMouseInGame++;
                SpeedModifier += 0.01f;

                GameManager.Singleton.MouseKilledSound.Play();
            }
        }

        private float flipCooldown = 0.5f;
        private float lastFlipTime;

        protected void TryFlip()
        {
            if (Time.timeSinceLevelLoad - lastFlipTime > flipCooldown * (4 / Speed * SpeedModifier))
            {
                lastFlipTime = Time.timeSinceLevelLoad;
                Flip();
            }
        }

        protected void Flip()
        {
            transform.localScale = new(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
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
        FastZigzagMouse,
        Egle,
        Owl,
    }
}