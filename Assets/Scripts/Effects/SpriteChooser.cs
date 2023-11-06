using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class SpriteChooser : MonoBehaviour
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }

        private void Awake()
        {
            var sprite = Sprites[Random.Range(0, Sprites.Length)];
            var image = GetComponent<Image>();
            if (image != null)
                image.sprite = sprite;
            else
                GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}