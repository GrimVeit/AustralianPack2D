using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWinState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IVideoProvider _videoProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ISoundProvider _soundProvider;
    private readonly IGameMoneyGiftProvider _gameMoneyGiftProvider;
    //private readonly ISound _sound_Main;

    private IEnumerator timer;

    public StartWinState_Game(IStateMachineProvider machineProvider, IVideoProvider videoProvider, UIGameRoot sceneRoot, ISoundProvider soundProvider, IGameMoneyGiftProvider gameMoneyGiftProvider)
    {
        _machineProvider = machineProvider;
        _videoProvider = videoProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _gameMoneyGiftProvider = gameMoneyGiftProvider;
        //_sound_Main = _soundProvider.GetSound("Background_Main");
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _gameMoneyGiftProvider.SendGift();
        _videoProvider.Play("Win");
        _sceneRoot.OpenWinStartPanel();
        //_soundProvider.PlayOneShot("Win");

        yield return new WaitForSeconds(3.2f);

        //_sound_Main.Play();
        //_sound_Main.SetVolume(0, 1f, 0.1f);

        ChangeStateToWin();
    }

    private void ChangeStateToWin()
    {
        _machineProvider.EnterState(_machineProvider.GetState<WinState_Game>());
    }
}
