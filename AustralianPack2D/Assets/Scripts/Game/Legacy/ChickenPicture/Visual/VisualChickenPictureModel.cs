using System;

public class VisualChickenPictureModel
{
    private readonly IStoreChickenPictureProvider _store;
    private readonly IStoreChickenPictureListener _listener;
    private readonly ISoundProvider _soundProvider;

    private ChickenType _currentType;

    public VisualChickenPictureModel(
        IStoreChickenPictureProvider store,
        IStoreChickenPictureListener listener,
        ISoundProvider soundProvider)
    {
        _store = store;
        _listener = listener;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _listener.OnPieceOpened += HandlePiece;
    }

    public void Dispose()
    {
        _listener.OnPieceOpened -= HandlePiece;
    }

    #region VISUAL

    public void GetPicturesByType(ChickenType type)
    {
        _currentType = type;

        _soundProvider.PlayOneShot("PanelOpen");

        var snapshot = _store.GetPicturesByType(type);

        OnClear?.Invoke();

        OnSetType?.Invoke(type);

        foreach (var pic in snapshot.Pictures)
            foreach (var piece in pic.Pieces)
            {
                if(piece.IsOpen)
                    OnPieceUpdate?.Invoke(piece);
            }

        OnSelectType?.Invoke();
    }

    private void HandlePiece(ChickenPicturePiece dto)
    {
        if (dto.Type != _currentType) return;

        OnPieceUpdate?.Invoke(dto);
    }

    #endregion

    #region PICTURE

    public void ClickPicture(ChickenType type, int idPicture, int countHave, int countAll)
    {
        OnClickPicture?.Invoke(type, idPicture, countHave, countAll);
    }

    #endregion

    #region Output

    public event Action<ChickenType> OnSetType;
    public event Action OnClear;
    public event Action<ChickenPicturePiece> OnPieceUpdate;
    public event Action OnSelectType;

    public event Action<ChickenType, int, int, int> OnClickPicture;

    #endregion
}
