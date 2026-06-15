using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreChickenPictureModel
{
    public event Action<ChickenPicturePiece> OnPieceOwned;
    public event Action<ChickenPicturePiece> OnPieceOpened;
    public event Action OnAllOpened;

    private readonly ChickenAllPicturesSO _so;
    private ChickenAllPictures _runtime;

    private string FilePath => Path.Combine(Application.persistentDataPath, $"{_fileName}.json");
    private readonly string _fileName;
    private PicturesAllTypes _saveData;

    public StoreChickenPictureModel(string fileName, ChickenAllPicturesSO so)
    {
        _fileName = fileName;
        _so = so;
    }

    public void Initialize()
    {
        _runtime = new ChickenAllPictures(_so);

        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            _saveData = JsonUtility.FromJson<PicturesAllTypes>(json);
        }
        else
        {
            _saveData = CreateEmptySave();
        }

        for (int t = 0; t < _runtime.chickenTypePictures.Count; t++)
        {
            var typeRuntime = _runtime.chickenTypePictures[t];
            var typeSave = _saveData.Types[t];

            for (int p = 0; p < typeRuntime.Pictures.Count; p++)
            {
                var pictureRuntime = typeRuntime.Pictures[p];
                var pictureSave = typeSave.Pictures[p];

                for (int i = 0; i < pictureRuntime.Pieces.Count; i++)
                {
                    if (pictureSave.Pieces[i].IsOpen)
                    {
                        pictureRuntime.Pieces[i].Open();
                        OnPieceOpened?.Invoke(pictureRuntime.Pieces[i]);
                    }
                    else
                    {
                        if (pictureSave.Pieces[i].IsOwned)
                        {
                            pictureRuntime.Pieces[i].Owned();
                            OnPieceOwned?.Invoke(pictureRuntime.Pieces[i]);
                        }
                    }
                }
            }
        }
    }

    public void Dispose()
    {
        SyncRuntimeToSave();

        string json = JsonUtility.ToJson(_saveData);
        File.WriteAllText(FilePath, json);
    }


    #region Input

    public AllChickenPictures GetPicturesByType(ChickenType type)
    {
        var typeData = _runtime.chickenTypePictures.Find(t => t.Type == type);

        if (typeData == null) return null;


        return typeData;
    }

    public int CountAvailablePieces()
    {
        List<ChickenPicturePiece> available = new();

        foreach (var type in _runtime.chickenTypePictures)
        {
            foreach (var picture in type.Pictures)
            {
                foreach (var piece in picture.Pieces)
                {
                    if (!piece.IsOpen && !piece.IsOwned)
                    {
                        available.Add(piece);
                    }
                }
            }
        }

        return available.Count;
    }

    public List<ChickenTypeStatsDTO> GetTypesStats()
    {
        var result = new List<ChickenTypeStatsDTO>();

        foreach (var type in _runtime.chickenTypePictures)
        {
            int completed = 0;
            int total = type.Pictures.Count;

            foreach (var picture in type.Pictures)
            {
                bool isFull = true;

                foreach (var piece in picture.Pieces)
                {
                    if (!piece.IsOpen)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                    completed++;
            }

            result.Add(new ChickenTypeStatsDTO(
                type.Type,
                completed,
                total
            ));
        }

        return result;
    }

    public ChickenPicturePiece GetRandomAvailablePiece()
    {
        List<ChickenPicturePiece> available = new();

        foreach (var type in _runtime.chickenTypePictures)
        {
            foreach (var picture in type.Pictures)
            {
                foreach (var piece in picture.Pieces)
                {
                    if (!piece.IsOpen && !piece.IsOwned)
                    {
                        available.Add(piece);
                    }
                }
            }
        }

        if (available.Count == 0)
            return null;

        var pieceL = available[UnityEngine.Random.Range(0, available.Count)];

        pieceL.Owned();

        return pieceL;
    }

    public void OpenPiece(ChickenType type, int pictureId, int pieceId)
    {
        var typeData = _runtime.chickenTypePictures.Find(t => t.Type == type);
        if (typeData == null) return;

        var picture = typeData.Pictures.Find(p => p.Id == pictureId);
        if (picture == null) return;

        var piece = picture.Pieces.Find(p => p.IdPiece == pieceId);
        if (piece == null) return;

        if (piece.IsOpen)
            return;

        piece.Open();
        OnPieceOpened?.Invoke(piece);

        CheckAllOpened();
    }

    public void OwnedPiece(ChickenType type, int pictureId, int pieceId)
    {
        var typeData = _runtime.chickenTypePictures.Find(t => t.Type == type);
        if (typeData == null) return;

        var picture = typeData.Pictures.Find(p => p.Id == pictureId);
        if (picture == null) return;

        var piece = picture.Pieces.Find(p => p.IdPiece == pieceId);
        if (piece == null) return;

        if (piece.IsOpen) return;

        piece.Owned();
        OnPieceOwned?.Invoke(piece);

        CheckAllOpened();
    }

    #endregion

    private PicturesAllTypes CreateEmptySave()
    {
        var types = new List<PicturesType>();

        foreach (var type in _runtime.chickenTypePictures)
        {
            var pictures = new List<PictureData>();

            foreach (var pic in type.Pictures)
            {
                var pieces = new PicturePieceData[pic.Pieces.Count];

                for (int i = 0; i < pieces.Length; i++)
                    pieces[i] = new PicturePieceData(true, true);

                pictures.Add(new PictureData(pieces));
            }

            types.Add(new PicturesType(pictures.ToArray()));
        }

        return new PicturesAllTypes(types.ToArray());
    }

    private void CheckAllOpened()
    {
        foreach (var type in _runtime.chickenTypePictures)
        {
            foreach (var picture in type.Pictures)
            {
                foreach (var piece in picture.Pieces)
                {
                    if (!piece.IsOpen)
                        return;
                }
            }
        }

        OnAllOpened?.Invoke();
    }

    private void SyncRuntimeToSave()
    {
        for (int t = 0; t < _runtime.chickenTypePictures.Count; t++)
        {
            var typeRuntime = _runtime.chickenTypePictures[t];
            var typeSave = _saveData.Types[t];

            for (int p = 0; p < typeRuntime.Pictures.Count; p++)
            {
                var pictureRuntime = typeRuntime.Pictures[p];
                var pictureSave = typeSave.Pictures[p];

                for (int i = 0; i < pictureRuntime.Pieces.Count; i++)
                {
                    pictureSave.Pieces[i].IsOpen = pictureRuntime.Pieces[i].IsOpen;
                    pictureSave.Pieces[i].IsOwned = pictureRuntime.Pieces[i].IsOwned;
                }
            }
        }
    }
}

