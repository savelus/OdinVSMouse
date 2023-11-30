using UnityEngine;

namespace Effects
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;

        [SerializeField] private AddIndicatorController effectPrefab;

        [SerializeField] private GameObject mouseCropsePrefab;

        public void DrawExplosion(Vector3 position)
        {
            Instantiate(explosionPrefab, position, new Quaternion(), transform);
        }

        public void DrawAddIndicator(string text, Vector3 position)
        {
            var indicator = Instantiate(effectPrefab, position, new Quaternion(), transform);
            indicator.Text = text;
        }

        public void DrawMouseCropse(Vector3 position)
        {
            Instantiate(mouseCropsePrefab, position, new Quaternion(), transform);
        }
    }
}