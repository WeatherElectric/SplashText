using Il2CppSLZ.Bonelab.SaveData;
using Il2CppSLZ.Marrow.SceneStreaming;
using MelonLoader.Logging;

namespace WeatherElectric.SplashText;

public class Main : MelonMod
{
    internal const string Name = "SplashText";
    internal const string Description = "Adds splash text to Void G114's menu.";
    internal const string Author = "Mabel Amber";
    internal const string Company = "Weather Electric";
    internal const string Version = "2.4.0";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/WeatherElectric/SplashText/";

    public static Save SaveData;
    internal static LoggerInstance Logger;
    
    public override void OnInitializeMelon()
    {
        Logger = new LoggerInstance(LoggerInstance);
        Preferences.Init();
        UserSplashes.Init();
        BoneMenu.Init();
        
        SaveData = DataManager.Instance._activeSave;
        
        Hooking.OnUIRigCreated += OnUIRigCreated;
    }
    
    private static void OnUIRigCreated()
    {
        if (SceneStreamer.Session.Level.Barcode.ID != CommonBarcodes.Maps.VoidG114) return;
        Logger.Log("Void G114 loaded, creating splash text host", LogLevel.Debug);
        TextManager.Start();
    }
}