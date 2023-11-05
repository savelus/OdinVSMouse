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
        private void Start()
        {
            _countdown.ViewCountdown(3, StartGame);
        }

        private void StartGame()
        {
            _timer.SubscribeOnTimerChange(timer => _timerText.text = timer.TimeString);
            _entityController.StartGame();
            _timer.StartTimer(72);
        }
        
    }
}
