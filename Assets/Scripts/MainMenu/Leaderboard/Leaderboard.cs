using System.Collections.Generic;
using Data;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace MainMenu.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
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