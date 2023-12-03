using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace MainMenu
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private GameObject MenuScreen;
        [SerializeField] private GameObject RulesScreen;
        [SerializeField] private GameObject Leaderboard;

        [SerializeField] private Button PlayButton;

        [SerializeField] private Button OpenRuleButton;
        [SerializeField] private Button CloseRuleButton;

        [SerializeField] private Button LeaderBoardButton;
        [SerializeField] private Button CloseLeaderBoardButton;


        [SerializeField] private List<Sprite> PreloadGameImages;
        [SerializeField] private Button PreloadGameImagesButton;

        [SerializeField] private AudioSource ButtonSource;

        private int _currentNumberImage;
        private Vector3 _ruleInvisiblePosition;
        private bool _dataLoaded;
        private const float _moveTimeRuleScreen = 0.8f;


        private void Start()
        {
            Debug.Log("mainscreen start");
            if (YandexGame.SDKEnabled)
            {
                GetData();
            }
        }

        private void OnEnable() => YandexGame.GetDataEvent += GetData;

        private void OnDisable() => YandexGame.GetDataEvent -= GetData;

        private void GetData()
        {
            if(_dataLoaded) return;
            Debug.Log("get data start");

            MenuScreen.SetActive(true);

            _ruleInvisiblePosition =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -Screen.height / 2, 0));

            SetupInvisibleScreen(RulesScreen);
            SetupInvisibleScreen(Leaderboard.gameObject);

            PlayButton.onClick.RemoveAllListeners();
            PlayButton.onClick.AddListener(StartGame);
            PlayButton.onClick.AddListener(ButtonSource.Play);

            OpenRuleButton.onClick.RemoveAllListeners();
            OpenRuleButton.onClick.AddListener(() => OpenInvisibleScreen(RulesScreen));
            OpenRuleButton.onClick.AddListener(ButtonSource.Play);

            CloseRuleButton.onClick.RemoveAllListeners();
            CloseRuleButton.onClick.AddListener(() => CloseInvisibleScreen(RulesScreen));
            CloseRuleButton.onClick.AddListener(ButtonSource.Play);

            LeaderBoardButton.onClick.RemoveAllListeners();
            LeaderBoardButton.onClick.AddListener(() => OpenInvisibleScreen(Leaderboard));
            LeaderBoardButton.onClick.AddListener(ButtonSource.Play);

            CloseLeaderBoardButton.onClick.RemoveAllListeners();
            CloseLeaderBoardButton.onClick.AddListener(() => CloseInvisibleScreen(Leaderboard));
            CloseLeaderBoardButton.onClick.AddListener(ButtonSource.Play);

            SetupPreloadGameButton();
            
            _dataLoaded = true;
        }

        private void SetupPreloadGameButton()
        {
            _currentNumberImage = 0;
            PreloadGameImagesButton.onClick.RemoveAllListeners();
            PreloadGameImagesButton.onClick.AddListener(NextImage);
            PreloadGameImagesButton.onClick.AddListener(ButtonSource.Play);
            PreloadGameImagesButton.gameObject.SetActive(false);
        }

        private void NextImage()
        {
            if (_currentNumberImage >= PreloadGameImages.Count)
            {
                SceneManager.LoadScene("GameScene");
                return;
            }

            PreloadGameImagesButton.image.sprite = PreloadGameImages[_currentNumberImage];
            _currentNumberImage++;
        }

        private void StartGame()
        {
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

        private void OpenInvisibleScreen(GameObject screen)
        {
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