#region DTO

public class ChickenPicturesDTO
{
    public readonly ChickenType Type;
    public readonly List<ChickenPictureDTO> Pictures;

    public ChickenPicturesDTO(ChickenType type, List<ChickenPictureDTO> pictures)
    {
        Type = type;
        Pictures = pictures;
    }
}

public class ChickenPictureDTO
{
    public readonly int Id;
    public readonly List<ChickenPieceDTO> Pieces;

    public ChickenPictureDTO(int id, List<ChickenPieceDTO> pieces)
    {
        Id = id;
        Pieces = pieces;
    }
}

public class ChickenPieceDTO
{
    public readonly ChickenType Type;
    public readonly int IdPicture;
    public readonly int IdPiece;
    public readonly Sprite Sprite;
    public readonly bool IsOpen;
    public readonly bool IsOwned;

    public ChickenPieceDTO(ChickenType type, int idPicture, int idPiece, Sprite sprite, bool isOpen, bool isOwned)
    {
        Type = type;
        IdPicture = idPicture;
        IdPiece = idPiece;
        Sprite = sprite;
        IsOpen = isOpen;
        IsOwned = isOwned;
    }
}

#endregion

[Serializable]
public class PicturesAllTypes
{
    public PicturesType[] Types;

    public PicturesAllTypes(PicturesType[] types)
    {
        Types = types;
    }
}

[Serializable]
public class PicturesType
{
    public PictureData[] Pictures;

    public PicturesType(PictureData[] pictures)
    {
        Pictures = pictures;
    }
}

[Serializable]
public class PictureData
{
    public PicturePieceData[] Pieces;

    public PictureData(PicturePieceData[] pieces)
    {
        Pieces = pieces;
    }
}

[Serializable]
public class PicturePieceData
{
    public bool IsOpen;
    public bool IsOwned;

    public PicturePieceData(bool isOpen, bool isOwned)
    {
        IsOpen = isOpen;
        IsOwned = isOwned;
    }
}



public class ChickenTypeStatsDTO
{
    public readonly ChickenType Type;
    public readonly int CompletedPictures;
    public readonly int TotalPictures;

    public ChickenTypeStatsDTO(ChickenType type, int completedPictures, int totalPictures)
    {
        Type = type;
        CompletedPictures = completedPictures;
        TotalPictures = totalPictures;
    }
}