using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteChooser : MonoBehaviour
{
    [field: SerializeField]
    public Sprite[] Sprites { get; private set; }

    private void Awake()
    {
        var sprite = Sprites[Random.Range(0, Sprites.Length)];
        GetComponent<Image>().sprite = sprite;
    }
}