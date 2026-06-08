using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreChickenPicturePieceModel
{
    private readonly IStoreChickenPictureListener _storeChickenPictureListener;
    private readonly IStoreChickenPictureProvider _storeChickenPictureProvider;

    private readonly List<ChickenPicturePiece> _ownedPieces = new();

    public StoreChickenPicturePieceModel(IStoreChickenPictureListener storeChickenPictureListener, IStoreChickenPictureProvider storeChickenPictureProvider)
    {
        _storeChickenPictureListener = storeChickenPictureListener;
        _storeChickenPictureProvider = storeChickenPictureProvider;
    }

    public void Initialize()
    {
        _storeChickenPictureListener.OnPieceOwned += AddOwnedPiece;
        _storeChickenPictureListener.OnPieceOpened += RemoveOwnedPiece;
    }

    public void Dispose()
    {
        _storeChickenPictureListener.OnPieceOwned -= AddOwnedPiece;
        _storeChickenPictureListener.OnPieceOpened -= RemoveOwnedPiece;
    }

    private void AddOwnedPiece(ChickenPicturePiece piece)
    {
        Debug.Log($"ADD PIECE INVENTORY - TYPE: {piece.Type}, ID_PICTURE: {piece.IdPicture}, ID_PIECE: {piece.IdPiece}");

        OnAddPiece?.Invoke(piece);
        _ownedPieces.Add(piece);
    }

    private void RemoveOwnedPiece(ChickenPicturePiece piece)
    {
        if (_ownedPieces.Contains(piece))
        {
            OnRemovePiece?.Invoke(piece);
            _ownedPieces.Remove(piece);
        }
    }

    public void OpenPiece(ChickenPicturePiece piece)
    {
        _storeChickenPictureProvider.OpenPiece(piece.Type, piece.IdPicture, piece.IdPiece);
    }

    #region OUTPUT

    public event Action<ChickenPicturePiece> OnAddPiece;
    public event Action<ChickenPicturePiece> OnRemovePiece;

    #endregion
}
