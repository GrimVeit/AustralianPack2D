using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoneyGiftPresenter
{
    private readonly GameMoneyGiftModel _model;
    private readonly GameMoneyGiftView _view;

    public GameMoneyGiftPresenter(GameMoneyGiftModel model, GameMoneyGiftView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnChangeMoney += _view.SetGiftCount;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeMoney -= _view.SetGiftCount;
    }
}

public interface IGameMoneyGiftProvider
{
    public void AddGift(GameLevel level);
    public void AddGift(int count);

    public void SendGift();
}
