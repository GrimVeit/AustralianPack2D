using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Menu : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Menu(
        UIMainMenuRoot sceneRoot, 
        IBookPageProvider bookPageProvider,
        ICardBoxProvider cardBoxProvider,
        ICardBoxListener cardBoxListener,
        ICardBoxBuyVisualListener cardBoxBuyVisualListener,
        
        ICardsBoxPseudoListener cardsBoxPseudoListener,
        ICardsBoxPseudoProvider cardsBoxPseudoProvider,
        
        IShopCardPresentationProvider shopCardPresentationProvider,
        IShopCardPresentationListener shopCardPresentationListener,
        
        FirebaseAuthenticationPresenter firebaseAuthenticationPresenter,
        FirebaseDatabasePresenter firebaseDatabasePresenter,
        NicknamePresenter nicknamePresenter,
        
        IVideoProvider videoProvider,
        
        ICardVisualListener cardVisualListener)
    {
        states[typeof(StartHoldOnState_Menu)] = new StartHoldOnState_Menu(this);

        states[typeof(CheckAuthorizationState_Menu)] = new CheckAuthorizationState_Menu(this, firebaseAuthenticationPresenter);

        states[typeof(HoldOnRegistrateState_Menu)] = new HoldOnRegistrateState_Menu(this, sceneRoot, videoProvider);
        states[typeof(NameAndAvatarInputState_Menu)] = new NameAndAvatarInputState_Menu(this, sceneRoot, nicknamePresenter, firebaseAuthenticationPresenter, firebaseDatabasePresenter);
        states[typeof(RegistrationState_Menu)] = new RegistrationState_Menu(this, sceneRoot, firebaseAuthenticationPresenter, firebaseDatabasePresenter);

        states[typeof(StartMainState_Menu)] = new StartMainState_Menu(this, firebaseAuthenticationPresenter, firebaseDatabasePresenter);
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);
        states[typeof(LevelState_Menu)] = new LevelState_Menu(this, sceneRoot);
        states[typeof(SettingsState_Menu)] = new SettingsState_Menu(this, sceneRoot);
        states[typeof(LeadersState_Menu)] = new LeadersState_Menu(this, sceneRoot);

        states[typeof(ShopState_Menu)] = new ShopState_Menu(this, sceneRoot);
        states[typeof(ShopCoverState_Menu)] = new ShopCoverState_Menu(this, sceneRoot);
        states[typeof(ShopChoosePackState_Menu)] = new ShopChoosePackState_Menu(this, sceneRoot, cardBoxBuyVisualListener);
        states[typeof(ShopOpenPackState_Menu)] = new ShopOpenPackState_Menu(this, sceneRoot, cardBoxProvider, cardBoxListener, cardsBoxPseudoProvider, cardsBoxPseudoListener, shopCardPresentationProvider);
        states[typeof(ShopCardsPresentationState_Menu)] = new ShopCardsPresentationState_Menu(this, sceneRoot, shopCardPresentationProvider, shopCardPresentationListener);
        states[typeof(ShopCardPresentationState_Menu)] = new ShopCardPresentationState_Menu(this, sceneRoot);

        states[typeof(AlbumState_Menu)] = new AlbumState_Menu(this, sceneRoot, bookPageProvider);
        states[typeof(AlbumTableState_Menu)] = new AlbumTableState_Menu(this, sceneRoot, cardVisualListener);
        states[typeof(AlbumCardPresentationState_Menu)] = new AlbumCardPresentationState_Menu(this, sceneRoot);
    }

    public void Initialize()
    {
        EnterState(GetState<StartHoldOnState_Menu>());
    }

    public void Dispose()
    {

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
