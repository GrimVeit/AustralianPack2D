using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IBookPageProvider _bookPageProvider;

    public AlbumState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IBookPageProvider bookPageProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _bookPageProvider = bookPageProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - ALBUM / MENU</color>");

        _sceneRoot.OnClickToExit_AlbumHeader += ChangeStateToMain;

        _sceneRoot.OnClickToCommon_AlbumChoose += Common;
        _sceneRoot.OnClickToUncommon_AlbumChoose += Uncommon;
        _sceneRoot.OnClickToRare_AlbumChoose += Rare;
        _sceneRoot.OnClickToEpic_AlbumChoose += Epic;
        _sceneRoot.OnClickToMythical_AlbumChoose += Mythical;

        _sceneRoot.OpenAlbumHeaderPanel();
        _sceneRoot.OpenAlbumMiddlePanel();
        _sceneRoot.OpenBackgroundPanel_Album();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToExit_AlbumHeader -= ChangeStateToMain;

        _sceneRoot.OnClickToCommon_AlbumChoose -= Common;
        _sceneRoot.OnClickToUncommon_AlbumChoose -= Uncommon;
        _sceneRoot.OnClickToRare_AlbumChoose -= Rare;
        _sceneRoot.OnClickToEpic_AlbumChoose -= Epic;
        _sceneRoot.OnClickToMythical_AlbumChoose -= Mythical;

        _sceneRoot.CloseAlbumMiddlePanel();
    }

    private void Common()
    {
        _bookPageProvider.OpenPage(CardType.Common);

        ChangeStateToAlbumTable();
    }

    private void Uncommon()
    {
        _bookPageProvider.OpenPage(CardType.Uncommon);

        ChangeStateToAlbumTable();
    }

    private void Rare()
    {
        _bookPageProvider.OpenPage(CardType.Rare);

        ChangeStateToAlbumTable();
    }

    private void Epic()
    {
        _bookPageProvider.OpenPage(CardType.Epic);

        ChangeStateToAlbumTable();
    }

    private void Mythical()
    {
        _bookPageProvider.OpenPage(CardType.Mythical);

        ChangeStateToAlbumTable();
    }

    private void ChangeStateToMain()
    {
        _sceneRoot.CloseAlbumHeaderPanel();
        _sceneRoot.CloseBackgroundPanel_Album();

        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }

    private void ChangeStateToAlbumTable()
    {
        _machineProvider.EnterState(_machineProvider.GetState<AlbumTableState_Menu>());
    }
}
