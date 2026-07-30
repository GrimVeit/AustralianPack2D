using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAuthorizationState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;

    public CheckAuthorizationState_Menu(IStateMachineProvider machineProvider, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter)
    {
        _machineProvider = machineProvider;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - AUTHORIZATION STATE / GAME</color>");

        if (_firebaseAuthenticationPresenter.IsAuthorization())
        {
            ChangeStateToStartMain();
        }
        else
        {
            ChangeStateToStartRegistration();
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToStartRegistration()
    {
        _machineProvider.EnterState(_machineProvider.GetState<HoldOnRegistrateState_Menu>());
    }

    private void ChangeStateToStartMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<StartMainState_Menu>());
    }
}
