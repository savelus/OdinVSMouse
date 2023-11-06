using Assets.Scripts;
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
        private void Start()
        {
            _countdown.ViewCountdown(3, StartGame);
            InitGame();
        }

        private void InitGame()
        {
            Entity.SpeedModifier = _influenceLastGame * (1 + StaticGameData.KilledMouseInGame / 200f) * 0.5f;
        }

        private void StartGame()
        {
            GameManager.IsGameStarted = true;
            _timer.SubscribeOnTimerChange(timer => _timerText.text = timer.TimeString);
            _timer.SubscribeOnTimerEnd(_ => _endGame.ViewEndGameScreen());
            StaticGameData.KilledMouseInGame = 0;
            _timer.StartTimer(20);
        }
        
    }
}
