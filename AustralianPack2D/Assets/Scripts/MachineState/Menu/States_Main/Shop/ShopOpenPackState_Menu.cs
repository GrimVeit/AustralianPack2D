using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpenPackState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    private readonly ICardBoxProvider _cardBoxProvider;
    private readonly ICardBoxListener _cardBoxListener;

    private readonly ICardsBoxPseudoProvider _cardsBoxPseudoProvider;
    private readonly ICardsBoxPseudoListener _cardsBoxPseudoListener;

    private readonly ICardPresentationProvider _cardPresentationProvider;

    private bool _isOpenPack = false;
    private bool _isEndMovePseudo = false;
    private bool _isEndRotatePseudo = false;

    private IEnumerator timer;

    public ShopOpenPackState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, ICardBoxProvider cardBoxProvider, ICardBoxListener cardBoxListener, ICardsBoxPseudoProvider cardsBoxPseudoProvider, ICardsBoxPseudoListener cardsBoxPseudoListener, ICardPresentationProvider cardPresentationProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _cardBoxProvider = cardBoxProvider;
        _cardBoxListener = cardBoxListener;
        _cardsBoxPseudoProvider = cardsBoxPseudoProvider;
        _cardsBoxPseudoListener = cardsBoxPseudoListener;
        _cardPresentationProvider = cardPresentationProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP OPEN PACK / MENU</color>");

        if (timer != null) Coroutines.Stop(timer);

        _cardBoxListener.OnEndOpen += ReturnOpen;
        _cardsBoxPseudoListener.OnEndMove += ReturnEndMovePseudo;
        _cardsBoxPseudoListener.OnEndRotate += ReturnEndRotatePseudo;

        _sceneRoot.CloseShopHeaderPanel();
        _sceneRoot.OpenShopOpenPackPanel();
        _sceneRoot.OpenBackgroundPanel_Green();

        _isOpenPack = false;
        _isEndMovePseudo = false;
        _isEndRotatePseudo = false;

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _cardBoxListener.OnEndOpen -= ReturnOpen;
        _cardsBoxPseudoListener.OnEndMove -= ReturnEndMovePseudo;
        _cardsBoxPseudoListener.OnEndRotate -= ReturnEndRotatePseudo;
    }

    private IEnumerator Timer()
    {
        _cardBoxProvider.Hide();
        _cardBoxProvider.Show(0.7f);

        yield return new WaitForSeconds(1.7f);

        _cardBoxProvider.ActivateOpen();

        yield return new WaitUntil(() => _isOpenPack);

        _cardBoxProvider.Hide();
        _cardsBoxPseudoProvider.Show();

        _cardsBoxPseudoProvider.MoveToShow(1);

        yield return new WaitUntil(() => _isEndMovePseudo);

        _cardsBoxPseudoProvider.ShowRotate(0.5f);

        yield return new WaitUntil(() => _isEndRotatePseudo);

        _cardsBoxPseudoProvider.Hide();

        _cardPresentationProvider.Show(0.5f);

        yield return new WaitForSeconds(1f);

        _cardPresentationProvider.ShowDuplicates();

        yield return new WaitForSeconds(2f);

        ChangeStateToShopChoosePack();
    }

    private void ReturnOpen()
    {
        _isOpenPack = true;
    }

    private void ReturnEndMovePseudo()
    {
        _isEndMovePseudo = true;
    }

    private void ReturnEndRotatePseudo()
    {
        _isEndRotatePseudo = true;
    }

    private void ChangeStateToShopChoosePack()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopCardsPresentationState_Menu>());
    }
}
