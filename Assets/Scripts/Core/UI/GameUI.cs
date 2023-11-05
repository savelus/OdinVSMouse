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
        }

        private void StartGame()
        {
            _timer.SubscribeOnTimerChange(timer => _timerText.text = timer.TimeString);
            _timer.SubscribeOnTimerEnd(_ => _endGame.ViewEndGameScreen());
            _entityController.StartGame(_influenceLastGame * StaticGameData.KilledMouseInGame / 100);
            StaticGameData.KilledMouseInGame = 0;
            _timer.StartTimer(20);
        }
        
    }
}
