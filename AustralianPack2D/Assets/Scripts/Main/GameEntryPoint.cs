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

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
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
        coroutines.StartCoroutine(LoadAndStartMenu());
    }


    private IEnumerator LoadSceneAndRun<TSceneEntry>(string scene, bool showLoading = false, Action<TSceneEntry> setup = null)
        where TSceneEntry : MonoBehaviour
    {
        if (showLoading)
        {
            yield return rootView.ShowLoadingScreen(0);
            yield return new WaitForSeconds(0.4f);
        }

        yield return SceneManager.LoadSceneAsync(scene);

        var sceneEntryPoint = Object.FindObjectOfType<TSceneEntry>();
        setup?.Invoke(sceneEntryPoint);

        if (showLoading)
        {
            yield return new WaitForSeconds(0.6f);
            yield return rootView.HideLoadingScreen(0);
        }
    }

    #region Загрузка конкретных сцен

    private IEnumerator LoadAndStartCheck()
    {
        yield return LoadSceneAndRun<CountryCheckerSceneEntryPoint>(Scenes.CHECKER, false, sceneEntry =>
        {
            sceneEntry.GoToGame -= HandleClickToGame;
            sceneEntry.GoToGame += HandleClickToGame;

            sceneEntry.GoToOther -= HandleGoToOther;
            sceneEntry.GoToOther += HandleGoToOther;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartOther()
    {
        yield return LoadSceneAndRun<OtherSceneEntryPoint>(Scenes.OTHER, false, sceneEntry =>
        {
            sceneEntry.OnGoToGame -= HandleClickToGame;
            sceneEntry.OnGoToGame += HandleClickToGame;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartGame()
    {
        yield return LoadSceneAndRun<GameSceneEntryPoint>(Scenes.GAME, false, sceneEntry =>
        {
            sceneEntry.OnClickToGame -= HandleClickToGame;
            sceneEntry.OnClickToGame += HandleClickToGame;

            sceneEntry.Run(rootView);
        });
    }

    private IEnumerator LoadAndStartMenu()
    {
        yield return LoadSceneAndRun<MenuEntryPoint>(Scenes.MAIN_MENU, true, sceneEntry =>
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

