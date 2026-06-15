using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAllPictures
{
    public readonly List<AllChickenPictures> chickenTypePictures = new();

    public ChickenAllPictures(ChickenAllPicturesSO chickenAllPicturesSO)
    {
        for (int i = 0; i < chickenAllPicturesSO.TypePictures.Count; i++) 
        { 
            var listPictures = new List<ChickenPictures>();

            for (int j = 0; j < chickenAllPicturesSO.TypePictures[i].Pictures.Count; j++)
            {
                var listPieces = new List<ChickenPicturePiece>();

                for (int k = 0; k < chickenAllPicturesSO.TypePictures[i].Pictures[j].Pieces.Count; k++)
                {
                    ChickenPicturePiece piece = new(chickenAllPicturesSO.TypePictures[i].Type, j, k, chickenAllPicturesSO.TypePictures[i].Pictures[j].Pieces[k]);
                    listPieces.Add(piece);
                }

                listPictures.Add(new ChickenPictures(j, listPieces));
            }

            chickenTypePictures.Add(new AllChickenPictures(chickenAllPicturesSO.TypePictures[i].Type, listPictures));
        }
    }
}

public class AllChickenPictures
{
    private readonly ChickenType _type;
    private readonly List<ChickenPictures> _pictures;

    public AllChickenPictures(ChickenType type, List<ChickenPictures> pictures)
    {
        _type = type;
        _pictures = pictures;
    }

    public ChickenType Type => _type;
    public List<ChickenPictures> Pictures => _pictures;
}

public class ChickenPictures
{
    [SerializeField] private int _id;
    [SerializeField] private List<ChickenPicturePiece> _pieces;

    public ChickenPictures(int id, List<ChickenPicturePiece> pieces)
    {
        _id = id;
        _pieces = pieces;
    }

    public int Id => _id;
    public List<ChickenPicturePiece> Pieces => _pieces;
}

public class ChickenPicturePiece
{
    private readonly ChickenType _type;
    private readonly int _idPicture;
    private readonly int _idPiece;
    private readonly Sprite _piece;
    private bool isOpen = false;
    private bool isOwned = false;

    public ChickenPicturePiece(ChickenType type, int idPicture, int idPiece, Sprite piece)
    {
        _type = type;
        _idPicture = idPicture;
        _idPiece = idPiece;
        _piece = piece;
    }

    public void Open()
    {
        isOpen = true;
    }

    public void Owned()
    {
        isOwned = true;
    }

    public ChickenType Type => _type;
    public int IdPicture => _idPicture;
    public int IdPiece => _idPiece;
    public Sprite Sprite => _piece;
    public bool IsOpen => isOpen;
    public bool IsOwned => isOwned;
}
