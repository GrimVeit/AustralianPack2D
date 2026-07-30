using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOnRegistrateState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;

    private IEnumerator timer;

    public HoldOnRegistrateState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IVideoProvider videoProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _videoProvider.Play("BackgroundRegistration");

        _sceneRoot.OpenBackgroundRegistrationPanel();

        yield return new WaitForSeconds(3.5f);

        ChangeStateToNameAvatar();
    }

    private void ChangeStateToNameAvatar()
    {
        _machineProvider.EnterState(_machineProvider.GetState<NameAndAvatarInputState_Menu>());
    }
}
