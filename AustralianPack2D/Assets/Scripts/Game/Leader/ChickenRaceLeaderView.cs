using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChickenRaceLeaderView : View
{
    [SerializeField] private Transform transformMain;
    [SerializeField] private Transform transformLeader;
    [SerializeField] private Image imageLeader;
    [SerializeField] private TextMeshProUGUI textLeader;
    [SerializeField] private ChickenSpriteNames chickenSpriteNames;

    private Tween tweenScaleMain;
    private Tween tweenScaleLeader;

    public void Activate()
    {
        tweenScaleMain?.Kill();

        tweenScaleMain = transformMain.DOScale(1, 0.15f).SetEase(Ease.OutBack);
    }

    public void Deactivate()
    {
        tweenScaleLeader?.Kill();
        tweenScaleMain?.Kill();

        tweenScaleMain = transformMain.DOScale(0, 0.15f).SetEase(Ease.InBack).OnComplete(() => 
        {
            transformLeader.localScale = Vector3.zero;
        });
    }

    public void SetLeader(IChickenUnit unit)
    {
        tweenScaleLeader?.Kill();

        transformLeader.localScale = Vector3.zero;


        var spriteName = chickenSpriteNames.GetChickenSpriteName(unit.Type);

        if (spriteName == null)
        {
            Debug.LogError("Not found ChickenSpriteName with ChickenType - " + unit.Type);
            return;
        }


        transformLeader.DOScale(1.2f, 0.15f).SetEase(Ease.OutBack);

        imageLeader.sprite = spriteName.Sprite;
        textLeader.text = spriteName.Name;
    }

    [Serializable]
    private class ChickenSpriteNames
    {
        [SerializeField] private List<ChickenSpriteName> chickenSprites = new();

        public ChickenSpriteName GetChickenSpriteName(ChickenType type)
        {
            return chickenSprites.Find(data => data.Type == type);
        }
    }

    [Serializable]
    private class ChickenSpriteName
    {
        [SerializeField] private ChickenType type;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string name;

        public ChickenType Type => type;
        public Sprite Sprite => sprite;
        public string Name => name;
    }
}
