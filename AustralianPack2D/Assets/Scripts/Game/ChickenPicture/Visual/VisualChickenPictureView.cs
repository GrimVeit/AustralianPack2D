using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualChickenPictureView : View
{
    [SerializeField] private ChickenPictureButtons chickenPictureButtons;
    [SerializeField] private List<VisualChickenPicture> chickenPictureList;
    [SerializeField] private List<PictureDropZone> dropZones;
    [SerializeField] private TypeNames typeNames;

    private ChickenType _type;

    public void Initialize()
    {
        chickenPictureButtons.OnChooseType += ChooseType;
        chickenPictureButtons.Initialize();

        for (int i = 0; i < chickenPictureList.Count; i++)
        {
            chickenPictureList[i].OnClickPicture += ClickPicture;
            chickenPictureList[i].Initialize(i);
        }
    }

    public void Dispose()
    {
        chickenPictureButtons.OnChooseType -= ChooseType;
        chickenPictureButtons.Dispose();

        for (int i = 0; i < chickenPictureList.Count; i++)
        {
            chickenPictureList[i].OnClickPicture -= ClickPicture;
            chickenPictureList[i].Dispose();
        }
    }

    public void Clear()
    {
        chickenPictureList.ForEach(cp => cp.Clear());
    }

    public void SetType(ChickenType Type)
    {
        _type = Type;

        typeNames.SetType(_type);

        dropZones.ForEach(cp => cp.SetType(Type));
    }

    public void UpdatePiece(ChickenPicturePiece dto)
    {
        var index = dto.IdPicture;

        Debug.Log("ID Picture - " + dto.IdPicture);

        chickenPictureList[index].UpdatePiece(dto);
    }

    #region Output

    public event Action<ChickenType> OnChooseType;
    public event Action<ChickenType, int, int, int> OnClickPicture;

    private void ClickPicture(int idPicture, int countHave, int allCount)
    {
        OnClickPicture?.Invoke(_type, idPicture, countHave, allCount);
    }

    private void ChooseType(ChickenType type)
    {
        OnChooseType?.Invoke(type);
    }

    #endregion

    [System.Serializable]
    private class TypeNames
    {
        [SerializeField] private List<TypeName> typeNames = new List<TypeName>();
        [SerializeField] private TextMeshProUGUI textName;

        public void SetType(ChickenType type)
        {
            var typeName = GetTypeName(type);

            if(typeName == null)
            {
                Debug.LogWarning("Not found TypeName with ChickenType - " + type);
                return;
            }

            textName.text = typeName.Name;
            textName.color = typeName.Color;
        }

        private TypeName GetTypeName(ChickenType type)
        {
            return typeNames.Find(data => data.Type == type);
        }
    }

    [System.Serializable]
    private class TypeName
    {
        [SerializeField] private ChickenType type;
        [SerializeField] private string name;
        [SerializeField] private Color color;

        public ChickenType Type => type;
        public string Name => name;
        public Color Color => color;
    }
}

#region Visual

[Serializable]
public class VisualChickenPicture
{
    [SerializeField] private string name;
    [SerializeField] private List<VisualChickenPicturePiece> picturePieces = new();
    [SerializeField] private PieceLayout pieceLayout;
    [SerializeField] private Button buttonPicture;

    private int _idPicture;

    public void Initialize(int id)
    {
        _idPicture = id;

        buttonPicture.onClick.AddListener(ClickPicture);
    }

    public void Dispose()
    {
        buttonPicture.onClick.RemoveListener(ClickPicture);
    }

    private int CountHave()
    {
        return picturePieces.Where(data => data.IsHave).Count();
    }

    

    public void Clear()
    {
        picturePieces.ForEach(pp => pp.Clear());
    }

    public void UpdatePiece(ChickenPicturePiece dto)
    {
        var layout = pieceLayout.GetLayoutByTypeId(dto.Type, dto.IdPiece);

        if(layout == null)
        {
            Debug.LogError("Not found PieceLayout with Type - " + dto.Type);
            return;
        }

        picturePieces[dto.IdPiece].SetSprite(dto.Sprite);
        picturePieces[dto.IdPiece].SetPosSize(layout.Position, layout.Size);
    }

    [Serializable]
    private class PieceLayout
    {
        [SerializeField] List<TypeLayout> layouts = new();

        public Layout GetLayoutByTypeId(ChickenType type, int index)
        {
            return layouts.Find(l => l.Type == type).Layouts[index];
        }

        [Serializable]
        private class TypeLayout
        {
            public ChickenType Type => type;
            public List<Layout> Layouts => layouts;

            [SerializeField] private string name;
            [SerializeField] private ChickenType type;
            [SerializeField] private List<Layout> layouts;


        }

        [Serializable]
        public class Layout
        {
            [SerializeField] private Vector2 position;
            [SerializeField] private Vector2 size;

            public Vector2 Position => position;
            public Vector2 Size => size;
        }
    }

    #region Output

    public event Action<int, int, int> OnClickPicture;

    private void ClickPicture()
    {
        OnClickPicture?.Invoke(_idPicture, CountHave(), picturePieces.Count);
    }

    #endregion
}

[Serializable]
public class VisualChickenPicturePiece
{
    public bool IsHave => isHave;

    [SerializeField] private Image imagePiece;

    private bool isHave;

    public void Clear()
    {
        isHave = false;

        imagePiece.sprite = null;
        imagePiece.rectTransform.sizeDelta = Vector2.zero;
    }

    public void SetSprite(Sprite sprite)
    {
        isHave = true;

        imagePiece.sprite = sprite;
    }

    public void SetPosSize(Vector3 localPos, Vector3 size)
    {
        imagePiece.rectTransform.localPosition = localPos;
        imagePiece.rectTransform.sizeDelta = size;
    }
}

#endregion

#region BUTTONS

[Serializable]
public class ChickenPictureButtons
{
    [SerializeField] private List<ChickenPictureButton> chickenPictureButtons = new();

    public void Initialize()
    {
        for (int i = 0; i < chickenPictureButtons.Count; i++)
        {
            chickenPictureButtons[i].OnChooseType += ChooseType;
            chickenPictureButtons[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < chickenPictureButtons.Count; i++)
        {
            chickenPictureButtons[i].OnChooseType -= ChooseType;
            chickenPictureButtons[i].Dispose();
        }
    }

    #region Output

    public event Action<ChickenType> OnChooseType;

    private void ChooseType(ChickenType type)
    {
        OnChooseType?.Invoke(type);
    }

    #endregion
}

[Serializable]
public class ChickenPictureButton
{
    [SerializeField] private ChickenType chickenType;
    [SerializeField] private Button buttonType;

    public void Initialize()
    {
        buttonType.onClick.AddListener(ChooseType);
    }

    public void Dispose()
    {
        buttonType.onClick.RemoveListener(ChooseType);
    }

    #region Output

    public event Action<ChickenType> OnChooseType;

    private void ChooseType()
    {
        OnChooseType?.Invoke(chickenType);
    }

    #endregion
}

#endregion
