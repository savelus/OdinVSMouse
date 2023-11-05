using Data;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(AddLogin);
        }

        private void AddLogin()
        {
            StaticGameData.Username = _inputField.text ?? "Crying mouse";
            CloseWindow();
        }

        private void CloseWindow()
        {
        
        }
    }
}
