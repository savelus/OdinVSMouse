using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float initialSpeed;
        private float speed;
        public float Speed
        {
            get => speed;
            private set { 
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
            set {
                angleDeg = value;
                UpdateDirection();
            }
        }

        [field: SerializeField]
        public float LiveDistance { get; set; }
        private float elapsedDistance;
        private Vector3 lastPos;

        private void UpdateDirection()
        {
            rigidbody.velocity = MathUtils.AngleToDirection(angleDeg) * Speed;
            transform.localEulerAngles = new(0, 0, angleDeg);
        }

        private new Rigidbody2D rigidbody;

        public SpriteDrawer CraterDrawer { get; set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            Speed = initialSpeed;
            AngleDeg = initialAngleDeg;
            lastPos = transform.position;
        }

        private void Start()
        {
            Speed *= 1 + LiveDistance * 0.1f;
        }

        private void Update()
        {
            elapsedDistance += (transform.position - lastPos).magnitude;

            if (elapsedDistance >= LiveDistance)
            {
                OnLiveDistanceEnded();
            }

            lastPos = transform.position;
        }

        private void OnLiveDistanceEnded()
        {
            DestroySelf();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TagManager.IsEntity(collision.tag))
            {
                OnHit();
            }
        }

        private void OnHit()
        {
            DestroySelf();
        }

        private bool isDestroyed;
        private void DestroySelf()
        {
            if (!isDestroyed)
            {
                isDestroyed = true;

                var point = Camera.main.WorldToScreenPoint(transform.position).AsVector2();
                point = new(point.x / Screen.width, point.y / Screen.height);
                if (new Rect(0, 0, 1, 1).Contains(point))
                    CraterDrawer?.DrawOn(point);
                Destroy(gameObject);
            }
        }
    }
}