using System;
using UnityEngine;

public class VisualPseudoPicturePieceModel
{
    private readonly IStoreChickenPicturePieceListener _storeChickenPicturePieceListener;
    private readonly IStoreChickenPicturePieceProvider _storeChickenPicturePieceProvider;
    private readonly ISoundProvider _soundProvider;

    private bool isActive = true;

    public VisualPseudoPicturePieceModel(IStoreChickenPicturePieceListener storeChickenPicturePieceListener, IStoreChickenPicturePieceProvider storeChickenPicturePieceProvider, ISoundProvider soundProvider)
    {
        _storeChickenPicturePieceListener = storeChickenPicturePieceListener;
        _storeChickenPicturePieceProvider = storeChickenPicturePieceProvider;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _storeChickenPicturePieceListener.OnAddPiece += AddPiece;
        _storeChickenPicturePieceListener.OnRemovePiece += RemovePiece;
    }

    public void Dispose()
    {
        _storeChickenPicturePieceListener.OnAddPiece -= AddPiece;
        _storeChickenPicturePieceListener.OnRemovePiece -= RemovePiece;
    }

    private void AddPiece(ChickenPicturePiece piece)
    {
        OnAddPieceToVisual?.Invoke(piece);
    }

    private void RemovePiece(ChickenPicturePiece piece)
    {
        OnRemovePieceFromVisual?.Invoke(piece);
    }

    public void OpenPiece(ChickenPicturePiece piece)
    {
        _storeChickenPicturePieceProvider.OpenPiece(piece);
    }



    public void SoundPieceReturn()
    {
        _soundProvider.PlayOneShot("Pseudo_Return");
    }

    public void SoundPiecePut()
    {
        _soundProvider.PlayOneShot("Pseudo_Put");
    }

    #region ACTIVATOR

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    #endregion

    #region Output

    public event Action<ChickenPicturePiece> OnAddPieceToVisual;
    public event Action<ChickenPicturePiece> OnRemovePieceFromVisual;

    #endregion
}
