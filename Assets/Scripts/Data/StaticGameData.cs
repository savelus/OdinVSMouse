using System;

namespace Data
{
    public static class StaticGameData
    {
        public static string Username = "";
        public static bool IsFirstOpenGame = true;

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
        public static int KilledMouseInPreviousGame;

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