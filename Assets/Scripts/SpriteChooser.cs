using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteChooser : MonoBehaviour
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
        }
    }
}