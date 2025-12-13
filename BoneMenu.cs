using Menu = WeatherElectric.BONELAB.BoneMenu.Menu;

namespace WeatherElectric.SplashText;

internal static class BoneMenu
{
    public static void Init()
    {
        var subCat = Menu.MainPage.CreatePage("Splash Text", Color.yellow);
        subCat.CreateEnum("Splash Mode", Color.white, Preferences.SplashMode.Value, v =>
        {
            Preferences.SplashMode.Value = (SplashMode)v;
            Preferences.OwnCategory.SaveToFile(false);
            TextManager.SetText();
        });
    }
}