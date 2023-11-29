using System;
using DataBase;

namespace Data
{
    public static class StaticGameData
    {
        public static bool IsFirstOpenGame = true;
        public static CurrentUser CurrentUser { get; set; }

        public static int MoneyInGame
        {
            get => _moneyInGame;
            set
            {
                _moneyInGame = value;
                _onMoneyUp?.Invoke(value);
            }
        }

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
        private static int _moneyInGame;

        private static Action<int> _onMouseKill;
        private static Action<int> _onMoneyUp;

        public static void SubscribeOnKillMouseInGame(Action<int> killedMouse)
        {
            _onMouseKill += killedMouse;
        }

        public static void UnsubscribeOnKillMouseInGame(Action<int> killedMouse)
        {
            _onMouseKill -= killedMouse;
        }
        
        public static void SubscribeOnCoinUp(Action<int> uppedMoney)
        {
            _onMoneyUp += uppedMoney;
        }
        public static void UnsubscribeOnCoinUp(Action<int> uppedMoney)
        {
            _onMoneyUp -= uppedMoney;
        }
    }
}