using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;

    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        SetupGlobalSettings();

        instance = new GameEntryPoint();
        instance.Run();
    }

    private static void SetupGlobalSettings()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartCheck());
    }


    private IEnumerator LoadSceneAndRun<TSceneEntry>(string scene, bool showLoading, int screenLoadId, Action<TSceneEntry> setup = null)
        where TSceneEntry : MonoBehaviour
    {
        if (showLoading)
        {
            if (rootView.Index != screenLoadId)
                rootView.ClearSceneUI();

            yield return rootView.ShowLoadingScreen(screenLoadId);
            yield return new WaitForSeconds(0.4f);
        }

        yield return SceneManager.LoadSceneAsync(scene);

        if (showLoading)
        {
            yield return new WaitForSeconds(0.6f);
        }

        var sceneEntryPoint = Object.FindObjectOfType<TSceneEntry>();
        setup?.Invoke(sceneEntryPoint);

        if (showLoading)
        {
            yield return new WaitForSeconds(0.5f);

            yield return rootView.HideLoadingScreen(screenLoadId);
        }
    }

    #region Загрузка конкретных сцен

    private IEnumerator LoadAndStartCheck()
    {
        yield return LoadSceneAndRun<CountryCheckerSceneEntryPoint>(Scenes.CHECKER, false, 0, sceneEntry =>
        {
            sceneEntry.GoToMenu -= HandleClickToMenu;
            sceneEntry.GoToMenu += HandleClickToMenu;

            sceneEntry.GoToOther -= HandleGoToOther;
            sceneEntry.GoToOther += HandleGoToOther;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartOther()
    {
        yield return LoadSceneAndRun<OtherSceneEntryPoint>(Scenes.OTHER, false, 0, sceneEntry =>
        {
            sceneEntry.OnGoToMenu -= HandleClickToMenu;
            sceneEntry.OnGoToMenu += HandleClickToMenu;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartGame()
    {
        yield return LoadSceneAndRun<GameSceneEntryPoint>(Scenes.GAME, true, 1, sceneEntry =>
        {
            sceneEntry.OnClickToGame -= HandleClickToGame;
            sceneEntry.OnClickToGame += HandleClickToGame;

            sceneEntry.OnClickToMenu -= HandleClickToMenu;
            sceneEntry.OnClickToMenu += HandleClickToMenu;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartMenu()
    {
        yield return LoadSceneAndRun<MenuEntryPoint>(Scenes.MAIN_MENU, true, 1, sceneEntry =>
        {
            sceneEntry.OnClickToGame -= HandleClickToGame;
            sceneEntry.OnClickToGame += HandleClickToGame;

            sceneEntry.Run(rootView);
        });
    }

    #endregion

    #region Handlers

    private void HandleGoToOther()
    {
        coroutines.StartCoroutine(LoadAndStartOther());
    }

    private void HandleClickToGame()
    {
        coroutines.StartCoroutine(LoadAndStartGame());
    }

    private void HandleClickToMenu()
    {
        coroutines.StartCoroutine(LoadAndStartMenu());
    }

    #endregion
}

