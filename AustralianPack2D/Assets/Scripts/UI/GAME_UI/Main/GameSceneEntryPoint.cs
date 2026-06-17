using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private List<Sprite> spritesGameCards;
    [SerializeField] private UIGameRoot gameRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private VideoPresenter videoPresenter;

    private PlayToolbarPresenter playToolbarPresenter;

    private StoreLevelPresenter storeLevelPresenter;
    private StoreGameCardsPresenter storeGameCardsPresenter;
    private CardsGameSpawnerPresenter cardsGameSpawnerPresenter;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(gameRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC),
                    viewContainer.GetView<SoundView>());

        videoPresenter = new VideoPresenter(new VideoModel(), viewContainer.GetView<VideoView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        
        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        playToolbarPresenter = new PlayToolbarPresenter(new PlayToolbarModel(), viewContainer.GetView<PlayToolbarView>());

        storeLevelPresenter = new StoreLevelPresenter(new StoreLevelModel(PlayerPrefsKeys.LEVEL_NUMBER));
        storeGameCardsPresenter = new StoreGameCardsPresenter(new StoreGameCardsModel(spritesGameCards));
        cardsGameSpawnerPresenter = new CardsGameSpawnerPresenter(viewContainer.GetView<CardsGameSpawnerView>());

        stateMachine = new StateMachine_Game(storeLevelPresenter, storeGameCardsPresenter, cardsGameSpawnerPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        videoPresenter.Initialize();

        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        playToolbarPresenter.Initialize();

        storeLevelPresenter.Initialize();
        
        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {
        playToolbarPresenter.OnClickToExit += HandleClickToMenu;
        playToolbarPresenter.OnClickToRestart += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        playToolbarPresenter.OnClickToExit -= HandleClickToMenu;
        playToolbarPresenter.OnClickToRestart -= HandleClickToGame;
    }

    private void Deactivate()
    {
        playToolbarPresenter.HideToolbar();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        Deactivate();

        DeactivateEvents();

        bankPresenter?.Dispose();
        videoPresenter?.Dispose();

        playToolbarPresenter?.Dispose();

        storeLevelPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void OnApplicationQuit()
    {
        Dispose();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            bankPresenter?.Save();
        }
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            bankPresenter?.Save();
        }
    }

    #region Output


    public event Action OnClickToMenu;
    public event Action OnClickToGame;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
