using System;
using DataBase;

namespace Data
{
    public static class StaticGameData
    {

        public static int KilledMouseInGame
        {
            get => _killedMouseInGame;
            set
            {
                _killedMouseInGame = value;
                _onMouseKill?.Invoke(value);
            }
        }

        private static int _killedMouseInGame;

        private static Action<int> _onMouseKill;

        public static void SubscribeOnKillMouseInGame(Action<int> killedMouse)
        {
            _onMouseKill += killedMouse;
        }

        public static void UnsubscribeOnKillMouseInGame(Action<int> killedMouse)
        {
            _onMouseKill -= killedMouse;
        }
    }
}