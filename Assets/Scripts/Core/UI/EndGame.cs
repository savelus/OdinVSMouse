using System;
using System.Collections.Generic;
using Data;
using MainMenu.Leaderboard;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countKilledMouse;
        [SerializeField] private TMP_Text _message;
        [Space]
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _mainMenuButton;

        [SerializeField] private List<Message> _messages;
        private void Awake()
        {
            _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Scenes/MainMenuScene"));
            _restartGameButton.onClick.AddListener(StartGame);
        }

        public void ViewEndGameScreen()
        {
            gameObject.SetActive(true);

            var killedMouse = StaticGameData.KilledMouseInGame;
            _countKilledMouse.text = killedMouse.ToString();
            _message.text = SetMessage(killedMouse);
            
            Leaderboard.SetLeaderboardEntry();
        }

        private string SetMessage(int killedMouse)
        {
            for (var i = 0; i < _messages.Count; i++)
                if (killedMouse >= _messages[i].KilledMouse &&
                    (i + 1 >= _messages.Count || killedMouse < _messages[i + 1].KilledMouse))
                    return _messages[i].Text;
            return "";
        }

        private void StartGame()
        {
        
        }
    }

    [Serializable]
    public class Message
    {
        public int KilledMouse;
        public string Text;
    
    }
}