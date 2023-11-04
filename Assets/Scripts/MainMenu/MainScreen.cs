using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private GameObject MenuScreen;
        [SerializeField] private GameObject RulesScreen;

        [SerializeField] private Button PlayButton;
        [SerializeField] private Button OpenRuleButton;
        [SerializeField] private Button CloseRuleButton;

        [SerializeField] private List<Sprite> PreloadGameImages;
        [SerializeField] private Button PreloadGameImagesButton;
    
        private AsyncOperation _loadGameSceneOperation;
        private int _currentNumberImage;

        private void Start()
        {
            _loadGameSceneOperation = null;

            MenuScreen.SetActive(true);
            SetupRule();

            PlayButton.onClick.RemoveAllListeners();
            PlayButton.onClick.AddListener(StartGame);
        
            OpenRuleButton.onClick.RemoveAllListeners();
            OpenRuleButton.onClick.AddListener(OpenRule);
        
            CloseRuleButton.onClick.RemoveAllListeners();
            CloseRuleButton.onClick.AddListener(CloseRule);

            SetupPreloadGameButton();
        }

        private void SetupPreloadGameButton()
        {
            _currentNumberImage = 0;
            PreloadGameImagesButton.onClick.RemoveAllListeners();
            PreloadGameImagesButton.onClick.AddListener(NextImage);
            PreloadGameImagesButton.gameObject.SetActive(false);
        }

        private void NextImage()
        {
            if (_currentNumberImage >= PreloadGameImages.Count)
                OpenGameScene();
            PreloadGameImagesButton.image.sprite = PreloadGameImages[_currentNumberImage];
            _currentNumberImage++;
        }

        private void OpenGameScene()
        {
            _loadGameSceneOperation.allowSceneActivation = true;
            
        }

        private void StartGame()
        {
            _loadGameSceneOperation =  SceneManager.LoadSceneAsync("SampleScene");
            _loadGameSceneOperation.allowSceneActivation = false;
            PreloadGameImagesButton.gameObject.SetActive(true);
            MenuScreen.SetActive(false);
            RulesScreen.SetActive(false);
            NextImage();
        }

        private void SetupRule()
        {
            RulesScreen.transform.position = new Vector3(transform.position.x, -Screen.height);
            RulesScreen.SetActive(false);
        }

        private void OpenRule() {
            RulesScreen.SetActive(true);
            RulesScreen.transform.DOMove(MenuScreen.transform.position, 1f);
        }

        private void CloseRule()
        {
            DOTween.Sequence()
                .Append(RulesScreen.transform.DOMoveY(-Screen.height, 1f))
                .AppendCallback(() => RulesScreen.SetActive(false));
        }
    }
}
