namespace WeatherElectric.SplashText.Melon;

internal static class UserData
{
    private static readonly string WeatherElectricPath = Path.Combine(MelonUtils.UserDataDirectory, "Weather Electric");
    private static readonly string ModPath = Path.Combine(MelonUtils.UserDataDirectory, "Weather Electric/SplashText");
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
        if (!File.Exists(EntriesPath))
        {
            File.Create(EntriesPath).Close();
        }
    }
}