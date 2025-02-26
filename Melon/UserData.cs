using MelonLoader.Utils;
using UnityEngine.Networking;

namespace WeatherElectric.SplashText.Melon;

internal static class UserData
{
    private static readonly string WeatherElectricPath = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric");
    private static readonly string ModPath = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric/SplashText");
    public static readonly string EntriesPath = Path.Combine(ModPath, "UserEntries.txt");

    public static void Setup()
    {
        if (!Directory.Exists(WeatherElectricPath))
        {
            Directory.CreateDirectory(WeatherElectricPath);
        }
        if (!Directory.Exists(ModPath))
        {
            Directory.CreateDirectory(ModPath);
        }

        if (File.Exists(EntriesPath)) return;
        File.Create(EntriesPath).Close();
        File.WriteAllLinesAsync(EntriesPath, BonelabSplashes.Splashes);
        var lines = File.ReadAllLines(EntriesPath);
        var trimmedLines = lines.Select(line => line.TrimEnd()).ToList();
        File.WriteAllLines(EntriesPath, trimmedLines);
    }
}