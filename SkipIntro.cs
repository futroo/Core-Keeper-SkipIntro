using PugMod;
using UnityEngine;
using HarmonyLib;
using System.Linq;
using System.Collections;

[Harmony]
public class SkipIntro : IMod
{
    public const string VERSION = "1.0.0";
    public const string NAME = "SkipIntro";
    public const string AUTHOR = "Futroo";

    private LoadedMod modInfo;

    public void EarlyInit()
    {
        Debug.Log($"[{NAME}]: Version: {VERSION}");
        modInfo = API.ModLoader.LoadedMods.FirstOrDefault(obj => obj.Handlers.Contains(this));
        if (modInfo == null)
        {
            Debug.Log($"[{NAME}]: Failed to load {NAME}!");
            return;
        }
        Debug.Log($"[{NAME}]: Mod loaded successfully!");
    }

    public void Init()
    {

    }

    public void ModObjectLoaded(Object obj)
    {

    }

    public void Shutdown()
    {

    }

    void IMod.Update()
    {

    }

    static IEnumerator NewStartingCoroutine(LoadingScene __instance)
    {
        Manager.InitializeGlobalManager();
        __instance.sceneHandler.gameObject.SetActive(true);
        yield return Yielders.WaitForEndOfFrame();
        Manager.load.QueueScene("Title", 1f, 1f, FadePresets.cut, false, 0);
        yield break;
    }

    #region Harmony
    [HarmonyPrefix, HarmonyPatch(typeof(LoadingScene), "Start")]
    public static bool LoadingSceneStartPrefix(LoadingScene __instance)
    {
        __instance.StartCoroutine(NewStartingCoroutine(__instance));
        return false;
    }
    #endregion Harmony
}
