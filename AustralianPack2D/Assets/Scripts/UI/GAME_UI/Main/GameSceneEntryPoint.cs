using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardPacksSO cardPacksSO;
    [SerializeField] private List<Sprite> spritesGameCards;
    [SerializeField] private UIGameRoot gameRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private VideoPresenter videoPresenter;

    private PlayToolbarPresenter playToolbarPresenter;

    private StoreCardDesignPresenter storeCardDesignPresenter;
    private StoreLevelPresenter storeLevelPresenter;
    private StoreGameCardsPresenter storeGameCardsPresenter;
    private CardsGameSpawnerPresenter cardsGameSpawnerPresenter;
    private CardsOrchestrationPresenter cardsOrchestrationPresenter;
    private GameScorePresenter gameScorePresenter;

    private StoreCardPresenter storeCardPresenter;
    private CardBoxBuyPresenter cardBoxBuyPresenter;
    private CardsBoxPseudoPresenter cardsBoxPseudoPresenter;
    private CardBoxPresenter cardBoxPresenter;
    private CardPresentationPresenter cardPresentationPresenter;
    private CardUniqueCounterPresenter cardUniqueCounterPresenter;
    private GameMoneyGiftPresenter gameMoneyGiftPresenter;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(gameRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC), viewContainer.GetView<SoundView>());

        videoPresenter = new VideoPresenter(new VideoModel(), viewContainer.GetView<VideoView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        
        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        playToolbarPresenter = new PlayToolbarPresenter(new PlayToolbarModel(), viewContainer.GetView<PlayToolbarView>());

        storeCardDesignPresenter = new StoreCardDesignPresenter(new StoreCardDesignModel());
        storeLevelPresenter = new StoreLevelPresenter(new StoreLevelModel(PlayerPrefsKeys.LEVEL_NUMBER));
        storeGameCardsPresenter = new StoreGameCardsPresenter(new StoreGameCardsModel(spritesGameCards));
        cardsGameSpawnerPresenter = new CardsGameSpawnerPresenter(viewContainer.GetView<CardsGameSpawnerView>());
        cardsOrchestrationPresenter = new CardsOrchestrationPresenter(new CardsOrchestrationModel(cardsGameSpawnerPresenter));
        gameScorePresenter = new GameScorePresenter(new GameScoreModel(cardsOrchestrationPresenter, storeLevelPresenter), viewContainer.GetView<GameScoreView>());

        storeCardPresenter = new StoreCardPresenter(new StoreCardModel(cardPacksSO));
        cardBoxBuyPresenter = new CardBoxBuyPresenter(new CardBoxBuyModel(bankPresenter));
        cardsBoxPseudoPresenter = new CardsBoxPseudoPresenter(new CardsBoxPseudoModel(cardBoxBuyPresenter), viewContainer.GetView<CardsBoxPseudoView>());
        cardBoxPresenter = new CardBoxPresenter(new CardBoxModel(cardBoxBuyPresenter), viewContainer.GetView<CardBoxView>());
        cardPresentationPresenter = new CardPresentationPresenter(new CardPresentationModel(cardBoxBuyPresenter, storeCardPresenter, storeCardPresenter), viewContainer.GetView<CardPresentationView>());
        cardUniqueCounterPresenter = new CardUniqueCounterPresenter(new CardUniqueCounterModel(cardPresentationPresenter), viewContainer.GetView<CardUniqueCounterView>());
        gameMoneyGiftPresenter = new GameMoneyGiftPresenter(new GameMoneyGiftModel(bankPresenter), viewContainer.GetView<GameMoneyGiftView>());

        stateMachine = new StateMachine_Game(
            storeLevelPresenter, 
            storeGameCardsPresenter, 
            cardsGameSpawnerPresenter, 
            storeCardDesignPresenter,
            cardsGameSpawnerPresenter,
            sceneRoot,
            cardsOrchestrationPresenter,
            videoPresenter,
            soundPresenter,
            gameScorePresenter,
            gameScorePresenter,
            cardBoxBuyPresenter,
            cardBoxPresenter,
            cardBoxPresenter,
            cardsBoxPseudoPresenter,
            cardsBoxPseudoPresenter,
            cardPresentationPresenter,
            gameMoneyGiftPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        videoPresenter.Initialize();

        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        playToolbarPresenter.Initialize();

        gameScorePresenter.Initialize();
        storeLevelPresenter.Initialize();
        storeCardDesignPresenter.Initialize();
        cardsOrchestrationPresenter.Initialize();

        storeCardPresenter.Initialize();
        gameMoneyGiftPresenter.Initialize();
        cardUniqueCounterPresenter.Initialize();
        cardPresentationPresenter.Initialize();
        cardBoxPresenter.Initialize();
        cardsBoxPseudoPresenter.Initialize();
        cardBoxBuyPresenter.Initialize();

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
        sceneRoot.OnClickToMenu_Win += HandleClickToMenu;
        sceneRoot.OnClickToGame_Win += HandleClickToGame;
        playToolbarPresenter.OnClickToExit += HandleClickToMenu;
        playToolbarPresenter.OnClickToRestart += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToMenu_Win -= HandleClickToMenu;
        sceneRoot.OnClickToGame_Win -= HandleClickToGame;
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

        gameScorePresenter?.Dispose();
        storeLevelPresenter?.Dispose();
        storeCardDesignPresenter?.Dispose();
        cardsOrchestrationPresenter?.Dispose();

        storeCardPresenter?.Dispose();
        gameMoneyGiftPresenter?.Dispose();
        cardUniqueCounterPresenter?.Dispose();
        cardPresentationPresenter.Dispose();
        cardBoxPresenter.Dispose();
        cardsBoxPseudoPresenter.Dispose();
        cardBoxBuyPresenter.Dispose();

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
