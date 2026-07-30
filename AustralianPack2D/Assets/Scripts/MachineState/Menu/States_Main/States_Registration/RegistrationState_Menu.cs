using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationState_Menu : IState
{
    private readonly IStateMachineProvider _globalStateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public RegistrationState_Menu(IStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - REGISTRATION STATE / GAME</color>");

        _firebaseAuthenticationPresenter.OnSignUp += _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp += ChangeStateToStartMainMenu;

        _firebaseAuthenticationPresenter.OnSignUpError += ChangeStateToNameAndAvatarInput;

        _firebaseAuthenticationPresenter.SignUp();

        _sceneRoot.OpenLoadingPanel();
    }

    public void ExitState()
    {
        _firebaseAuthenticationPresenter.OnSignUp -= _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp -= ChangeStateToStartMainMenu;

        _firebaseAuthenticationPresenter.OnSignUpError -= ChangeStateToNameAndAvatarInput;

        _sceneRoot.CloseLoadingPanel();
    }

    private void ChangeStateToNameAndAvatarInput()
    {
        _globalStateMachineProvider.EnterState(_globalStateMachineProvider.GetState<NameAndAvatarInputState_Menu>());
    }

    private void ChangeStateToStartMainMenu()
    {
        _sceneRoot.CloseBackgroundRegistrationPanel();

        _globalStateMachineProvider.EnterState(_globalStateMachineProvider.GetState<StartMainState_Menu>());
    }
}
