using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskEffectSequence_StartGame : MonoBehaviour, IMaskEffectSequence
{
    public string Id => id;

    [SerializeField] private string id;
    [SerializeField] private Image transformChooseLocationPanel;
    [SerializeField] private List<Image> imageList;
    [SerializeField] private Transform transformGamePanel;
    [SerializeField] private Transform transformPosFigure;

    private MaskEffectFigure _effectFigure;
    private Material _matPanel;

    public void Enter(Action OnComplete)
    {
        _effectFigure.transform.localScale = Vector3.zero;
        _effectFigure.SetType(MaskFigureType.Square);
        _effectFigure.SetParent(transformGamePanel.transform);
        _effectFigure.SetPosition(transformPosFigure.transform.localPosition);

        for (int i = 0; i < imageList.Count; i++)
        {
            imageList[i].material = _matPanel;
        }

        transformChooseLocationPanel.material = _matPanel;
        transformChooseLocationPanel.DOColor(Color.black, 0.5f);
        _effectFigure.Show(1f, 4, OnComplete);
    }

    public void Exit()
    {
        transformChooseLocationPanel.DOColor(Color.white, 0.2f);
        transformChooseLocationPanel.material = null;

        for (int i = 0; i < imageList.Count; i++)
        {
            imageList[i].material = null;
        }
    }

    public void SetData(MaskEffectFigure figure, Material matPanel)
    {
        _effectFigure = figure;
        _matPanel = matPanel;
    }
}
