namespace WeatherElectric.SplashText.Scripts.Helpers;

internal static class EntryHelper
{
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(UserData.EntriesPath);
        if (lines.Length == 0)
        {
            ModConsole.Error("No entries found in UserEntries.txt. Defaulting to BonelabSplashes.");
            return BonelabSplashes.GetRandomOfflineSplash();
        }
        var r = rnd.Next(lines.Length);
        var randomSplash = lines[r];
        
        randomSplash = TemplateProcessing.Process(randomSplash);

        return randomSplash;
    }
}