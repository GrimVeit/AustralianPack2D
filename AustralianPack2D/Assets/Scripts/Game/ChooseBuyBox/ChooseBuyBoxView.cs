using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBuyBoxView : View
{
    [SerializeField] private BuyBoxes buyBoxes;
    [SerializeField] private UIEffect effectBuy;

    public void Initialize()
    {
        buyBoxes.OnChooseBox += ChooseBox;

        buyBoxes.Initialize();
        effectBuy.Initialize();
    }

    public void Dispose()
    {
        buyBoxes.OnChooseBox -= ChooseBox;

        buyBoxes.Dispose();
        effectBuy.Dispose();
    }

    public void ShowAll()
    {
        buyBoxes.ShowAll();
    }

    public void HideAll(int idAbsolute)
    {
        buyBoxes.HideAll(idAbsolute);
        effectBuy.DeactivateEffect();
    }

    public void Choose(int id)
    {
        var box = buyBoxes.GetBuyBox(id);

        if (box == null)
        {
            Debug.Log("Not found BuyBox with Id - " + id);
            return;
        }

        effectBuy.ActivateEffect();

        box.Choose();
    }

    public void Unchoose(int id)
    {
        var box = buyBoxes.GetBuyBox(id);

        if (box == null)
        {
            Debug.Log("Not found BuyBox with Id - " + id);
            return;
        }

        box.Unchoose();
    }

    #region Output

    public event Action<int> OnChooseBox;

    private void ChooseBox(int id)
    {
        OnChooseBox?.Invoke(id);
    }

    #endregion
}

[Serializable]
public class BuyBoxes
{
    [SerializeField] private List<BuyBox> buyBoxes = new();

    public void Initialize()
    {
        for (int i = 0; i < buyBoxes.Count; i++)
        {
            buyBoxes[i].OnChooseBox += ChooseBox;
            buyBoxes[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < buyBoxes.Count; i++)
        {
            buyBoxes[i].OnChooseBox -= ChooseBox;
            buyBoxes[i].Dispose();
        }
    }

    public void ShowAll()
    {
        buyBoxes.ForEach(bb => bb.Show());
    }

    public void HideAll(int id)
    {
        for (int i = 0; i < buyBoxes.Count; i++)
        {
            if (buyBoxes[i].Id == id)
            {
                buyBoxes[i].Hide(true);
            }
            else
            {
                buyBoxes[i].Hide(false);
            }
        }
    }

    public BuyBox GetBuyBox(int id)
    {
        return buyBoxes.FirstOrDefault(bb => bb.Id == id);
    }

    #region Output

    public event Action<int> OnChooseBox;

    private void ChooseBox(int id)
    {
        OnChooseBox?.Invoke(id);
    }

    #endregion
}

[Serializable]
public class BuyBox
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private Transform transformMain;
    [SerializeField] private Image imageBox;
    [SerializeField] private float durationScaleMain;
    [SerializeField] private float durationScaleBox;
    [SerializeField] private Button buttonBox;

    private Tween tweenScaleMain;
    private Tween tweenScaleChoose;

    public void Initialize()
    {
        buttonBox.onClick.AddListener(ChooseBox);
    }

    public void Dispose()
    {
        buttonBox.onClick.RemoveListener(ChooseBox);
    }

    public void Show()
    {
        tweenScaleMain?.Kill();

        imageBox.transform.localScale = Vector3.one;
        tweenScaleMain = transformMain.DOScale(1, durationScaleMain);
    }

    public void Hide(bool isAbsolute = false)
    {
        tweenScaleMain?.Kill();

        if (isAbsolute)
        {
            transformMain.localScale = Vector3.zero;
        }
        else
        {
            tweenScaleMain = transformMain.DOScale(0, durationScaleMain);
        }
    }

    public void Choose()
    {
        tweenScaleChoose?.Kill();

        tweenScaleChoose = imageBox.transform.DOScale(1.2f, durationScaleBox);
    }

    public void Unchoose()
    {
        tweenScaleChoose?.Kill();

        tweenScaleChoose = imageBox.transform.DOScale(1, durationScaleBox);
    }

    #region Output

    public event Action<int> OnChooseBox;

    private void ChooseBox()
    {
        OnChooseBox?.Invoke(id);
    }

    #endregion
}
