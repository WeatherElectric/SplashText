// ReSharper disable MemberCanBePrivate.Global, these categories may be used outside of this namespace to create bonemenu options.
namespace WeatherElectric.SplashText.Melon;

internal static class Preferences
{
    public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    public static readonly MelonPreferences_Category OwnCategory = MelonPreferences.CreateCategory("SplashText");
    
    public static MelonPreferences_Entry<int> LoggingMode { get; set; }
    public static MelonPreferences_Entry<bool> OfflineMode { get; set; }
    public static MelonPreferences_Entry<SplashMode> SplashMode { get; set; }
    public static MelonPreferences_Entry<string> Thingy { get; set; }

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0, "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        GlobalCategory.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        OfflineMode = OwnCategory.CreateEntry("OfflineMode", false, "Offline Mode", "If true, the mod will not fetch splash text from an external webserver. However, you will also not get any updates to the splash text list.");
        SplashMode = OwnCategory.CreateEntry("SplashMode", Melon.SplashMode.Bonelab, "Splash Mode", "The mode to use for splash text. UserEntries = Use user entries, Minecraft = Use Minecraft splash text, Bonelab = Use Bonelab/Bonelab community related splash text. Terraria = Use Terraria splash text.");
        Thingy = OwnCategory.CreateEntry("Thingy", "", "Thingy");
        OwnCategory.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
        OwnCategory.SaveToFile(false);
        ModConsole.Msg("Finished preferences setup for SplashText", 1);
    }
}

internal enum SplashMode
{
    UserEntries,
    Minecraft,
    Bonelab,
    Terraria
}