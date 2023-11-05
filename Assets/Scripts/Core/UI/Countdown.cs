using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Core.UI
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counter;

        private float _remainingTime;
        private bool _isTimerPLaying;

        private int _timeOnScreen;
        private Action _endCallback;

        public void ViewCountdown(int remainingTime, Action endCallback)
        {
            _endCallback = endCallback;
            gameObject.SetActive(true);
            _remainingTime = remainingTime + 1;
            _isTimerPLaying = true;
            SetupRemainingTimeOnScreen();
        }

        private void Update()
        {
            if(!_isTimerPLaying) return;

            if (_remainingTime - Time.deltaTime <= 0)
            {
                _isTimerPLaying = false;
                gameObject.SetActive(false);
                _endCallback?.Invoke();
            }

            _remainingTime -= Time.deltaTime;
            
            if (_timeOnScreen == (int)_remainingTime) return;
            SetupRemainingTimeOnScreen();
        }

        private void SetupRemainingTimeOnScreen()
        {
            _timeOnScreen = (int)_remainingTime;
            _counter.text = _timeOnScreen.ToString();
        }
    }
}
