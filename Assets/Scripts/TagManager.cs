using System.Linq;

public static class TagManager
{
    public static string Projectile => "Projectile";

    public static string Enemy => "Enemy";

    public static string Ally => "Ally";

    public static string Neutral => "Neutral";

    public static bool IsEntity(string tag) => new string[] { Enemy, Ally, Neutral }.Contains(tag);
}