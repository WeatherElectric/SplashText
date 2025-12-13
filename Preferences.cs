// ReSharper disable MemberCanBePrivate.Global, these categories may be used outside of this namespace to create bonemenu options.
namespace WeatherElectric.SplashText;

internal static class Preferences
{
    public static readonly MelonPreferences_Category OwnCategory = MelonPreferences.CreateCategory("SplashText");
    
    public static MelonPreferences_Entry<bool> OfflineMode { get; set; }
    public static MelonPreferences_Entry<SplashMode> SplashMode { get; set; }

    public static void Init()
    {
        OfflineMode = OwnCategory.CreateEntry("OfflineMode", false, "Offline Mode", "If true, the mod will not fetch splash text from an external webserver. However, you will also not get any updates to the splash text list.");
        SplashMode = OwnCategory.CreateEntry("SplashMode", SplashText.SplashMode.Bonelab, "Splash Mode", "The mode to use for splash text. UserEntries = Use user entries, Minecraft = Use Minecraft splash text, Bonelab = Use Bonelab/Bonelab community related splash text. Terraria = Use Terraria splash text.");
        OwnCategory.SetFilePath(MelonPrefs.Preferences.FilePath);
        OwnCategory.SaveToFile(false);
    }
}

internal enum SplashMode
{
    UserEntries,
    Minecraft,
    Bonelab,
    Terraria
}