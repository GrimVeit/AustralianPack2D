using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class VisualPseudoPicturePieceView : View
{
    [SerializeField] private VisualInteractivePseudoPicturePieces visualInteractivePseudoPieces;
    [SerializeField] private VisualNotInteractivePseudoPicturePieces notInteractivePseudoPieces;

    public void Initialize()
    {
        visualInteractivePseudoPieces.OnOpenPiece += OpenPiece;
        visualInteractivePseudoPieces.OnStartDrag += StartDrag;
        visualInteractivePseudoPieces.OnStopDrag += StopDrag;

        visualInteractivePseudoPieces.OnPieceReturn += PieceReturn;
        visualInteractivePseudoPieces.OnPiecePut += PiecePut;

        notInteractivePseudoPieces.OnPieceReturn += PieceReturn;
    }

    public void Dispose()
    {
        visualInteractivePseudoPieces.OnOpenPiece -= OpenPiece;
        visualInteractivePseudoPieces.OnStartDrag -= StartDrag;
        visualInteractivePseudoPieces.OnStopDrag -= StopDrag;

        visualInteractivePseudoPieces.OnPieceReturn -= PieceReturn;
        visualInteractivePseudoPieces.OnPiecePut -= PiecePut;

        notInteractivePseudoPieces.OnPieceReturn -= PieceReturn;
    }

    public void AddPiece(ChickenPicturePiece piece)
    {
        visualInteractivePseudoPieces.AddPiece(piece);
        notInteractivePseudoPieces.AddPiece(piece);
    }

    public void RemovePiece(ChickenPicturePiece piece)
    {
        visualInteractivePseudoPieces.RemovePiece(piece);
        notInteractivePseudoPieces.RemovePiece(piece);
    }


    [System.Serializable]
    private class VisualInteractivePseudoPicturePieces
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private VisualPseudoPicturePiece pseudoPiecePrefab;
        [SerializeField] private RectTransform pseudoPiecesParent;

        private List<VisualPseudoPicturePiece> _pseudoPieces = new();
        private VisualPseudoPicturePiece _currentPseudoChip;

        public void AddPiece(ChickenPicturePiece piece)
        {
            var pseudoPiece = Instantiate(pseudoPiecePrefab, pseudoPiecesParent);

            var pos = GetRandomPointInRect(pseudoPiecesParent);
            pseudoPiece.SetMainPos(pos);
            pseudoPiece.transform.localPosition = pos;

            pseudoPiece.OnGrabbing += GrabPseudoPiece;
            pseudoPiece.Initialize();

            var size = new Vector2(piece.Sprite.rect.width, piece.Sprite.rect.height) / 7.2f;

            pseudoPiece.SetSize(size);
            pseudoPiece.SetData(piece);

            pseudoPiece.transform.localScale = Vector3.zero;
            pseudoPiece.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);

            _pseudoPieces.Add(pseudoPiece);
        }

        public void RemovePiece(ChickenPicturePiece piece)
        {
            var pseudoPiece = GetPseudoPicturePiece(piece);

            if (pseudoPiece == null) return;

            _pseudoPieces.Remove(pseudoPiece);

            pseudoPiece.OnGrabbing -= GrabPseudoPiece;
            pseudoPiece.OnStartMove -= StartMove;
            pseudoPiece.OnMove -= Move;
            pseudoPiece.OnEndMove -= EndMove;

            pseudoPiece.Dispose();

            Destroy(pseudoPiece.gameObject);
        }

        private VisualPseudoPicturePiece GetPseudoPicturePiece(ChickenPicturePiece piece)
        {
            return _pseudoPieces.FirstOrDefault(data => data.Data == piece);
        }

        #region GRAB/UNGRAB

        public void GrabPseudoPiece(VisualPseudoPicturePiece data)
        {
            UngrabPseudoPiece();

            _currentPseudoChip = data;

            _currentPseudoChip.OnStartMove += StartMove;
            _currentPseudoChip.OnMove += Move;
            _currentPseudoChip.OnEndMove += EndMove;
        }

        public void UngrabPseudoPiece()
        {
            if (_currentPseudoChip != null)
            {
                _currentPseudoChip.OnStartMove -= StartMove;
                _currentPseudoChip.OnMove -= Move;
                _currentPseudoChip.OnEndMove -= EndMove;

                _currentPseudoChip.Teleport();

                _currentPseudoChip = null;
            }
        }

        #endregion

        #region MOVE

        public void StartMove()
        {
            if (_currentPseudoChip != null)
            {
                OnStartDrag?.Invoke();
                _currentPseudoChip.StartMove();
            }
        }

        public void Move(Vector2 vector)
        {
            if (_currentPseudoChip != null)
                _currentPseudoChip.Move(vector / canvas.scaleFactor);
        }

        public void EndMove(VisualPseudoPicturePiece piece, RectTransform rectTransform)
        {
            if (IsInsideParent(pseudoPiecesParent, rectTransform))
            {
                Debug.Log("Внутри родителя (локально)");

                _currentPseudoChip.SetMainPos(rectTransform.anchoredPosition);
                _currentPseudoChip.EndMove();
            }
            else
            {
                Collider2D collider = Physics2D.OverlapPoint(rectTransform.position);

                if (collider != null)
                {
                    Debug.Log(collider.gameObject.name);

                    if (collider.gameObject.TryGetComponent(out IPictureDropZone zone))
                    {
                        Debug.Log($"TYPE PIECE: {piece.Data.Type}/TYPE ZONE: {zone.Type}, ID_PICTURE PIECE: {piece.Data.IdPicture}/ID_PICTURE ZONE: {zone.IdZone}");

                        if (piece.Data.Type == zone.Type && piece.Data.IdPicture == zone.IdZone)
                        {
                            OnPiecePut?.Invoke();
                            OnOpenPiece?.Invoke(piece.Data);
                        }
                        else
                        {
                            OnPieceReturn?.Invoke();
                            _currentPseudoChip.EndMove();
                        }
                    }
                    else if(collider.gameObject.TryGetComponent(out IHintPictureZone hintZone))
                    {
                        hintZone.SetType(piece.Data.Type);

                        _currentPseudoChip.EndMove();
                    }
                    else
                    {
                        OnPieceReturn?.Invoke();
                        _currentPseudoChip.EndMove();
                    }
                }
                else
                {
                    OnPieceReturn?.Invoke();
                    _currentPseudoChip.EndMove();
                }
            }

            OnStopDrag?.Invoke();
        }

        #endregion

        private Vector2 GetRandomPointInRect(RectTransform rectTransform)
        {
            Rect rect = rectTransform.rect;

            float x = UnityEngine.Random.Range(rect.xMin, rect.xMax);
            float y = UnityEngine.Random.Range(rect.yMin, rect.yMax);

            return new Vector2(x, y);
        }

        private bool IsInsideParent(RectTransform parent, RectTransform child)
        {
            Vector2 pos = child.anchoredPosition;
            Rect rect = parent.rect;

            return pos.x >= rect.xMin && pos.x <= rect.xMax &&
                   pos.y >= rect.yMin && pos.y <= rect.yMax;
        }

        #region Output

        public event Action<ChickenPicturePiece> OnOpenPiece;

        public event Action OnStartDrag;
        public event Action OnStopDrag;

        public event Action OnPieceReturn;
        public event Action OnPiecePut;

        #endregion
    }

    [System.Serializable]
    private class VisualNotInteractivePseudoPicturePieces
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private VisualPseudoPicturePiece pseudoPiecePrefab;
        [SerializeField] private RectTransform pseudoPiecesParent;

        private List<VisualPseudoPicturePiece> _pseudoPieces = new();
        private VisualPseudoPicturePiece _currentPseudoChip;

        public void AddPiece(ChickenPicturePiece piece)
        {
            var pseudoPiece = Instantiate(pseudoPiecePrefab, pseudoPiecesParent);

            var pos = GetRandomPointInRect(pseudoPiecesParent);
            pseudoPiece.SetMainPos(pos);
            pseudoPiece.transform.localPosition = pos;

            pseudoPiece.OnGrabbing += GrabPseudoPiece;
            pseudoPiece.Initialize();

            var size = new Vector2(piece.Sprite.rect.width, piece.Sprite.rect.height) / 7.2f;

            pseudoPiece.SetSize(size);
            pseudoPiece.SetData(piece);

            pseudoPiece.transform.localScale = Vector3.zero;
            pseudoPiece.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);

            _pseudoPieces.Add(pseudoPiece);
        }

        public void RemovePiece(ChickenPicturePiece piece)
        {
            var pseudoPiece = GetPseudoPicturePiece(piece);

            if (pseudoPiece == null) return;

            _pseudoPieces.Remove(pseudoPiece);

            pseudoPiece.OnGrabbing -= GrabPseudoPiece;
            pseudoPiece.OnStartMove -= StartMove;
            pseudoPiece.OnMove -= Move;
            pseudoPiece.OnEndMove -= EndMove;

            pseudoPiece.Dispose();

            Destroy(pseudoPiece.gameObject);
        }

        private VisualPseudoPicturePiece GetPseudoPicturePiece(ChickenPicturePiece piece)
        {
            return _pseudoPieces.FirstOrDefault(data => data.Data == piece);
        }

        #region GRAB/UNGRAB

        public void GrabPseudoPiece(VisualPseudoPicturePiece data)
        {
            UngrabPseudoPiece();

            _currentPseudoChip = data;

            _currentPseudoChip.OnStartMove += StartMove;
            _currentPseudoChip.OnMove += Move;
            _currentPseudoChip.OnEndMove += EndMove;
        }

        public void UngrabPseudoPiece()
        {
            if (_currentPseudoChip != null)
            {
                _currentPseudoChip.OnStartMove -= StartMove;
                _currentPseudoChip.OnMove -= Move;
                _currentPseudoChip.OnEndMove -= EndMove;

                _currentPseudoChip.Teleport();

                _currentPseudoChip = null;
            }
        }

        #endregion

        #region MOVE

        public void StartMove()
        {
            if (_currentPseudoChip != null)
                _currentPseudoChip.StartMove();
        }

        public void Move(Vector2 vector)
        {
            if (_currentPseudoChip != null)
                _currentPseudoChip.Move(vector / canvas.scaleFactor);
        }

        public void EndMove(VisualPseudoPicturePiece piece, RectTransform rectTransform)
        {
            if (IsInsideParent(pseudoPiecesParent, rectTransform))
            {
                _currentPseudoChip.SetMainPos(rectTransform.anchoredPosition);
                _currentPseudoChip.EndMove();
            }
            else
            {
                OnPieceReturn?.Invoke();
                _currentPseudoChip.EndMove();
            }
        }

        #endregion

        private Vector2 GetRandomPointInRect(RectTransform rectTransform)
        {
            Rect rect = rectTransform.rect;

            float x = UnityEngine.Random.Range(rect.xMin, rect.xMax);
            float y = UnityEngine.Random.Range(rect.yMin, rect.yMax);

            return new Vector2(x, y);
        }

        private bool IsInsideParent(RectTransform parent, RectTransform child)
        {
            Vector2 pos = child.anchoredPosition;
            Rect rect = parent.rect;

            return pos.x >= rect.xMin && pos.x <= rect.xMax &&
                   pos.y >= rect.yMin && pos.y <= rect.yMax;
        }

        public event Action OnPieceReturn;
    }


    #region

    public event Action<ChickenPicturePiece> OnOpenPiece;

    public event Action OnStartDrag;
    public event Action OnStopDrag;

    public event Action OnPieceReturn;
    public event Action OnPiecePut;

    private void OpenPiece(ChickenPicturePiece piece)
    {
        OnOpenPiece?.Invoke(piece);
    }

    private void StartDrag()
    {
        OnStartDrag?.Invoke();
    }

    private void StopDrag()
    {
        OnStopDrag?.Invoke();
    }

    private void PieceReturn()
    {
        OnPieceReturn?.Invoke();
    }

    private void PiecePut()
    {
        OnPiecePut?.Invoke();
    }

    #endregion
}
