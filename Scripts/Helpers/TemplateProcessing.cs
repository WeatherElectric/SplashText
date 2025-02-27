namespace WeatherElectric.SplashText.Scripts.Helpers;

public static class TemplateProcessing
{
    private static readonly List<string> UserPicturesFilenames = [];
    private static readonly List<string> UserDocumentsFilenames = [];
    private static readonly List<string> UserDownloadsFilenames = [];
    private static readonly List<string> UserDesktopFilenames = [];
    private static readonly List<string> SteamGames = [];

    internal static void CacheSteamGames()
    {
        if (!HelperMethods.IsAndroid())
        {
            var steamFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Steam\steamapps\common";
            var folders = Directory.GetDirectories(steamFolder).ToList();
            foreach (var folder in folders)
            {
                var cleanFolderName = folder.Replace(steamFolder + "\\", "");
                SteamGames.Add(cleanFolderName);
            }
        }
        SteamGames.Add("(You're on a Quest, so I can't get your Steam games. Womp.)");
    }
    
    internal static void CacheUserFiles()
    {
        // pictures
        var picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        var files = Directory.GetFiles(picturesFolder).ToList();
        foreach (var file in files)
        {
            UserPicturesFilenames.Add(Path.GetFileName(file));
        }
        
        // documents
        var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        files = Directory.GetFiles(documentsFolder).ToList();
        foreach (var file in files)
        {
            UserDocumentsFilenames.Add(Path.GetFileName(file));
        }
        
        // downloads
        var downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        files = Directory.GetFiles(downloadsFolder).ToList();
        foreach (var file in files)
        {
            UserDownloadsFilenames.Add(Path.GetFileName(file));
        }
        var folders = Directory.GetDirectories(downloadsFolder).ToList();
        foreach (var folder in folders)
        {
            var cleanFolderName = folder.Replace(downloadsFolder + "\\", "");
            UserDownloadsFilenames.Add(cleanFolderName);
        }
        
        // desktop
        var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        files = Directory.GetFiles(desktopFolder).ToList();
        foreach (var file in files)
        {
            UserDesktopFilenames.Add(Path.GetFileName(file));
        }
    }
    
    private static readonly Dictionary<string, Func<string>> Replacements = new()
    {
        { "[UserName]", () => HelperMethods.IsAndroid() ? "Quest User" : Environment.UserName },
        { "[PalletCount]", () => AssetWarehouse.Instance.GetPallets().Count.ToString() },
        { "[CurrentAvatar]", () => Player.RigManager._avatarCrate.Crate.Title },
        { "[Height]", () => 
            {
                var height = Main.SaveData.PlayerSettings.PlayerHeight;
                var totalInches = height * 0.393701;
                var feet = (int)(totalInches / 12);
                var inches = (int)Math.Round(totalInches % 12);
                return $"{feet}'{inches}\"";
            }
        },
        { "[RandomFavoriteSpawnable]", () =>
            {
                var rnd = new System.Random();
                var spawnable = Main.SaveData.PlayerSettings.FavoriteSpawnables[(Index)rnd.Next(Main.SaveData.PlayerSettings.FavoriteSpawnables.Count)];
                var barcode = new Barcode { ID = spawnable.ToString() };
                var crateRef = new SpawnableCrateReference(barcode);
                return crateRef.Crate.Title;
            }
        },
        { "[RandomFavoriteAvatar]", () =>
            {
                var rnd = new System.Random();
                var avatar = Main.SaveData.PlayerSettings.FavoriteAvatars[(Index)rnd.Next(Main.SaveData.PlayerSettings.FavoriteAvatars.Count)];
                var barcode = new Barcode { ID = avatar.ToString() };
                var crateRef = new AvatarCrateReference(barcode);
                return crateRef.Crate.Title;
            }
        },
        { "[CPU]", () => SystemInfo.processorType },
        { "[GPU]", () => SystemInfo.graphicsDeviceName },
        { "[GPUVendor]", () => SystemInfo.graphicsDeviceVendor },
        { "[RAM]", () =>
            {
                var memoryInMb = SystemInfo.systemMemorySize;
                var memoryInGb = memoryInMb / 1024.0;
                return Math.Round(memoryInGb) + " GB";
            } 
        },
        { "[OS]", () => SystemInfo.operatingSystem },
        { "[MachineName]", () => HelperMethods.IsAndroid() ? "Oculus Quest" : Environment.MachineName },
        { "[TotalDiskSpace]", () =>
            {
                var drive = new DriveInfo("C");
                return drive.TotalSize / 1024 / 1024 / 1024 + " GB";
            }
        },
        { "[FreeDiskSpace]", () =>
            {
                var drive = new DriveInfo("C");
                return drive.AvailableFreeSpace / 1024 / 1024 / 1024 + " GB";
            }
        },
        { "[RandomUserPicture]", () =>
            {
                var rnd = new System.Random();
                return UserPicturesFilenames[rnd.Next(UserPicturesFilenames.Count)];
            }
        },
        { "[RandomUserDocument]", () =>
            {
                var rnd = new System.Random();
                return UserDocumentsFilenames[rnd.Next(UserDocumentsFilenames.Count)];
            }
        },
        { "[RandomUserDownloadsFile]", () =>
            {
                var rnd = new System.Random();
                return UserDownloadsFilenames[rnd.Next(UserDownloadsFilenames.Count)];
            }
        },
        { "[RandomUserDesktopFile]", () =>
            {
                var rnd = new System.Random();
                return UserDesktopFilenames[rnd.Next(UserDesktopFilenames.Count)];
            }
        },
        { "[RandomSteamGame]", () =>
            {
                var rnd = new System.Random();
                return SteamGames[rnd.Next(SteamGames.Count)];
            }
        }
    };
    
    private static readonly Dictionary<string, Func<string>> Actions = new()
    {
        { "[PlaceTxtFile]", () =>
            {
                var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var txtFile = Path.Combine(userFolder, "gift.txt");
                File.WriteAllText(txtFile, FileContents.GetRandomText());
                return "";
            } 
        },
        { "[SpawnNullbody]", () =>
            {
                var playerPos = Player.RemapRig.transform.position;
                var spawnLocation = playerPos + Player.RemapRig.transform.forward * 2;
                HelperMethods.SpawnCrate(CommonBarcodes.NPCs.Nullbody, spawnLocation, Quaternion.identity, Vector3.one);
                return "";
            }
        },
        { "[PeterGriffin]", () =>
            {
                Application.OpenURL("https://familyguy.fandom.com/wiki/Peter_Griffin");
                return "";
            }
        }
    };
    
    public static void AddTemplate(string key, Func<string> value)
    {
        Replacements.Add(key, value);
    }
    
    public static string Process(string text, bool skipActions = false)
    {
        foreach (var replacement in Replacements)
        {
            if (text.Contains(replacement.Key))
            {
                text = text.Replace(replacement.Key, replacement.Value());
            }
        }
        
        if (skipActions) return text;
        
        foreach (var action in Actions)
        {
            if (text.Contains(action.Key))
            {
                text = text.Replace(action.Key, action.Value());
            }
        }
        
        return text;
    }
}