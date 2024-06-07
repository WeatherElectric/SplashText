using Il2CppTMPro;
using UnityEngine.SceneManagement;
using WeatherElectric.SplashText.Scripts.Helpers;
using WeatherElectric.SplashText.Scripts.Lists;
using Object = UnityEngine.Object;

namespace WeatherElectric.SplashText.Scripts;

internal static class TextManager
{
    private static GameObject _splashTextHost;
    private static TextMeshPro _textMeshPro;
    
    public static void Start()
    {
        CreateGameObject();
        SetGameObjectPosition();
        SetText();
    }
    
    private static void CreateGameObject()
    {
        GameObject uiRoot = FuckYouSLZ("//-----UI");
        ModConsole.Msg($"Found UI root: {uiRoot.name}", 1);
        Transform canvasRoot = uiRoot.transform.Find("CANVAS_UX");
        ModConsole.Msg("Found canvas root", 1);
        Transform menuRoot = canvasRoot.Find("MENU");
        ModConsole.Msg("Found menu root", 1);
        GameObject buildInfoObj = menuRoot.Find("txt_buildInfo").gameObject;
        ModConsole.Msg("Found build info object", 1);
        _splashTextHost = Object.Instantiate(buildInfoObj, menuRoot.transform);
        ModConsole.Msg("Created splash text host", 1);
        return;

        // ReSharper disable once InconsistentNaming
        // SLZ tends to put // in their gameobject names, which fucks up GameObject.Find
        GameObject FuckYouSLZ(string name)
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = scene.GetRootGameObjects();
            return rootObjects.FirstOrDefault(rootObject => rootObject.name == name);
        }
    }

    private static void SetGameObjectPosition()
    {
        if (_splashTextHost == null) return;
        RectTransform splashTextRect = _splashTextHost.GetComponent<RectTransform>();
        ModConsole.Msg("Got splash text rect", 1);
        splashTextRect.position = new Vector3(28.1982f, 2.1303f, -3.7628f);
        ModConsole.Msg("Set splash text position", 1);
        splashTextRect.rotation = Quaternion.Euler(0, -180, 30);
        ModConsole.Msg("Set splash text rotation", 1);
        splashTextRect.localScale = new Vector3(10f, 10f, 10f);
        ModConsole.Msg("Set splash text scale", 1);
        _splashTextHost.name = "SplashTextHost";
        ModConsole.Msg("Renamed splash text host", 1);
        _textMeshPro = _splashTextHost.GetComponent<TextMeshPro>();
        _textMeshPro.color = Color.yellow;
    }

    public static void SetText()
    {
        if (_textMeshPro == null) return;

        switch (Preferences.SplashMode.Value)
        {
            case SplashMode.Minecraft:
                _textMeshPro.text = MinecraftSplashes.GetRandomSplash();
                break;
            case SplashMode.UserEntries:
                _textMeshPro.text = EntryHelper.GetRandomEntry();
                break;
            case SplashMode.Bonelab:
                SetBonelabSplash();
                break;
            case SplashMode.Terraria:
                _textMeshPro.text = TerrariaSplashes.GetRandomSplash();
                break;
            default:
                ModConsole.Error("Invalid splash mode! Defaulting to BONELAB.");
                SetBonelabSplash();
                break;
        }
    }

    private static void SetBonelabSplash()
    {
        if (Preferences.OfflineMode.Value)
        {
            _textMeshPro.text = BonelabSplashes.GetRandomOfflineSplash();
            return;
        }
        BonelabSplashes.GetRandomOnlineSplash(Boobs);

        return;

        void Boobs(string text)
        {
            _textMeshPro.text = text;
        }
    }
}