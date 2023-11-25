using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private List<int> _checkPoints;
        [SerializeField] private List<Color> _colorsForProgresBar;
        [SerializeField] private GameObject[] _buffIcons;
        [SerializeField] private Transform _buffIconHolder;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private Image _sliderFillAreaBackground;
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private Button _buffButton;

        [SerializeField] private AudioSource _buffIsCompleteSound;
        private int _currentCheckPoint;
        private Tweener _sliderTweener;
        private int _killedMouse;

        private void Start()
        {
            StaticGameData.SubscribeOnKillMouseInGame(MouseKilled);
            _currentCheckPoint = 0;
            SetupBuffButton();
            UpdateColorsOnSlider();
            UpdateBuffIcon();
        }

        private void OnDestroy()
        {
            StaticGameData.UnsubscribeOnKillMouseInGame(MouseKilled);
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
            _slider.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            _buffButton.gameObject.SetActive(false);

            _currentCheckPoint++;
            UpdateColorsOnSlider();
            UpdateBuffIcon();
            UpdateView();

            _buffIsCompleteSound.Play();
        }

        private void UpdateBuffIcon()
        {
            _buffIconHolder.ForEachChield(chield => Destroy(chield.gameObject));
            Instantiate(_buffIcons[_currentCheckPoint], _buffIconHolder);
        }

        private void UpdateColorsOnSlider()
        {
            _sliderBackground.color = _sliderFillAreaBackground.color;
            _sliderFillAreaBackground.color = _colorsForProgresBar[_currentCheckPoint];
        }

        private void MouseKilled(int countKilledMoused)
        {
            _killedMouse = countKilledMoused;
            UpdateView();
        }

        private void UpdateView()
        {
            var postfix = GetPostfix(_killedMouse, "мышей", "мышь", "мыши", "мышей");
            _counterText.text = $"{_killedMouse} / {_checkPoints[_currentCheckPoint]} {postfix}";
            _slider.value = (float)_killedMouse / _checkPoints[_currentCheckPoint];

            if (_killedMouse >= _checkPoints[_currentCheckPoint] && !_buffButton.gameObject.activeSelf)
                ViewBuffButton();
        }

        private void ViewBuffButton()
        {
            _buffButton.gameObject.SetActive(true);
            _sliderTweener?.Play();

            _buffIsCompleteSound.Play();
        }

        private string GetPostfix(int count, string normal, string for1, string from2to4, string others)
        {
            if (count is >= 5 and <= 20 || count % 10 == 0 || count % 10 >= 5 && count % 10 <= 9)
                return normal;
            if (count % 10 == 1)
                return for1;
            if (count % 10 >= 2 && count % 10 <= 4)
                return from2to4;
            return others;
        }
    }
}