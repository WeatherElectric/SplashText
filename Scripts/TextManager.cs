using Il2CppTMPro;
using UnityEngine.SceneManagement;
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
        var uiRoot = FuckYouSLZ("//-----UI");
        Main.Logger.Log($"Found UI root: {uiRoot.name}", LogLevel.Debug);
        var canvasRoot = uiRoot.transform.Find("CANVAS_UX");
        Main.Logger.Log("Found canvas root", LogLevel.Debug);
        var menuRoot = canvasRoot.Find("MENU");
        Main.Logger.Log("Found menu root", LogLevel.Debug);
        var buildInfoObj = menuRoot.Find("txt_buildInfo").gameObject;
        Main.Logger.Log("Found build info object", LogLevel.Debug);
        _splashTextHost = Object.Instantiate(buildInfoObj, menuRoot.transform);
        Main.Logger.Log("Created splash text host", LogLevel.Debug);
        return;

        // ReSharper disable once InconsistentNaming
        // SLZ tends to put // in their gameobject names, which fucks up GameObject.Find
        GameObject FuckYouSLZ(string name)
        {
            var scene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = scene.GetRootGameObjects();
            return rootObjects.FirstOrDefault(rootObject => rootObject.name == name);
        }
    }

    private static void SetGameObjectPosition()
    {
        if (_splashTextHost == null) return;
        var splashTextRect = _splashTextHost.GetComponent<RectTransform>();
        Main.Logger.Log("Got splash text rect", LogLevel.Debug);
        splashTextRect.position = new Vector3(28.1982f, 2.1303f, -3.7628f);
        Main.Logger.Log("Set splash text position", LogLevel.Debug);
        splashTextRect.rotation = Quaternion.Euler(0, -180, 30);
        Main.Logger.Log("Set splash text rotation", LogLevel.Debug);
        splashTextRect.localScale = new Vector3(10f, 10f, 10f);
        Main.Logger.Log("Set splash text scale", LogLevel.Debug);
        _splashTextHost.name = "SplashTextHost";
        Main.Logger.Log("Renamed splash text host", LogLevel.Debug);
        _textMeshPro = _splashTextHost.GetComponent<TextMeshPro>();
        _textMeshPro.color = Color.yellow;
    }

    public static void SetText()
    {
        if (!_textMeshPro) return;

        switch (Preferences.SplashMode.Value)
        {
            case SplashMode.Minecraft:
                _textMeshPro.text = MinecraftSplashes.GetRandomSplash();
                break;
            case SplashMode.UserEntries:
                _textMeshPro.text = UserSplashes.GetRandomEntry();
                break;
            case SplashMode.Bonelab:
                SetBonelabSplash();
                break;
            case SplashMode.Terraria:
                _textMeshPro.text = TerrariaSplashes.GetRandomSplash();
                break;
            default:
                Main.Logger.Log("Invalid splash mode! Defaulting to BONELAB.", LogLevel.Error);
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
        BonelabSplashes.GetRandomOnlineSplash(TMPCallback);

        return;

        void TMPCallback(string text)
        {
            _textMeshPro.text = text;
        }
    }
}