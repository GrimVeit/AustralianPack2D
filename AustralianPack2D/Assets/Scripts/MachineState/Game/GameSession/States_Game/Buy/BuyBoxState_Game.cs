using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBoxState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IBuyBoxProvider _buyBoxProvider;
    private readonly IBuyPiecesProvider _buyPiecesProvider;

    private IEnumerator timer;

    public BuyBoxState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IBuyBoxProvider buyBoxProvider, IBuyPiecesProvider buyPiecesProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _buyBoxProvider = buyBoxProvider;
        _buyPiecesProvider = buyPiecesProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _sceneRoot.OpenBuyBoxPanel();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseBuyBoxPanel();
    }

    private IEnumerator Timer()
    {
        _buyBoxProvider.OpenClose(true);
        _buyBoxProvider.SetScale(1.2f);

        _buyBoxProvider.MoveToCenter(0.8f);
        _buyPiecesProvider.GeneratePieces();
        _buyBoxProvider.ScaleTo(1.5f, 0.8f);

        yield return new WaitForSeconds(1.2f);

        _buyBoxProvider.OpenCover(1f);

        yield return new WaitForSeconds(1.4f);

        _buyBoxProvider.MoveToCenterDown(0.6f);

        yield return new WaitForSeconds(0.8f);

        yield return _buyPiecesProvider.ShowPieces();

        yield return new WaitForSeconds(0.2f);

        _sceneRoot.OpenPiecesPanel();

        yield return new WaitForSeconds(0.2f);

        yield return _buyPiecesProvider.OwnedPieces();

        yield return new WaitForSeconds(0.1f);

        _buyBoxProvider.CloseCover(0.6f);

        yield return new WaitForSeconds(0.6f);

        _buyBoxProvider.ScaleTo(0, 0.6f);

        yield return new WaitForSeconds(0.6f);

        ChangeStateToCards();
    }

    private void ChangeStateToCards()
    {
        _machineProvider.EnterState(_machineProvider.GetState<CardsState_Game>());
    }
}
