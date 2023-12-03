using Data;
using Entities;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private EntityController _entityController;
        [SerializeField] private Countdown _countdown;
        [SerializeField] private EndGame _endGame;
        [SerializeField] private float _influenceLastGame = 1;

        [SerializeField] private AudioSource _backgroundGameMusic;

        private void Start()
        {
            InitGame();
            Debug.Log("game inited");
            _countdown.ViewCountdown(3, StartGame);
        }

        private void InitGame()
        {
            GameManager.IsGameEnded = false;
            GameManager.IsGameStarted = false;
            Entity.SpeedModifier = _influenceLastGame * (1 + StaticGameData.KilledMouseInGame * 0.005f / 2) * 0.5f;
            StaticGameData.KilledMouseInGame = 0;
        }

        private void StartGame()
        {
            GameManager.IsGameStarted = true;
            _timer.SubscribeOnTimerChange(timer => _timerText.text = timer.TimeString);
            _timer.SubscribeOnTimerEnd(_ =>
            {
                _endGame.ViewEndGameScreen();
                _backgroundGameMusic.Stop();
            });

            _timer.StartTimer(24);
            _backgroundGameMusic.Play();
        }
    }
}