using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankDisplayView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private Transform moneyDisplay;

    private Vector3 defaultMoneyTableScale = Vector3.one;

    private Tween tweenScale;

    public void Initialize()
    {

    }

    public void AddMoney()
    {
        tweenScale?.Kill();

        moneyDisplay.localScale = defaultMoneyTableScale;

        tweenScale = moneyDisplay.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => tweenScale = moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void RemoveMoney()
    {
        tweenScale?.Kill();

        moneyDisplay.localScale = defaultMoneyTableScale;

        tweenScale = moneyDisplay.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => tweenScale = moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void SendMoneyDisplay(int money)
    {
        textMoney.text = money.ToString();
    }
}
