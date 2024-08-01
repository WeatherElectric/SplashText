using BoneLib.BoneMenu;

namespace WeatherElectric.SplashText.Menu;

internal static class BoneMenu
{
    public static void Setup()
    {
        Page mainCat = Page.Root.CreatePage("<color=#6FBDFF>Weather Electric</color>", Color.white);
        Page subCat = mainCat.CreatePage("Splash Text", Color.yellow);
        subCat.CreateEnum("Splash Mode", Color.white, Preferences.SplashMode.Value, v =>
        {
            Preferences.SplashMode.Value = (SplashMode)v;
            Preferences.OwnCategory.SaveToFile(false);
            TextManager.SetText();
        });
#if DEBUG
        subCat.CreateFunction("Reroll", Color.white, TextManager.SetText);
#endif
    }
}