using BoneLib;
using Il2CppSLZ.Bonelab.SaveData;
using Il2CppSLZ.Marrow.SceneStreaming;
using WeatherElectric.SplashText.Menu;

namespace WeatherElectric.SplashText;

public class Main : MelonMod
{
    internal const string Name = "SplashText";
    internal const string Description = "Adds splash text to Void G114's menu.";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "2.0.0";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/SoulWithMae/SplashText/";

    internal static Save SaveData;

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        BoneMenu.Setup();
        UserData.Setup();

        SaveData = DataManager.ActiveSave;
        
        Hooking.OnUIRigCreated += OnUIRigCreated;
    }

    private static void OnUIRigCreated()
    {
        if (SceneStreamer.Session.Level.Barcode.ID != "fa534c5a868247138f50c62e424c4144.Level.VoidG114") return;
        TextManager.Start();
    }
    
    
}