using HarmonyLib;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow.SceneStreaming;

namespace WeatherElectric.SplashText;

[HarmonyPatch]
public static class Patching
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Player_Health.MakeVignette))]
    public static void Postfix(Player_Health __instance)
    {
        if (SceneStreamer.Session.Level.Barcode != "fa534c5a868247138f50c62e424c4144.Level.VoidG114") return;
        ModConsole.Msg("Void G114 loaded, creating splash text host", 1);
        TextManager.Start();
            
#if DEBUG
        var testText1 = "[UserName] exists";
        var testText2 = "[PalletCount] pallets";
        var testText3 = "[CurrentAvatar] is your current avatar";
        testText1 = testText1.Replace("[UserName]", Environment.UserName);
        testText2 = testText2.Replace("[PalletCount]", AssetWarehouse.Instance.GetPallets().Count.ToString());
        var crateRef = new AvatarCrateReference(Main.SaveData.PlayerSettings.CurrentAvatar);
        testText3 = testText3.Replace("[CurrentAvatar]", crateRef.Crate.Title);
        ModConsole.Msg($"Test text 1: {testText1}", 1);
        ModConsole.Msg($"Test text 2: {testText2}", 1);
        ModConsole.Msg($"Test text 3: {testText3}", 1);
#endif
    }
}