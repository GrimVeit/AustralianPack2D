using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPiecesPresenter : IBuyPiecesProvider
{
    private readonly BuyPiecesModel _model;
    private readonly BuyPiecesView _view;

    public BuyPiecesPresenter(BuyPiecesModel model, BuyPiecesView view)
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
        _view.OnOwnedPiece += _model.OwnedPiece;

        _model.OnSetPieces += _view.SetPieces;
    }

    private void DeactivateEvents()
    {
        _view.OnOwnedPiece -= _model.OwnedPiece;

        _model.OnSetPieces -= _view.SetPieces;
    }

    #region Input

    public void GeneratePieces() => _model.GeneratePieces();
    public IEnumerator ShowPieces() => _view.ShowCoro();

    public IEnumerator OwnedPieces() => _view.OwnedCoro();

    public void Clear()
    {

    }

    #endregion
}

public interface IBuyPiecesListener
{
    public event Action OnAllOwned;
    public event Action OnAllShowed;
}

public interface IBuyPiecesProvider
{
    public void GeneratePieces();
    public IEnumerator ShowPieces();
    public IEnumerator OwnedPieces();
    public void Clear();
}
