using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCoverPage : MonoBehaviour
{
    public BookCoverPageData BookPageData;

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
public class BookCoverPageData
{
    public int Index => index;

    [SerializeField] private int index;
}
