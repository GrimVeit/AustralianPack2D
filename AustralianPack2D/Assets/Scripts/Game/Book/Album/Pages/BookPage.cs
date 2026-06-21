using DG.Tweening;
using UnityEngine;

public class BookPage : MonoBehaviour
{
    public BookPageData BookPageData;

    public RectTransform RectTransform;

    public void ShowInstant()
    {
        gameObject.SetActive(true);
        RectTransform.anchoredPosition = Vector2.zero;
    }

    public void HideInstant()
    {
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class BookPageData
{
    public int Index => index;
    public CardType TypeCards => typeCards;

    [SerializeField] private int index;
    [SerializeField] private CardType typeCards;
}
