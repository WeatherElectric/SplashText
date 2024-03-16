namespace WeatherElectric.SplashText.Scripts.Helpers;

internal static class EntryHelper
{
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(UserData.EntriesPath);
        var r = rnd.Next(lines.Length);
        var line = lines[r];
        if (line.Contains("{UserName}"))
        {
            line = line.Replace("{UserName}", Environment.UserName);
        }
        return line;
    }
}