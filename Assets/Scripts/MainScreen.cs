using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private GameObject RulesScreen;

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OpenRuleButton;
    [SerializeField] private Button CloseRuleButton;

    private void Start() 
    {
        SetupRule();
        PlayButton.onClick.AddListener(StartGame);
        OpenRuleButton.onClick.AddListener(OpenRule);
        CloseRuleButton.onClick.AddListener(CloseRule);
    }

    private void SetupRule()
    {
        RulesScreen.transform.position = new Vector3(transform.position.x, -Screen.height);
        RulesScreen.SetActive(false);
    }

    private void StartGame()
    {
        throw new NotImplementedException();
    }

    private void OpenRule() {
        RulesScreen.SetActive(true);
        RulesScreen.transform.DOMove(MenuScreen.transform.position, 1f);
    }

    private void CloseRule()
    {
        DOTween.Sequence()
            .Append(RulesScreen.transform.DOMoveY(-Screen.height, 1f))
            .AppendCallback(() => RulesScreen.SetActive(false));
    }
}
