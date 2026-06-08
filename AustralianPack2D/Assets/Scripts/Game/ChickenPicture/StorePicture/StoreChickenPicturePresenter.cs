using System;
using System.Collections.Generic;

public class StoreChickenPicturePresenter : IStoreChickenPictureListener, IStoreChickenPictureProvider
{
    private readonly StoreChickenPictureModel _model;

    public StoreChickenPicturePresenter(StoreChickenPictureModel model)
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

    public event Action<ChickenPicturePiece> OnPieceOwned
    {
        add => _model.OnPieceOwned += value;
        remove => _model.OnPieceOwned -= value;
    }

    public event Action<ChickenPicturePiece> OnPieceOpened
    {
        add => _model.OnPieceOpened += value;
        remove => _model.OnPieceOpened -= value;
    }

    public event Action OnAllOpened
    {
        add => _model.OnAllOpened += value;
        remove => _model.OnAllOpened -= value;
    }

    #endregion

    #region Input

    public AllChickenPictures GetPicturesByType(ChickenType type) => _model.GetPicturesByType(type);
    public ChickenPicturePiece GetRandomAvailablePiece() => _model.GetRandomAvailablePiece();

    public List<ChickenTypeStatsDTO> GetTypesStats() => _model.GetTypesStats();

    public int CountAvailablePieces() => _model.CountAvailablePieces();

    public void OpenPiece(ChickenType type, int pictureId, int pieceId) => _model.OpenPiece(type, pictureId, pieceId);
    public void OwnedPiece(ChickenType type, int pictureId, int pieceId) => _model.OwnedPiece(type, pictureId, pieceId);

    #endregion
}

public interface IStoreChickenPictureListener
{
    public event Action<ChickenPicturePiece> OnPieceOwned;
    public event Action<ChickenPicturePiece> OnPieceOpened;

    public event Action OnAllOpened;
}

public interface IStoreChickenPictureProvider
{
    public AllChickenPictures GetPicturesByType(ChickenType type);
    public ChickenPicturePiece GetRandomAvailablePiece();

    public List<ChickenTypeStatsDTO> GetTypesStats();

    public int CountAvailablePieces();

    public void OpenPiece(ChickenType type, int pictureId, int pieceId);
    public void OwnedPiece(ChickenType type, int pictureId, int pieceId);
}
