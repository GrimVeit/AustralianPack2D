using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPiecesModel
{
    private readonly IStoreChickenPictureProvider _storeChickenPictureProvider;

    public BuyPiecesModel(IStoreChickenPictureProvider storeChickenPictureProvider)
    {
        _storeChickenPictureProvider = storeChickenPictureProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void GeneratePieces()
    {
        int allAvailableCount = _storeChickenPictureProvider.CountAvailablePieces();

        var countRandom = UnityEngine.Random.Range(3, 6);

        var list = new List<ChickenPicturePiece>();

        if(countRandom > allAvailableCount)
        {
            countRandom = allAvailableCount;
        }

        for (int i = 0; i < countRandom; i++)
        {
            list.Add(_storeChickenPictureProvider.GetRandomAvailablePiece());
        }

        if(list.Count == 0)
        {
            //BRANCH
        }

        Debug.Log(list.Count);

        OnSetPieces?.Invoke(list);
    }

    public void OwnedPiece(ChickenPicturePiece piece)
    {
        _storeChickenPictureProvider.OwnedPiece(piece.Type, piece.IdPicture, piece.IdPiece);
    }

    #region Output

    public event Action<List<ChickenPicturePiece>> OnSetPieces;

    #endregion
}
