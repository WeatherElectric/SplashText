using System.Linq;
using HarmonyLib;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;
using TMPro;
using UnityEngine.SceneManagement;
using WeatherElectric.SplashText.Scripts;
using Object = UnityEngine.Object;

namespace WeatherElectric.SplashText;

public class Main : MelonMod
{
    internal const string Name = "SplashText";
    internal const string Description = "Adds splash text to Void G114's menu.";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "0.0.1";
    internal const string DownloadLink = null;
    
    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        BoneMenu.Setup();
        UserData.Setup();
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
        }
    }
}