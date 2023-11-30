using System.Collections.Generic;
using System.Data;
using PUSHKA.MySQL;

namespace DataBase
{
    public static class OdinGameDB
    {
        /*private const string IP = "185.139.69.192";
        private const string BaseName = "odingame";
        private const string TableName = "USERS";
        private const string DataBaseUser = "USERNAME";
        private const string DataBasePassword = "Password1!";

        private static readonly SqlDataBase Base = new(IP, BaseName, DataBaseUser, DataBasePassword);

        public static List<ReadOnlyUser> GetUsersList()
        {
            var rawData = SelectQuery($"SELECT * FROM {BaseName}.{TableName}");
            if (rawData is null || rawData.Rows.Count == 0) return null;

            var readonlyUsers = new List<ReadOnlyUser>();
            for (var i = 0; i < rawData.Rows.Count; i++)
            {
                var row = rawData.Rows[i];
                var user = new ReadOnlyUser(row[0].ToString(), (int)row[1], (int)row[2]);
                readonlyUsers.Add(user);
            }

            return readonlyUsers;
        }

        public static CurrentUser CreateNewUser(CurrentUser currentUser)
        {
            RunQuery(
                $"INSERT INTO {BaseName}.{TableName} (`LOGIN`, `SCORE`, `MONEY`) VALUES ({currentUser.Login}, '0', '0')");
            return currentUser;
        }

        public static ReadOnlyUser? GetUser(CurrentUser currentUser)
        {
            var rawData = SelectQuery($"SELECT * FROM {BaseName}.{TableName} WHERE LOGIN = {currentUser.Login}");
            if (rawData == null || rawData.Rows.Count == 0) return null;
            var rawUserInfo = rawData.Rows[0];
            return new ReadOnlyUser(currentUser.Login, (int)rawUserInfo[1], (int)rawUserInfo[2]);
        }
        
        public static ReadOnlyUser? GetUser(string login)
        {
            var rawData = SelectQuery($"SELECT * FROM {BaseName}.{TableName} WHERE LOGIN = {login}");
            if (rawData == null || rawData.Rows.Count == 0) return null;
            var rawUserInfo = rawData.Rows[0];
            return new ReadOnlyUser(login, (int)rawUserInfo[1], (int)rawUserInfo[2]);
        }

        public static void UpdateUser(CurrentUser currentUser)
        {
            RunQuery(
                $"UPDATE {BaseName}.{TableName} SET `SCORE` = {currentUser.Score}, `MONEY` = {currentUser.Money} WHERE (`LOGIN` = {currentUser.Login})");
        }

        private static void RunQuery(string query) =>
            Base.RunQuery(query);

        private static DataTable SelectQuery(string query)
        {
            Base.SelectQuery(query, out var data);
            return data;
        }*/
    }
}