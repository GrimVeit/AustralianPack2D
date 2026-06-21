using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDesignBuyVisualView : View
{
    [SerializeField] private List<CardDesignBuyVisual> designVisuals = new();

    [SerializeField] private float timeOpenCloseShadow;
    [SerializeField] private float timeOpenClosePrice;
    [SerializeField] private float timeSelectDeselectCheck;
    [SerializeField] private float timeChooseUnchoose;

    public void Initialize()
    {
        for (int i = 0; i < designVisuals.Count; i++)
        {
            designVisuals[i].OnChoose += ChooseVisual;
            designVisuals[i].OnBuy += BuyVisual;
            designVisuals[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < designVisuals.Count; i++)
        {
            designVisuals[i].OnChoose -= ChooseVisual;
            designVisuals[i].OnBuy -= BuyVisual;
            designVisuals[i].Dispose();
        }
    }

    public void Open(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Open(timeOpenCloseShadow, timeOpenClosePrice);
    }

    public void Close(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Close(timeOpenCloseShadow, timeOpenClosePrice);
    }

    public void Select(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Select(timeSelectDeselectCheck);
    }

    public void Deselect(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Deselect(timeSelectDeselectCheck);
    }

    public void Choose(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Choose(timeChooseUnchoose);
    }

    public void Unchoose(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Unchoose(timeChooseUnchoose);
    }

    private CardDesignBuyVisual GetDesignVisual(int id)
    {
        return designVisuals.Find(v => v.Id == id);
    }

    #region Output

    public event Action<int, bool> OnChoose;
    public event Action<int, int> OnBuy;

    private void ChooseVisual(int id)
    {
        OnChoose?.Invoke(id, true);
    }

    private void BuyVisual(int id, int price)
    {
        OnBuy?.Invoke(id, price);
    }

    #endregion
}
