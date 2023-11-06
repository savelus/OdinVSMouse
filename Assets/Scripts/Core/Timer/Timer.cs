﻿using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Timer
{
    public class Timer : MonoBehaviour
    {
        public string TimeString { get; private set; }
        private float _remainingTime;
        public float RemainingTime 
        {
            get => _remainingTime;
            set
            {
                if (value >= 0)
                {
                    _remainingTime = value;
                    OnTimeChanged();
                }
            }
        }
        public bool TimerIsRunning { get; private set; }

        private float _startSeconds;
        private int _previousMinutes;
        private Action<Timer> _onTimerChanged;
        private Action<Timer> _onTimerEnd;

        private const float SecondsInOneDay = 86400;
        private const float SecondsInOneHour = 3600;
        private const float SecondsInOneMinute = 60;

        private void Awake()
        {
            SubscribeOnTimerEnd(_ => { GameManager.IsGameStarted = false; GameManager.IsGameEnded = true; });
        }

        private void Update()
        {
            if(!TimerIsRunning) return;
            var currentTime = GetCurrentTime();
            RemainingTime = currentTime;
            if (RemainingTime < 0.01f)
                EndTimer();
        }

        private void EndTimer()
        {
            RemainingTime = 0;
            StopTimer();
        }

        private void OnTimeChanged()
        {
            var worldSeconds = RemainingTime * SecondsInOneDay / _startSeconds;

            var hours = (int)Math.Truncate(worldSeconds / SecondsInOneHour);
            var minutes = (int)Math.Truncate((worldSeconds - hours * SecondsInOneHour)/ SecondsInOneMinute);
            if (minutes == _previousMinutes)
            {
                _previousMinutes = minutes;
                return;
            }

            _previousMinutes = minutes;
            TimeString = $"{hours:D2}h {minutes:D2}m";
            _onTimerChanged?.Invoke(this);
        }

        private float GetCurrentTime()
        {
            return RemainingTime - Time.deltaTime;
        }

        
        public void StartTimer(float seconds)
        {
            if (seconds <= 0) throw new ArgumentException("seconds can not 0");
            
            _startSeconds = seconds;
            RemainingTime = seconds;
            TimerIsRunning = true;
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