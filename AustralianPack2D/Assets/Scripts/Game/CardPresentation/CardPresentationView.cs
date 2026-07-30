using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPresentationView : View 
{
    [SerializeField] private Image imageCard;

    public void CardPresentation(Sprite sprite)
    {
        imageCard.sprite = sprite;
    }
}
