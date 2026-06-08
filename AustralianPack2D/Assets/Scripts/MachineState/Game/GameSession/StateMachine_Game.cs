using System;
using System.Collections.Generic;

public class StateMachine_Game : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game
    (
        IStoreChickenProvider storeChickenProvider,
        ISpawnerChickenProvider spawnerChickenProvider,
        IChooseChickenProvider chooseChickenProvider,
        UIGameRoot sceneRoot,
        IChickenBattleProvider chickenBattleProvider,
        IChickenBattleListener chickenBattleListener,
        IChooseBuyBoxProvider chooseBuyBoxProvider,
        IVideoProvider videoProvider,
        IMaskEffectProvider maskEffectProvider,
        ISlotMachineListener slotMachineListener,
        ISlotMachineProvider slotMachineProvider,
        IVisualChickenEffectProvider visualChickenEffectProvider,

        ITimerProvider timerProvider_Start,
        ITimerListener timerListener_Start,
        ITimerProvider timerProvider_Game,

        IVisualChickenPictureListener visualChickenPictureListener,
        IShowChickenPictureListener showChickenPictureListener,

        IBuyBoxProvider buyBoxProvider,
        IBuyPiecesProvider buyPiecesProvider,

        ICountChickenPictureProvider countChickenPictureProvider,
        IMoneyProvider moneyProvider,
        IChickenRaceLeaderProvider chickenRaceLeaderProvider,
        ICameraFollowProvider cameraFollowProvider,

        ISoundProvider soundProvider
    )
    {
        states[typeof(IntroVideoState_Game)] = new IntroVideoState_Game(this, sceneRoot, videoProvider, maskEffectProvider);
        states[typeof(PlayVideoState_Game)] = new PlayVideoState_Game(this, sceneRoot, videoProvider, maskEffectProvider);
        states[typeof(ChooseLocationState_Game)] = new ChooseLocationState_Game(this, sceneRoot, maskEffectProvider, slotMachineListener, slotMachineProvider);
        states[typeof(StartGameRunState_Game)] = new StartGameRunState_Game(this, sceneRoot, maskEffectProvider);

        states[typeof(SettingsState_Game)] = new SettingsState_Game(this, sceneRoot);
        states[typeof(StartCardsState_Game)] = new StartCardsState_Game(this, sceneRoot, visualChickenPictureListener, countChickenPictureProvider);
        states[typeof(StartCardsTypeState_Game)] = new StartCardsTypeState_Game(this, sceneRoot, showChickenPictureListener);
        states[typeof(StartShowFullPictureState_Game)] = new StartShowFullPictureState_Game(this, sceneRoot);
        states[typeof(StartShowNotFullPictureState_Game)] = new StartShowNotFullPictureState_Game(this, sceneRoot);

        states[typeof(ChickenSpawnState_Game)] = new ChickenSpawnState_Game(this, spawnerChickenProvider, chooseChickenProvider, storeChickenProvider, sceneRoot);
        states[typeof(ChooseChickenState_Game)] = new ChooseChickenState_Game(this, chooseChickenProvider, sceneRoot, visualChickenEffectProvider);
        states[typeof(WaitGameRunState_Game)] = new WaitGameRunState_Game(this, timerProvider_Start, timerListener_Start, soundProvider);
        states[typeof(GameRunState_Game)] = new GameRunState_Game(this, chickenBattleProvider, chickenBattleListener, sceneRoot, timerProvider_Game, chickenRaceLeaderProvider, cameraFollowProvider, chooseChickenProvider, soundProvider);
        states[typeof(CheckWinnerState_Game)] = new CheckWinnerState_Game(this, chickenBattleListener, chickenBattleProvider, chooseChickenProvider);

        states[typeof(StartLoseState_Game)] = new StartLoseState_Game(this, videoProvider, sceneRoot, soundProvider);
        states[typeof(LoseState_Game)] = new LoseState_Game(this, sceneRoot, soundProvider);

        states[typeof(StartWinState_Game)] = new StartWinState_Game(this, videoProvider, sceneRoot, soundProvider);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot);

        states[typeof(ChooseBuyBoxState_Game)] = new ChooseBuyBoxState_Game(this, chooseBuyBoxProvider, sceneRoot, moneyProvider, soundProvider);
        states[typeof(BuyBoxState_Game)] = new BuyBoxState_Game(this, sceneRoot, buyBoxProvider, buyPiecesProvider);

        states[typeof(CardsState_Game)] = new CardsState_Game(this, sceneRoot, visualChickenPictureListener, countChickenPictureProvider);
        states[typeof(CardsTypeState_Game)] = new CardsTypeState_Game(this, sceneRoot, showChickenPictureListener);
        states[typeof(ShowFullPictureState_Game)] = new ShowFullPictureState_Game(this, sceneRoot);
        states[typeof(ShowNotFullPictureState_Game)] = new ShowNotFullPictureState_Game(this, sceneRoot);
    }

    public void Initialize()
    {
        EnterState(GetState<IntroVideoState_Game>());
    }

    public void Dispose()
    {
        _currentState?.ExitState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void EnterState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
