using Unity.VisualScripting;

namespace DataBase
{
    public class CurrentUser
    {
        public string Login { get; private set; }

        public int Score { get; private set; }
        public int Money { get; private set; }

        public CurrentUser(string login)
        {
            Login = login;
            Score = 0;
            Money = 0;
            var user = OdinGameDB.GetUser(this);

            if (user != null)
            {
                Score = user.Value.Score;
                Money = user.Value.Money;
            }

            OdinGameDB.CreateNewUser(this);
        }

        public void SetScore(int score)
        {
            if (score == Score) return;
            Score = score;
            OdinGameDB.UpdateUser(this);
        }

        public void AddMoney(int money)
        {
            Money += money;
            OdinGameDB.UpdateUser(this);
        }
        public void SetMoney(int money)
        {
            if (money == Money) return;
            Money = money;
            OdinGameDB.UpdateUser(this);
        }

        public override string ToString()
        {
            return $"{Login} score: {Score} money: {Money}";
        }
    }

    public struct ReadOnlyUser
    {
        public readonly string Login;
        public readonly int Score;
        public readonly int Money;

        public ReadOnlyUser(string login, int score, int money)
        {
            Login = login;
            Score = score;
            Money = money;
        }

        public override string ToString()
        {
            return $"{Login} score: {Score} money: {Money}";
        }
    }
}