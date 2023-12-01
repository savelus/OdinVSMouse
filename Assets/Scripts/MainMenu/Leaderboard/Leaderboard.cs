using System.Collections.Generic;
using Dan.Main;
using Dan.Models;
using Data;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace MainMenu.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardCell> _cells;

        private const string PublicKey = "2be87472caf559dd7184b9d9d77ad4cc9bf549fcc8036961ef2a7537a2a0ad2c";


        public static void SetLeaderboardEntry()
        {
            YandexGame.GetLeaderboard("LeaderboardOdin", 10, 3, 3, "large");
            YandexGame.onGetLeaderboard += SetEntry;
            
        }

        private static void SetEntry(LBData obj)
        {
            if(obj.thisPlayer.score < StaticGameData.KilledMouseInGame)
                YandexGame.NewLeaderboardScores("LeaderboardOdin", StaticGameData.KilledMouseInGame);
            YandexGame.onGetLeaderboard -= SetEntry;
        }
    }
}