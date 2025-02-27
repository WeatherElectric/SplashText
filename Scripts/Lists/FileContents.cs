namespace WeatherElectric.SplashText.Scripts.Lists;

public static class FileContents
{
    private static readonly string[] Texts =
    [
        "fish",
        "PREPARE THYSELF",
        "JUDGEMENT!",
        
        #region V1
        
        "WARNING: EXTREME DAMAGE SUSTAINED." +
        "RUNNING DIAGNOSTIC" +
        "ERROR: ARM CORE MODULE #1 NOT RESPONDING" +
        "ERROR: ARM CORE MODULE #2 NOT RESPONDING" +
        "WARNING: COMBAT SYSTEMS INOPERABLE" +
        "ATTEMTPING RECONSTRUCTION" +
        "ERROR: SELF-REPAIR NEXUS NOT RESPONDING" +
        "INSUFFICIENT BLOOD" +
        "INSUFFICIENT BLOOD" +
        "INITIATING ESCAPE PROTOCOL" +
        "ATTEMTPING CONNECTION WITH LIMBIC MODULES" +
        "ERROR: LEG CORE MODULE #1 NOT RESPONDING" +
        "ERROR: LEG CORE MODULE #2 NOT RESPONDING" +
        "WARNING: UNABLE TO SUSTAIN MOTOR FUNCTIONS" +
        "ERROR: VISUAL CORTEX MALFUNCTION" +
        "ERROR: LIMBIC FUNCTION NOT RESPONDING" +
        "INSUFFICIENT BLOOD" +
        "INSUFFICIENT BLOOD" +
        "WARNING: UNABLE TO SUSTAIN INTERNAL ORGANS" +
        "! PULSE FAILURE !" +
        "! PULSE FAILURE !" +
        "! PULSE FAILURE !" +
        "-!- SHUTDOWN IMMINENT -!-" +
        "ERROR: NO VOCAL INTERFACE DETECTED. UNABLE TO COMPLETE TASK" +
        "! PULSE FAILURE !" +
        "! PULSE FAILURE !" +
        "INSUFFICIENT BLOOD" +
        "INSUFFICIENT BLOOD" +
        "WARNING: UNABLE TO SUSTAIN BASIC FUNCTIONS" +
        "-!- SHUTDOWN IMMINENT -!-" +
        "-!- SHUTDOWN IMMINENT -!-" +
        "I DON'T WANT TO DIE." +
        "I DON'T WANT TO DIE." +
        "I DON'T WANT TO DIE." +
        "I DON'T WANT T",
        
        #endregion
        
        "hi :3",
        "hi [UserName]",
        "[CPU]",
        "hey man, nice shot!",
        "no not now",
        "maybe later",
        "it's the only way to live, in cars",
        "oh no, not me, i never lost control",
        "always on about the day it should have flown",
        "you can't stake your lives on a savior machine",
        "go play titanfall 2",
        "we should kill elon musk", // COPILOT WROTE THIS??????
        "yt-dlp is better than youtube-dl",
        "kill john lennon",
        "the grabbing hands grab all they can",
        "blank stare, disrepair, there's a big black hole gonna eat me up someday",
        "and when you return to the place that you call home, we will be there. we will be there.",
        "nico you're blue",
        "[FreeDiskSpace] out of [TotalDiskSpace], and this file is taking up just a LITTLE bit more :3",
        "you know what that means, FISH!",
        "[RandomFavoriteSpawnable] stinks",
        "hi [CurrentAvatar]",
        "my name is david, dad, i want some ice cream, david, that is my name, david, i want another, david, where is my ball? i'm running out on the road, there is a car, and it is going to hit me",
        "gomer",
        "d'oh",
        "bitch",
        "jake get a job, jake get a job, jake get a job, a job jake get",
        "go play littlebigplanet 2",
        "A small tit arrives, Jesus is within us all. Crap, I can't bottle.",
        "rip scrieel scrieq squirel rip rrelq quis squer reuq rille skin skin squiriel",
        "nba a afas"
    ];

    public static string GetRandomText()
    {
        var rnd = new System.Random();
        var randomSplash = Texts[rnd.Next(Texts.Length)];
        TemplateProcessing.Process(randomSplash, true);
        return randomSplash;
    }
}