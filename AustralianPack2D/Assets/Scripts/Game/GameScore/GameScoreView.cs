using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameScoreView : View
{
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private Color colorGood;
    [SerializeField] private Color colorNormal;

    private Tween tweenColor;

    public void SetMove(int count, bool isGood)
    {
        tweenColor?.Kill();

        textMoves.text = count.ToString();

        if (isGood)
        {
            tweenColor = textMoves.DOColor(colorGood, 0.2f);
        }
        else
        {
            tweenColor = textMoves.DOColor(colorNormal, 0.2f);
        }
    }
}
