using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCard : MonoBehaviour
{
    public RectTransform RectTransform => rectTransform;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image imageCard;

    public void SetData(Sprite sprite)
    {
        imageCard.sprite = sprite;
    }
}
