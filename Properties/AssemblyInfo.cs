using System.Reflection;

[assembly: AssemblyTitle(WeatherElectric.SplashText.Main.Description)]
[assembly: AssemblyDescription(WeatherElectric.SplashText.Main.Description)]
[assembly: AssemblyCompany(WeatherElectric.SplashText.Main.Company)]
[assembly: AssemblyProduct(WeatherElectric.SplashText.Main.Name)]
[assembly: AssemblyCopyright("Developed by " + WeatherElectric.SplashText.Main.Author)]
[assembly: AssemblyTrademark(WeatherElectric.SplashText.Main.Company)]
[assembly: AssemblyVersion(WeatherElectric.SplashText.Main.Version)]
[assembly: AssemblyFileVersion(WeatherElectric.SplashText.Main.Version)]
[assembly:
    MelonInfo(typeof(WeatherElectric.SplashText.Main), WeatherElectric.SplashText.Main.Name, WeatherElectric.SplashText.Main.Version,
        WeatherElectric.SplashText.Main.Author, WeatherElectric.SplashText.Main.DownloadLink)]
[assembly: MelonColor(255, 255, 198, 0)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]