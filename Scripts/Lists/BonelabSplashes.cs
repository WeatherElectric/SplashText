using System.Collections;
using UnityEngine.Networking;

namespace WeatherElectric.SplashText.Scripts.Lists;

public static class BonelabSplashes
{
    internal static readonly string[] Splashes =
    [
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
        "Only [PalletCount] mods installed? smh",
        "[CurrentAvatar]? what a lame avatar",
        "oh cool, [CurrentAvatar], thats a good avatar",
        "lol [Height]",
        "you really use [RandomFavoriteSpawnable]?",
        "you liked [RandomFavoriteAvatar] enough to put it in your BODYLOG?",
        "you csnt act like I dont innovare bonelab",
        "there's a nullbody behind you btw",
        "Now on Nintendo Labo!",
        "Use mod-help instead of mod-general you dinguses",
        "pysics sink",
        "hop on entanglement",
        "use UABE to install mods!",
        "the grand migration to melonloader 0.2",
        "C:/Users/[UserName]/AppData/LocalLow/Stress Level Zero/BONELAB/Mods!",
        "Patch 7?",
        "bring back bw chaos",
        "oregano's texture streaming hell!",
        "hi adam!",
        "hi cam!",
        "hi [UserName]!",
        "with the man who sold the world",
        "fuck you, i'm putting mesh colliders on everything. convex? no, CONCAVE!",
        "BL unity upgrade to 2022 when",
        "I WILL NEVER PACK FOR QUEST!",
        "damn shader variants",
        "LitMAS Standard!",
        "RIP depth texture (on quest lol)",
        "RIP constant force (on quest lol)",
        "Don't bother with the dungeon warrior secret, its ass",
        "at least it's not togen!",
        "[RandomFavoriteSpawnable] isn't even good.",
        "hey wait, you're not [CurrentAvatar], you're [UserName]!",
        "this is the 73rd line of the splash text list!",
        "im a wizard, watch, you have [RandomFavoriteAvatar] on your bodylog, was i right?",
        "cam's furry comission will be in patch 7!",
        "quest code modding died, rest in piss",
        "hateful of all else!",
        ":3",
        "fuck you, i'm a bonelab splash",
        "fuckin [UserName] is here",
        "do you think steam would take down bonelab's page because the game uses AI in some places",
        "my mom asked me if i did the dishes. i yelled \"StressLevelZero!” and she smiled. she knew it was washed.",
        "the hands will get you.",
        "fuck you [UserName]",
        "SLZ PLEAS make hotmk and b-side for just PC",
        "Quest: A Detriment To VR!",
        "<color=\"yellow\">and when you return to the place that you call home, <color=\"green\">he <color=\"yellow\">will be there, <color=\"green\">he <color=\"yellow\">will be there.",
        "if this game had wallrunning it'd be peak",
        "this mod is loaded by an actually good modloader!",
        "<color=\"green\">Ford",
        "get pink screen of death'd, idiot"
    ];

    private const string SplashAPI = "https://splashtext.weatherelectric.xyz/";

    public delegate void FetchTextCallback(string fetchedText);

    public static void GetRandomOnlineSplash(FetchTextCallback callback)
    {
        MelonCoroutines.Start(FetchText(callback));
    }

    private static IEnumerator FetchText(FetchTextCallback callback)
    {
        var request = UnityWebRequest.Get(SplashAPI);
        var asyncOperation = request.SendWebRequest();
        
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            var randomSplash = request.downloadHandler.text;
            
            ModConsole.Msg($"Text recieved: {randomSplash}", 1);
            
            randomSplash = TemplateProcessing.Process(randomSplash);

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
        
        randomSplash = TemplateProcessing.Process(randomSplash);
        
        return randomSplash;
    }
}