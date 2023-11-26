using System;
using UnityEngine;

namespace Core.Timer
{
    public class Timer : MonoBehaviour
    {
        public string TimeString { get; private set; }
        public bool TimerIsRunning { get; private set; }

        private Action<Timer> _onTimerChanged;
        private Action<Timer> _onTimerEnd;
        private float _remainingTime;
        private uint _intRemainingTime;

        private void Awake()
        {
            _onTimerEnd = null;
            _onTimerChanged = null;
            
            SubscribeOnTimerEnd(_ =>
            {
                GameManager.IsGameStarted = false;
                GameManager.IsGameEnded = true;
            });
        }

        private void Update()
        {
            if(!TimerIsRunning) return;

            ChangeTime();
        }

        public void StartTimer(uint seconds)
        {
            if (seconds <= 0) throw new ArgumentException("seconds can not 0");
            
            _remainingTime = seconds;
            _intRemainingTime = seconds;
            TimerIsRunning = true;
            
        }

        public void AddTime(float seconds)
        {
            _remainingTime += seconds;
            ChangeTime();
        }
        
        private void ChangeTime()
        {
            var deltaTime = Time.deltaTime;

            _remainingTime -= deltaTime;
            OnTimeChanged();
            
            if(_remainingTime <= 0)
                EndTimer();
        }

        private void EndTimer()
        {
            TimerIsRunning = false;
            TimeString = $"0 {GetHoursPostfix(0)}";
            _onTimerEnd?.Invoke(this);
        }

        private void OnTimeChanged()
        {
            if((uint)_remainingTime == _intRemainingTime) return;

            _intRemainingTime = (uint)_remainingTime;
            TimeString = $"{_intRemainingTime} {GetHoursPostfix(_intRemainingTime)}";
            _onTimerChanged?.Invoke(this);
        }

        private string GetHoursPostfix(uint hours)
        {
            if (hours is >= 5 and <= 20 || hours % 10 == 0 || hours % 10 >= 5 && hours % 10 <= 9)
                return "часов";
            if (hours % 10 == 1)
                return "час";
            if (hours % 10 >= 2 && hours % 10 <= 4)
                return "часа";
            return "Ч";
        }

        public void PauseTimer() =>
            TimerIsRunning = false;

        public void ResumeTimer() =>
            TimerIsRunning = true;

        public void StopTimer()
        {
            _onTimerEnd?.Invoke(this);
            _onTimerChanged = null;
            TimerIsRunning = false;
        }

        public void SubscribeOnTimerChange(Action<Timer> action) =>
            _onTimerChanged += action;

        public void UnsubscribeOnTimerChanged(Action<Timer> action) =>
            _onTimerChanged -= action;

        public void SubscribeOnTimerEnd(Action<Timer> action) =>
            _onTimerEnd += action;

        public void UnsubscribeOnTimerEnd(Action<Timer> action) =>
            _onTimerEnd -= action;
    }
}