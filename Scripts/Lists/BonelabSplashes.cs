using System.Collections;
using UnityEngine.Networking;

namespace WeatherElectric.SplashText.Scripts.Lists;

public static class BonelabSplashes
{
    private static readonly string[] Splashes = 
    {
        "Into the void with you!",
        "What up, son!",
        "Thursday, Yes, This Thursday!",
        "how i get spiderlab?",
        "bring back bonetome",
        "zonelab",
        "cam PLEAS give me the mono build",
        "why'd they make this game il2cpp",
        "Sadly On Quest!",
        "Also try Boneworks!",
        "Also try Duck Season!",
        "Also try Hover Junkers!",
        "Also try Half Life Alyx!",
        "Also try Nervbox!",
        "the blankbox entity is here",
        "rigmanager's null lol",
        "Fuck you Rican!",
        "today i will sync physics; duplicalte camera",
        "The new source of bodycam footage for edgy kids!",
        "I bet my life it'll be Thursday!",
        "evil brandon be like: i WILL release a update on thursday",
        "cant wait for project 4!",
        "MissingMethodException: Default constructor not found for type UnityEngine.Video.VideoPlayer",
        "TargetException: Instance constructor requires a target",
        "il2cpp compiler removing all useful components",
        "Invalid pallet.json!",
        "There you go!",
        "Pick it up!",
        "Put it down!",
        "Don't fence me in!",
        "Stuck inside this desert hell!",
        "Dogs are gonna get what you hold dear!",
        "Nulla Molles Accentus",
        "Faciem Coegi Vos",
        "I'm feelin so strange",
        "Ima Say Ma Namowa!",
        "Never enough photons.",
        "NEP.Paranoia.Scripts.Managers.ParanoiaManager!",
        "breadsoup's cooking \ud83d\udd25",
        "6 hour buffer fucking SUCKS",
        "i'm gonna put 9 realtime lights in the scene, suffer",
        "Only {PalletCount} mods installed? smh",
        "{CurrentAvatar}? what a lame avatar",
        "oh cool, {CurrentAvatar}, thats a good avatar",
        "lol [Height]",
        "you really use [RandomFavoriteSpawnable]?",
        "you liked [RandomFavoriteAvatar] enough to put it in your BODYLOG?"
    };

    private const string SplashAPI = "https://splashtext.weatherelectric.xyz/";

    public delegate void FetchTextCallback(string fetchedText);

    public static void GetRandomOnlineSplash(FetchTextCallback callback)
    {
        MelonCoroutines.Start(FetchText(callback));
    }

    private static IEnumerator FetchText(FetchTextCallback callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(SplashAPI);
        var asyncOperation = request.SendWebRequest();
        
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            var rnd = new System.Random();
            string randomSplash = request.downloadHandler.text;
            
            ModConsole.Msg($"Text recieved: {randomSplash}", 1);
            
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
                int feet = (int)height;
                float inches = height - feet;
                randomSplash = randomSplash.Replace("[Height]", $"{feet}'{inches}\"");
            }

            // It's gonna say this has errors: it does not. it builds fine, il2cpp just sucks
            if (randomSplash.Contains("[RandomFavoriteSpawnable]"))
            {
                var spawnable = Main.SaveData.PlayerSettings.FavoriteSpawnables[rnd.Next(Main.SaveData.PlayerSettings.FavoriteSpawnables.Count)];
                var crateRef = new SpawnableCrateReference(spawnable);
                randomSplash = randomSplash.Replace("[RandomFavoriteSpawnable]", crateRef.Crate.Title);
            }
        
            if (randomSplash.Contains("[RandomFavoriteAvatar]"))
            {
            
                var avatar = Main.SaveData.PlayerSettings.FavoriteAvatars[rnd.Next(Main.SaveData.PlayerSettings.FavoriteAvatars.Count)];
                var crateRef = new AvatarCrateReference(avatar);
                randomSplash = randomSplash.Replace("[RandomFavoriteAvatar]", crateRef.Crate.Title);
            }
            
            callback(randomSplash);
        }
        else
        {
            ModConsole.Error("Failed to fetch random text. Webserver is likely offline. Using backup method.");
            ModConsole.Error($"Webrequest Result: {request.result.ToString()}");
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                ModConsole.Error($"Error: {request.error}");
            }
            callback(GetRandomOfflineSplash());
        }
    }
    
    public static string GetRandomOfflineSplash()
    {
        var rnd = new System.Random();
        var randomSplash = Splashes[rnd.Next(Splashes.Length)];
        
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
            int feet = (int)height;
            float inches = height - feet;
            randomSplash = randomSplash.Replace("[Height]", $"{feet}'{inches}\"");
        }

        // It's gonna say this has errors: it does not. it builds fine, il2cpp just sucks
        if (randomSplash.Contains("[RandomFavoriteSpawnable]"))
        {
            var spawnable = Main.SaveData.PlayerSettings.FavoriteSpawnables[rnd.Next(Main.SaveData.PlayerSettings.FavoriteSpawnables.Count)];
            var crateRef = new SpawnableCrateReference(spawnable);
            randomSplash = randomSplash.Replace("[RandomFavoriteSpawnable]", crateRef.Crate.Title);
        }
        
        if (randomSplash.Contains("[RandomFavoriteAvatar]"))
        {
            
            var avatar = Main.SaveData.PlayerSettings.FavoriteAvatars[rnd.Next(Main.SaveData.PlayerSettings.FavoriteAvatars.Count)];
            var crateRef = new AvatarCrateReference(avatar);
            randomSplash = randomSplash.Replace("[RandomFavoriteAvatar]", crateRef.Crate.Title);
        }
        
        return randomSplash;
    }
}