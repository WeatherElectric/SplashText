using SLZ.Marrow.Warehouse;

namespace WeatherElectric.SplashText.Scripts.Helpers;

internal static class EntryHelper
{
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(UserData.EntriesPath);
        var r = rnd.Next(lines.Length);
        var randomSplash = lines[r];
        
        if (randomSplash.Contains("{UserName}"))
        {
            randomSplash = randomSplash.Replace("{UserName}", Environment.UserName);
        }
        
        if (randomSplash.Contains("{PalletCount}"))
        {
            randomSplash = randomSplash.Replace("{PalletCount}", AssetWarehouse.Instance.GetPallets().Count.ToString());
        }

        if (randomSplash.Contains("{CurrentAvatar}"))
        {
            randomSplash = randomSplash.Replace("{CurrentAvatar}", Player.rigManager.AvatarCrate.Crate.Title);
        }
        
        return randomSplash;
    }
}