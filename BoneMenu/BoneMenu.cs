using WeatherElectric.SplashText.Scripts;

namespace WeatherElectric.SplashText.Menu;

internal static class BoneMenu
{
    public static void Setup()
    {
        MenuCategory mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCategory subCat = mainCat.CreateCategory("Splash Text", Color.yellow);
        subCat.CreateEnumElement("Splash Mode", Color.white, Preferences.SplashMode.Value, v =>
        {
            Preferences.SplashMode.Value = v;
            Preferences.OwnCategory.SaveToFile(false);
            TextManager.SetText();
        });
    }
}