using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwapper : MonoBehaviour
{
    [field: SerializeField]
    public Sprite[] Sprites { get; private set; }
    private int curSpriteIndex;

    [field: SerializeField]
    public float SwapCooldown { get; private set; }
    private float lastSwapTime;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastSwapTime = Time.timeSinceLevelLoad;
        curSpriteIndex = Random.Range(0, Sprites.Length);
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad - lastSwapTime > SwapCooldown)
        {
            lastSwapTime = Time.timeSinceLevelLoad;

            spriteRenderer.sprite = Sprites[curSpriteIndex];

            curSpriteIndex = (curSpriteIndex + 1) % Sprites.Length;
        }
    }
}