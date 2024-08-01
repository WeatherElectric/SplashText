namespace WeatherElectric.SplashText.Scripts.Helpers;

internal static class EntryHelper
{
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(UserData.EntriesPath);
        var r = rnd.Next(lines.Length);
        var randomSplash = lines[r];
        
        if (randomSplash.Contains("[UserName]"))
        {
            randomSplash = randomSplash.Replace("[UserName]", Environment.UserName);
        }
        
        if (randomSplash.Contains("[PalletCount]"))
        {
            randomSplash = randomSplash.Replace("[PalletCount]", AssetWarehouse.Instance.GetPallets().Count.ToString());
        }

        if (randomSplash.Contains("[CurrentAvatar]"))
        {
            var crateRef = new AvatarCrateReference(Main.SaveData.PlayerSettings.CurrentAvatar);
            randomSplash = randomSplash.Replace("[CurrentAvatar]", crateRef.Crate.Title);
        }

        if (randomSplash.Contains("[Height"))
        {
            var height = Main.SaveData.PlayerSettings.PlayerHeight;
            var feet = (int)height;
            var inches = height - feet;
            randomSplash = randomSplash.Replace("[Height]", $"{feet}'{inches}\"");
        }

        // It's gonna say this has errors: it does not. it builds fine, il2cpp just sucks
        if (randomSplash.Contains("[RandomFavoriteSpawnable]"))
        {
            var spawnable = Main.SaveData.PlayerSettings.FavoriteSpawnables[(Index)rnd.Next(Main.SaveData.PlayerSettings.FavoriteSpawnables.Count)];
            var crateRef = new SpawnableCrateReference((Barcode)spawnable);
            randomSplash = randomSplash.Replace("[RandomFavoriteSpawnable]", crateRef.Crate.Title);
        }
        
        if (randomSplash.Contains("[RandomFavoriteAvatar]"))
        {
            
            var avatar = Main.SaveData.PlayerSettings.FavoriteAvatars[(Index)rnd.Next(Main.SaveData.PlayerSettings.FavoriteAvatars.Count)];
            var crateRef = new AvatarCrateReference((Barcode)avatar);
            randomSplash = randomSplash.Replace("[RandomFavoriteAvatar]", crateRef.Crate.Title);
        }
        
        return randomSplash;
    }
}