namespace WeatherElectric.SplashText.Scripts.Helpers;

internal static class EntryHelper
{
    public static string GetRandomEntry()
    {
        var rnd = new System.Random();
        var lines = File.ReadAllLines(UserData.EntriesPath);
        if (lines.Length == 0)
        {
            ModConsole.Error("No entries found in UserEntries.txt. Defaulting to BonelabSplashes.");
            return BonelabSplashes.GetRandomOfflineSplash();
        }
        var r = rnd.Next(lines.Length);
        var randomSplash = lines[r];
        
        randomSplash = ProcessTemplates(randomSplash);

        return randomSplash;
    }
    
    internal static string ProcessTemplates(string randomSplash)
    {
        var rnd = new System.Random();
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
            randomSplash = randomSplash.Replace("[CurrentAvatar]", Player.RigManager._avatarCrate.Crate.Title);
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
            var barcode = new Barcode
            {
                ID = spawnable.ToString()
            };
            var crateRef = new SpawnableCrateReference(barcode);
            randomSplash = randomSplash.Replace("[RandomFavoriteSpawnable]", crateRef.Crate.Title);
        }

        if (!randomSplash.Contains("[RandomFavoriteAvatar]")) return randomSplash;
        {
            var avatar = Main.SaveData.PlayerSettings.FavoriteAvatars[(Index)rnd.Next(Main.SaveData.PlayerSettings.FavoriteAvatars.Count)];
            var barcode = new Barcode
            {
                ID = avatar.ToString()
            };
            var crateRef = new AvatarCrateReference(barcode);
            randomSplash = randomSplash.Replace("[RandomFavoriteAvatar]", crateRef.Crate.Title);
        }

        return randomSplash;
    }
}