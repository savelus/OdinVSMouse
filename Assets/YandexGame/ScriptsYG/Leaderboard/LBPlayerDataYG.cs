using System;
using UnityEngine;
using UnityEngine.UI;
#if YG_TEXT_MESH_PRO
using TMPro;
#endif

namespace YG
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#7f075606f6c24091926fa3ad7ab59d10")]
    public class LBPlayerDataYG : MonoBehaviour
    {
        public ImageLoadYG imageLoad;
        public Image highlightedComponent;

        [Serializable]
        public struct TextLegasy
        {
            public Text rank, name, score;
        }
        public TextLegasy textLegasy;

#if YG_TEXT_MESH_PRO
        [Serializable]
        public struct TextMP
        {
            public TextMeshProUGUI rank, name, score;
        }
        public TextMP textMP;
#endif

        public class Data
        {
            public string rank;
            public string name;
            public string score;
            public string photoUrl;
            public bool inTop;
            public bool thisPlayer;
            public Sprite photoSprite;
        }

        [HideInInspector]
        public Data data = new();


        private static readonly Color TopColor = new(255/255f, 231/255f, 109/255f, 255/255f);
        private static readonly Color PlayerColor = new(132/255f, 0/255f, 255/255f, 255/255f);

        [ContextMenu(nameof(UpdateEntries))]
        public void UpdateEntries()
        {
            if (textLegasy.rank && data.rank != null) textLegasy.rank.text = data.rank;
            if (textLegasy.name && data.name != null) textLegasy.name.text = data.name;
            if (textLegasy.score && data.score != null) textLegasy.score.text = data.score;

#if YG_TEXT_MESH_PRO
            if (textMP.rank && data.rank != null) textMP.rank.text = data.rank;
            if (textMP.name && data.name != null) textMP.name.text = data.name;
            if (textMP.score && data.score != null) textMP.score.text = data.score;
#endif
            if (imageLoad)
            {
                if (data.photoSprite)
                {
                    imageLoad.PutSprite(data.photoSprite);
                }
                else if (data.photoUrl == null)
                {
                    imageLoad.ClearImage();
                }
                else
                {
                    imageLoad.Load(data.photoUrl);
                }
            }

            SetBackground();
        }

        private void SetBackground()
        {
            if(highlightedComponent == null) return;

            if (data.inTop)
                EnableBackground(TopColor);
            if (data.thisPlayer)
                EnableBackground(PlayerColor);
            if(!data.inTop && !data.thisPlayer)
                highlightedComponent.enabled = false;
        }

        private void EnableBackground(Color backgroundColor)
        {
            highlightedComponent.enabled = true;
            highlightedComponent.color = backgroundColor;
        }
    }
}