using Core.Timer;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer _timer; 
    private void Start()
    {
        _timer.SubscribeOnTimerChange(SetTimer);
        
        _timer.StartTimer(10);
    }

    private void SetTimer(Timer timer)
    {
        _timerText.text = timer.TimeString;
    }
}
