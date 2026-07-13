using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUniqueCounterView : View
{
    [SerializeField] private TextMeshProUGUI textCount;

    public void SetCount(int count)
    {
        textCount.text = count.ToString();
    }
}
