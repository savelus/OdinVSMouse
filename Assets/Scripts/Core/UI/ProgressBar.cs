using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private List<int> _checkPoints;
        [FormerlySerializedAs("_colorsForProgrssBar")] [SerializeField] private List<Color> _colorsForProgresBar;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private Image _sliderFillAreaBackground;
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private Button _buffButton;
        private int _currentCheckPoint;
        private Tweener _sliderTweener;
        private int _killedMouse;

        [SerializeField] private Button _testButton;
        private void Start()
        {
            _testButton.onClick.AddListener(() => StaticGameData.KilledMouseInGame++);
            StaticGameData.SubscribeOnKillMouseInGame(MouseKilled);
            _currentCheckPoint = 0;
            SetupBuffButton();
            UpdateColorsOnSlider();
        }

        private void SetupBuffButton()
        {
            _buffButton.gameObject.SetActive(false);
            var effectScale = _slider.transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
            _sliderTweener = _slider.transform.DOScale(effectScale, 0.5f);
            _sliderTweener.SetLoops(-1, LoopType.Yoyo);
            _sliderTweener.Pause();
            _buffButton.onClick.RemoveAllListeners();
            _buffButton.onClick.AddListener(ClickOnBuffButton);
        }

        private void ClickOnBuffButton()
        {
            _sliderTweener?.Pause();
            _buffButton.gameObject.SetActive(false);

            _currentCheckPoint++;
            MouseKilled(_killedMouse);
            UpdateColorsOnSlider();
        }

        private void UpdateColorsOnSlider()
        {
            _sliderBackground.color = _sliderFillAreaBackground.color;
            _sliderFillAreaBackground.color = _colorsForProgresBar[_currentCheckPoint];
        }

        private void MouseKilled(int countKilledMoused)
        {
            _killedMouse = countKilledMoused;
            _counterText.text = $"{countKilledMoused} / {_checkPoints[_currentCheckPoint]}";
            _slider.value = (float) countKilledMoused / _checkPoints[_currentCheckPoint];

            if (countKilledMoused >= _checkPoints[_currentCheckPoint] && !_buffButton.gameObject.activeSelf)
            {
                ViewBuffButton();
            }
        }

        private void ViewBuffButton()
        {
            //_buffButton.image.color = _colorsForProgresBar[_currentCheckPoint];
            _buffButton.gameObject.SetActive(true);
            _sliderTweener?.Play();
        }
    }
    
    
}