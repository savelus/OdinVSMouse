using TMPro;
using UnityEngine;

namespace MainMenu.Leaderboard
{
    public class LeaderboardCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _place;
        [SerializeField] private TextMeshProUGUI _login;
        [SerializeField] private TextMeshProUGUI _score;

        public void FillCell(string place, string login, string score)
        {
            _place.text = place;
            _login.text = login;
            _score.text = score;
        }
    }
}