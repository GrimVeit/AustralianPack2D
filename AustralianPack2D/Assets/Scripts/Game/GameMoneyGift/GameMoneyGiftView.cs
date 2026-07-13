using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMoneyGiftView : View
{
    [SerializeField] private TextMeshProUGUI textGiftCount;

    public void SetGiftCount(int count)
    {
        textGiftCount.text = count.ToString();
    }
}
