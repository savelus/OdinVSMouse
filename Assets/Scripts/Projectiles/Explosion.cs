using UnityEngine;
using Utils;

namespace Projectiles
{
    public class Explosion : MonoBehaviour
    {
        [field: SerializeField]
        public float StartSize { get; private set; }

        [field: SerializeField]
        public float EndSize { get; private set; }

        [field: SerializeField]
        public float Duration { get; private set; } = 1;
        private float elapsedTime;

        private SpriteRenderer spriteRenderer;

        private Vector3 initialPos;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            initialPos = transform.position;
            Redraw(0);
        }

        private void FixedUpdate()
        {
            float progress = elapsedTime / Duration;
            Redraw(progress);

            elapsedTime += Time.deltaTime;
            if (elapsedTime > Duration)
            {
                Destroy(gameObject);
            }
        }

        private void Redraw(float progress)
        {
            transform.localScale = Vector3.Lerp(Vector3.one * StartSize, Vector3.one * EndSize, progress);

            spriteRenderer.color = spriteRenderer.color.WithAlpha(1 - Mathf.Max(0, progress* 2 - 1));
        }
    }
}