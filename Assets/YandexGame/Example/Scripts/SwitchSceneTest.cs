using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YG.Example
{
    public class SwitchSceneTest : MonoBehaviour
    {
        public void SwitchScene(int sceneID)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneID);
        }
    }
}