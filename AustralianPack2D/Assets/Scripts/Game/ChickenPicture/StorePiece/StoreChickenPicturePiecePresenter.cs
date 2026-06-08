using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreChickenPicturePiecePresenter : IStoreChickenPicturePieceListener, IStoreChickenPicturePieceProvider
{
    private readonly StoreChickenPicturePieceModel _model;

    public StoreChickenPicturePiecePresenter(StoreChickenPicturePieceModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<ChickenPicturePiece> OnAddPiece
    {
        add => _model.OnAddPiece += value;
        remove => _model.OnAddPiece -= value;
    }

    public event Action<ChickenPicturePiece> OnRemovePiece
    {
        add => _model.OnRemovePiece += value;
        remove => _model.OnRemovePiece -= value;
    }

    #endregion

    #region Input

    public void OpenPiece(ChickenPicturePiece piece) => _model.OpenPiece(piece);

    #endregion
}

public interface IStoreChickenPicturePieceProvider
{
    void OpenPiece(ChickenPicturePiece piece);
}

public interface IStoreChickenPicturePieceListener
{
    public event Action<ChickenPicturePiece> OnAddPiece;
    public event Action<ChickenPicturePiece> OnRemovePiece;
}
