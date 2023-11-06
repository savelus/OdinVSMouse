using System.Collections.Generic;
using Data;
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
        [SerializeField] private LoginPanel LoginPanel;
        [SerializeField] private Leaderboard.Leaderboard Leaderboard;

        [SerializeField] private Button PlayButton;
        
        [SerializeField] private Button OpenRuleButton;
        [SerializeField] private Button CloseRuleButton;
        
        [SerializeField] private Button LeaderBoardButton;
        [SerializeField] private Button CloseLeaderBoardButton;
        

        [SerializeField] private List<Sprite> PreloadGameImages;
        [SerializeField] private Button PreloadGameImagesButton;


        private AsyncOperation _loadGameSceneOperation;
        private int _currentNumberImage;
        private Vector3 _ruleInvisiblePosition;
        private const float _moveTimeRuleScreen = 0.8f;

        private void Start()
        {
            _loadGameSceneOperation = null;

            MenuScreen.SetActive(true);
            
            _ruleInvisiblePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, -Screen.height/2, 0));
            
            SetupInvisibleScreen(RulesScreen);
            SetupInvisibleScreen(LoginPanel.gameObject);
            SetupInvisibleScreen(Leaderboard.gameObject);
            
            PlayButton.onClick.RemoveAllListeners();
            PlayButton.onClick.AddListener(StartGame);
        
            OpenRuleButton.onClick.RemoveAllListeners();
            OpenRuleButton.onClick.AddListener(() => OpenInvisibleScreen(RulesScreen));
            
            CloseRuleButton.onClick.RemoveAllListeners();
            CloseRuleButton.onClick.AddListener(() => CloseInvisibleScreen(RulesScreen));
            
            LeaderBoardButton.onClick.RemoveAllListeners();
            LeaderBoardButton.onClick.AddListener(() => OpenInvisibleScreen(Leaderboard.gameObject));
            LeaderBoardButton.onClick.AddListener(() => Leaderboard.ShowTable());
        
            CloseLeaderBoardButton.onClick.RemoveAllListeners();
            CloseLeaderBoardButton.onClick.AddListener(() => CloseInvisibleScreen(Leaderboard.gameObject));
            
            SetupPreloadGameButton();

            var userName = PlayerPrefs.GetString("username");
            if (userName is "Плачущая мышь" or "" && StaticGameData.IsFirstOpenGame)
            {
                LoginPanel.OpenWindow(() => CloseInvisibleScreen(LoginPanel.gameObject));
                OpenInvisibleScreen(LoginPanel.gameObject);
                StaticGameData.IsFirstOpenGame = false;
            }
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
            {
                OpenGameScene();
                return;
            }
            PreloadGameImagesButton.image.sprite = PreloadGameImages[_currentNumberImage];
            _currentNumberImage++;
        }

        private void OpenGameScene() => 
            _loadGameSceneOperation.allowSceneActivation = true;

        private void StartGame()
        {
            _loadGameSceneOperation =  SceneManager.LoadSceneAsync("GameScene");
            _loadGameSceneOperation.allowSceneActivation = false;
            PreloadGameImagesButton.gameObject.SetActive(true);
            MenuScreen.SetActive(false);
            RulesScreen.SetActive(false);
            NextImage();
        }

        private void SetupInvisibleScreen(GameObject screen)
        {
            screen.transform.position = _ruleInvisiblePosition;
            screen.SetActive(false);
        }

        private void OpenInvisibleScreen(GameObject screen) {
            screen.SetActive(true);
            screen.transform.DOMove(MenuScreen.transform.position, _moveTimeRuleScreen);
        }

        private void CloseInvisibleScreen(GameObject screen)
        {
            DOTween.Sequence()
                .Append(screen.transform.DOMove(_ruleInvisiblePosition, _moveTimeRuleScreen))
                .AppendCallback(() => screen.SetActive(false));
        }
    }
}
