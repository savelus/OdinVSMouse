using System.Collections.Generic;
using Dan.Main;
using Dan.Models;
using Data;
using UnityEngine;
using YG;

namespace MainMenu.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardCell> _cells;

        private const string PublicKey = "2be87472caf559dd7184b9d9d77ad4cc9bf549fcc8036961ef2a7537a2a0ad2c";

        public void ShowTable()
        {
            LeaderboardCreator.GetLeaderboard(PublicKey, FillTable);
            //var f = YandexGame.GetLeaderboard("LeaderboardOdin", 20, );
        }

        public static void SetLeaderboardEntry()
        {
            //LeaderboardCreator.UploadNewEntry(PublicKey, PlayerPrefs.GetString("username"), StaticGameData.KilledMouseInGame);
            YandexGame.NewLeaderboardScores("LeaderboardOdin", StaticGameData.KilledMouseInGame);
        }

        private void FillTable(Entry[] msg)
        {
            // for (int i = 0; i < _cells.Count; i++)
            // {
            //     if (i < msg.Length)
            //         _cells[i].FillCell(msg[i].Rank.ToString(), msg[i].Username, msg[i].Score.ToString());
            //     else
            //         _cells[i].FillCell((i + 1).ToString(), "------", "--");
            // }
        }
    }
}