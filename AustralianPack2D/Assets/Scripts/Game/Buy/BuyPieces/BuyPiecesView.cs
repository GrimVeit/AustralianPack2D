using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyPiecesView : View
{
    [SerializeField] private VisualBuyPiece piecePrefab;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private RectTransform transformShowPos;

    [SerializeField] private List<VisualBuyPiece> pieceList;

    public void SetPieces(List<ChickenPicturePiece> pieces)
    {
        for (int i = 0; i < pieceList.Count; i++)
        {
            pieceList[i].OnDestroy -= RemovePieces;

            Destroy(pieceList[i].gameObject);
        }

        pieceList.Clear();

        for (int i = 0; i < pieces.Count; i++)
        {
            var piece = Instantiate(piecePrefab, transformSpawn);
            piece.transform.localPosition = new Vector3(0, -450, 0);
            piece.SetData(pieces[i]);
            piece.SetSize(pieces[i].Sprite.rect.size / 5);

            piece.OnDestroy += RemovePieces;

            pieceList.Add(piece);
        }
    }

    private void RemovePieces(VisualBuyPiece piece)
    {
        piece.OnDestroy -= RemovePieces;

        OnOwnedPiece?.Invoke(piece.Data);

        pieceList.Remove(piece);

        Destroy(piece.gameObject);
    }

    public IEnumerator ShowCoro()
    {
        for (int i = pieceList.Count - 1; i >= 0; i--)
        {
            float timeAwait = Random.Range(0.4f, 1);

            pieceList[i].MoveTo(GetRandomPointInRect(transformShowPos), timeAwait);

            yield return new WaitForSeconds(timeAwait);
        }
    }

    public IEnumerator OwnedCoro()
    {
        for (int i = pieceList.Count - 1; i >= 0; i--)
        {
            float timeAwait = Random.Range(0.4f, 1);

            pieceList[i].Destroy();

            yield return new WaitForSeconds(timeAwait);
        }
    }

    private Vector2 GetRandomPointInRect(RectTransform rectTransform)
    {
        Rect rect = rectTransform.rect;

        float x = Random.Range(rect.xMin, rect.xMax);
        float y = Random.Range(rect.yMin, rect.yMax);

        return new Vector2(x, y);
    }

    #region Output

    public event Action<ChickenPicturePiece> OnOwnedPiece;

    #endregion
}
