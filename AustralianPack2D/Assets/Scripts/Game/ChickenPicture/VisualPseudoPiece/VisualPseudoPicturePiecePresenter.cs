using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPseudoPicturePiecePresenter : IPseudoPicturePieceActivatorProvider, IPseudoPicturePieceListener
{
    private readonly VisualPseudoPicturePieceModel _model;
    private readonly VisualPseudoPicturePieceView _view;

    public VisualPseudoPicturePiecePresenter(VisualPseudoPicturePieceModel pseudoChipModel, VisualPseudoPicturePieceView pseudoChipView)
    {
        _model = pseudoChipModel;
        _view = pseudoChipView;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnOpenPiece += _model.OpenPiece;
        _view.OnPieceReturn += _model.SoundPieceReturn;
        _view.OnPiecePut += _model.SoundPiecePut;

        _model.OnAddPieceToVisual += _view.AddPiece;
        _model.OnRemovePieceFromVisual += _view.RemovePiece;
    }

    private void DeactivateEvents()
    {
        _view.OnOpenPiece -= _model.OpenPiece;
        _view.OnPieceReturn -= _model.SoundPieceReturn;
        _view.OnPiecePut -= _model.SoundPiecePut;

        _model.OnAddPieceToVisual -= _view.AddPiece;
        _model.OnRemovePieceFromVisual -= _view.RemovePiece;
    }

    #region Output

    public event Action OnStartDrag
    {
        add => _view.OnStartDrag += value;
        remove => _view.OnStartDrag -= value;
    }

    public event Action OnStopDrag
    {
        add => _view.OnStopDrag += value;
        remove => _view.OnStopDrag -= value;
    }

    #endregion

    #region Input

    public void Activate()
    {
        _model.Activate();
    }

    public void Deactivate()
    {
        _model.Deactivate();
    }

    #endregion
}

public interface IPseudoPicturePieceListener
{
    public event Action OnStartDrag;
    public event Action OnStopDrag;
}

public interface IPseudoPicturePieceActivatorProvider
{
    void Activate();
    void Deactivate();
}
