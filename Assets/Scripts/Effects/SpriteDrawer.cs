using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Effects
{
    [RequireComponent(typeof(Image))]
    public class SpriteDrawer : MonoBehaviour
    {
        [SerializeField] private Sprite spriteToGetSize;

        [SerializeField] private Sprite appyingSprite;
        [SerializeField] private int applyinSpriteWidth = 150;

        private Texture2D texture;

        [SerializeField] private bool useDepth;
        private int[] depth;
        [SerializeField] private bool useDarkReplace;

        private Color[] applyinTextureColors;

        private float ratio;
        private Vector2Int asSize;
        Vector2Int expandedSize;

        private void Awake()
        {
            ratio = (float)appyingSprite.texture.height / appyingSprite.texture.width;
            asSize = new(applyinSpriteWidth, (int)(applyinSpriteWidth * ratio));
            expandedSize = new(spriteToGetSize.texture.width + asSize.x / 2,
                spriteToGetSize.texture.height + asSize.y / 2);


            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = new(-asSize.x / 2, -asSize.y / 2);
            //rectTransform.offsetMax = new(asSize.x / 2, asSize.y / 2);


            texture = new Texture2D(expandedSize.x, expandedSize.y);
            texture.SetPixels32(Enumerable.Repeat(new Color32(), texture.width * texture.height).ToArray());
            texture.Apply();
            var image = GetComponent<Image>();
            image.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2, texture.height / 2)
            );
            image.color = Color.white;

            if (useDepth)
                depth = Enumerable.Repeat(texture.height, expandedSize.x * expandedSize.y).ToArray();

            applyinTextureColors = appyingSprite.texture.GetPixels();
        }

        /// <param name="posInTexture"> Position limited in 0 to 1 </param>
        public void DrawOn(Vector2 posInTexture)
        {
            Vector2Int pos = new((int)(posInTexture.x * texture.width - asSize.x / 2),
                (int)(posInTexture.y * texture.height - asSize.y / 2));
            pos += new Vector2Int(asSize.x / 2, asSize.y / 2);
            pos = new Vector2Int((int)(pos.x / (1 + (float)asSize.x / 2 / spriteToGetSize.texture.width)),
                (int)(pos.y / (1 + (float)asSize.y / 2 / spriteToGetSize.texture.height)));
            Vector2Int size = new(appyingSprite.texture.width, appyingSprite.texture.height);

            Vector2 scale = new((float)asSize.x / appyingSprite.texture.width,
                (float)asSize.y / appyingSprite.texture.height);
            Vector2Int trAvSize = new(Math.Min(asSize.x + pos.x, texture.width) - pos.x,
                Math.Min(asSize.y + pos.y, texture.height) - pos.y);

            Color[] oldColors = texture.GetPixels(pos.x, pos.y, trAvSize.x, trAvSize.y);
            Color[] newColors = new Color[trAvSize.x * trAvSize.y];
            var appyingTextureOriginWidth = appyingSprite.texture.width;
            float texureHeight = texture.height;
            int curDepth = pos.y;
            ///Parallel.For(0, trAvSize.y, y => {
            for (int y = 0; y < trAvSize.y; y++)
            {
                for (int x = 0; x < trAvSize.x; x++)
                {
                    var index = x + y * trAvSize.x;
                    var depthIndex = pos.x + x + (pos.y + y) * expandedSize.x;
                    var oldPixel = oldColors[index];
                    var newPixel =
                        applyinTextureColors[(int)((x / scale.x) + (int)(y / scale.y) * (appyingTextureOriginWidth))];
                    if (!useDarkReplace || (oldPixel.a < 0.95f ||
                                            (newPixel.a >= 0.95f &&
                                             newPixel.maxColorComponent < oldPixel.maxColorComponent)))
                    {
                        if (useDepth)
                        {
                            if (curDepth < depth[depthIndex])
                            {
                                if (newPixel.a > 0.90f)
                                    depth[depthIndex] = curDepth;
                                newColors[index] = Color.Lerp(oldPixel, newPixel, newPixel.a)
                                    .WithAlpha(oldPixel.a + newPixel.a);
                            }
                            else
                            {
                                newColors[index] = oldPixel;
                            }
                        }
                        else
                        {
                            newColors[index] = Color.Lerp(oldPixel, newPixel, newPixel.a)
                                .WithAlpha(oldPixel.a + newPixel.a);
                        }
                    }
                    else
                    {
                        newColors[index] = oldPixel;
                    }
                }
            }

            texture.SetPixels(pos.x, pos.y, trAvSize.x, trAvSize.y, newColors);

            texture.Apply();
        }

        private void OnDestroy()
        {
            Texture2D.Destroy(texture);
        }

        public static Vector2 TransformToScreenSpace(Vector3 position)
        {
            var point = Camera.main.WorldToScreenPoint(position).AsVector2();
            point = new(point.x / Screen.width, point.y / Screen.height);
            return point;
        }
    }
}