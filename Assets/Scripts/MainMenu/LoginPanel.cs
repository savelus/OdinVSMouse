using System;
using Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _button;
        private Action _onCloseAction;

        private void Awake()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(AddLogin);
        }

        public void OpenWindow(Action closeAction)
        {
            _onCloseAction += closeAction;
        }
        private void AddLogin()
        {
            var userName = _inputField.text;
            if (string.IsNullOrEmpty(userName))
                userName = "Плачущая мышь";
            
            PlayerPrefs.SetString("username", userName);
            _onCloseAction?.Invoke();
        }
    }
}
