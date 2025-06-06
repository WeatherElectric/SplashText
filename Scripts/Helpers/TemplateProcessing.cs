﻿namespace WeatherElectric.SplashText.Scripts.Helpers;

public static class TemplateProcessing
{
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