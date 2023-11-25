using TMPro;
using UnityEngine;
using Utils;

namespace Effects
{
    [RequireComponent(typeof(TextMeshPro))]
    public class AddIndicatorController : MonoBehaviour
    {
        [SerializeField] private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                textMeshPro.text = value;
            }
        }

        [SerializeField] private float duration = 0.5f;
        private float elapsedTime;

        [SerializeField] private float distance = 0.5f;

        private TextMeshPro textMeshPro;

        private Vector3 initialPos;

        private void Awake()
        {
            textMeshPro = GetComponent<TextMeshPro>();
            initialPos = transform.position;
        }

        private void FixedUpdate()
        {
            var ratio = elapsedTime / duration;

            transform.position = initialPos + new Vector3(0, ratio * distance);
            textMeshPro.color = textMeshPro.color.WithAlpha(1 - Mathf.Max(0, (ratio * 2 - 1)));

            elapsedTime += Time.deltaTime;

            if (elapsedTime > duration)
            {
                Destroy(gameObject);
            }
        }
    }
}