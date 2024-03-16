using HarmonyLib;
using SLZ.Marrow.SceneStreaming;
using SLZ.SaveData;

namespace WeatherElectric.SplashText;

public class Main : MelonMod
{
    internal const string Name = "SplashText";
    internal const string Description = "Adds splash text to Void G114's menu.";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "1.1.2";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/SoulWithMae/SplashText/";

    public static Save SaveData;
    
    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        BoneMenu.Setup();
        UserData.Setup();
        
        SaveData = DataManager.Instance._activeSave;
    }
    
    [HarmonyPatch(typeof(Player_Health), "MakeVignette")]
    public class PlayerHealthPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedParameter.Global
        public static void Postfix(Player_Health __instance)
        {
            if (SceneStreamer.Session.Level.Barcode != CommonBarcodes.Maps.VoidG114) return;
            ModConsole.Msg("Void G114 loaded, creating splash text host", 1);
            TextManager.Start();
            
#if DEBUG
            var testText1 = "[UserName] exists";
            var testText2 = "[PalletCount] pallets";
            var testText3 = "[CurrentAvatar] is your current avatar";
            testText1 = testText1.Replace("[UserName]", Environment.UserName);
            testText2 = testText2.Replace("[PalletCount]", AssetWarehouse.Instance.GetPallets().Count.ToString());
            var crateRef = new AvatarCrateReference(SaveData.PlayerSettings.CurrentAvatar);
            testText3 = testText3.Replace("[CurrentAvatar]", crateRef.Crate.Title);
            ModConsole.Msg($"Test text 1: {testText1}", 1);
            ModConsole.Msg($"Test text 2: {testText2}", 1);
            ModConsole.Msg($"Test text 3: {testText3}", 1);
#endif
        }
    }
}