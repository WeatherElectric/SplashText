using MelonLoader.Utils;
using WeatherElectric.MelonUserData;

namespace WeatherElectric.SplashText.Melon;

internal static class UserSplashes
{
    private static UserData _userData;
    private static string _entriesFile;

    public static void Init()
    {
        _userData = new UserData(Main.Name);
        
        var entriesFile = _userData.GetFile("UserEntries.txt");
        if (entriesFile == null)
        {
            Main.Logger.Log("Entries file not present, creating file", LogLevel.Debug);
            File.Create(Path.Combine(_userData.Path, "UserEntries.txt")).Close();
            _entriesFile = Path.Combine(_userData.Path, "UserEntries.txt");
            File.WriteAllLinesAsync(_entriesFile, BonelabSplashes.Splashes);
            var lines = File.ReadAllLines(_entriesFile);
            var trimmedLines = lines.Select(line => line.TrimEnd()).ToList();
            File.WriteAllLines(_entriesFile, trimmedLines);
        }
        else
        {
            Main.Logger.Log("Entries file found", LogLevel.Debug);
            _entriesFile = entriesFile;
        }
    }
    
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(_entriesFile);
        if (lines.Length == 0)
        {
            Main.Logger.Log("No entries found in UserEntries.txt. Defaulting to BonelabSplashes.", LogLevel.Error);
            return BonelabSplashes.GetRandomOfflineSplash();
        }
        var r = rnd.Next(lines.Length);
        var randomSplash = lines[r];
        
        randomSplash = TemplateProcessing.Process(randomSplash);

        return randomSplash;
    }
}