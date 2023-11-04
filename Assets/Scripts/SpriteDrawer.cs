using Assets.Scripts.SharedUtils.Extensions;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static System.Math;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Image))]
    public class SpriteDrawer : MonoBehaviour
    {
        [SerializeField] 
        private Sprite spriteToGetSize;

        [SerializeField]
        private Sprite appyingSprite;
        [SerializeField]
        private int applyinSpriteWidth = 150;

        private Texture2D texture;

        private Color[] applyinTextureColors;

        private float ratio;
        private Vector2Int asSize;

        private void Awake()
        {
            ratio = (float)appyingSprite.texture.height / appyingSprite.texture.width;
            asSize = new(applyinSpriteWidth, (int)(applyinSpriteWidth * ratio));


            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = new(-asSize.x / 2, -asSize.y / 2);
            //rectTransform.offsetMax = new(asSize.x / 2, asSize.y / 2);


            texture = new Texture2D(spriteToGetSize.texture.width + asSize.x / 2, spriteToGetSize.texture.height + asSize.y / 2);
            texture.SetPixels32(Enumerable.Repeat(new Color32(), texture.width * texture.height).ToArray());
            texture.Apply();
            var image = GetComponent<Image>();
            image.sprite = Sprite.Create(
                texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(texture.width / 2, texture.height / 2)
            );
            image.color = Color.white;

            applyinTextureColors = appyingSprite.texture.GetPixels();
        }

        /// <param name="posInTexture"> Position limited in 0 to 1 </param>
        public void DrawOn(Vector2 posInTexture)
        {
            Vector2Int pos = new((int)(posInTexture.x * texture.width - asSize.x / 2), (int)(posInTexture.y * texture.height - asSize.y / 2));
            pos += new Vector2Int(asSize.x / 2, asSize.y / 2);
            pos = new Vector2Int((int)(pos.x / (1 + (float)asSize.x / 2 / spriteToGetSize.texture.width)), (int)(pos.y / (1 + (float)asSize.y / 2 / spriteToGetSize.texture.height)));
            Vector2Int size = new(appyingSprite.texture.width, appyingSprite.texture.height);
            
            Vector2 scale = new((float)asSize.x / appyingSprite.texture.width, (float)asSize.y / appyingSprite.texture.height);
            Vector2Int trAvSize = new(Min(asSize.x + pos.x, texture.width) - pos.x, Min(asSize.y + pos.y, texture.height) - pos.y);

            Color[] oldColors = texture.GetPixels(pos.x, pos.y, trAvSize.x, trAvSize.y);
            Color[] newColors = new Color[trAvSize.x * trAvSize.y];
            var appyingTextureOriginWidth = appyingSprite.texture.width;
            Parallel.For(0, trAvSize.y, y => {
                for (int x = 0; x < trAvSize.x; x++)
                {
                    var oldPixel = oldColors[x + y * trAvSize.x];
                    var newPixel = applyinTextureColors[(int)((x / scale.x) + (int)(y / scale.y) * (appyingTextureOriginWidth))];
                    if (oldPixel.a < 0.95f || (newPixel.a >= 0.95f && newPixel.maxColorComponent < oldPixel.maxColorComponent))
                        newColors[x + y * trAvSize.x] = Color.Lerp(oldPixel, newPixel, newPixel.a).WithAlpha(oldPixel.a + newPixel.a);
                    else
                        newColors[x + y * trAvSize.x] = oldPixel;
                }
            });

            texture.SetPixels(pos.x, pos.y, trAvSize.x, trAvSize.y, newColors);

            texture.Apply();
        }
    }
}