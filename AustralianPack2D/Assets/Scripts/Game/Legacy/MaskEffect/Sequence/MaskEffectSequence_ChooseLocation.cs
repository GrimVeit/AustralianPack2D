using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskEffectSequence_ChooseLocation : MonoBehaviour, IMaskEffectSequence
{
    public string Id => id;

    [SerializeField] private string id;
    [SerializeField] private RawImage transformPlayPanel;
    [SerializeField] private Transform transformChooseLocationPanel;
    [SerializeField] private Transform transformPosFigure;

    private MaskEffectFigure _effectFigure;
    private Material _matPanel;

    public void Enter(Action OnComplete)
    {
        _effectFigure.transform.localScale = Vector3.zero;
        _effectFigure.SetType(MaskFigureType.Square);
        _effectFigure.SetParent(transformChooseLocationPanel.transform);
        _effectFigure.SetPosition(transformPosFigure.transform.localPosition);

        transformPlayPanel.material = _matPanel;
        transformPlayPanel.raycastTarget = false;
        transformPlayPanel.DOColor(Color.black, 1f);
        _effectFigure.Show(2f, 4, OnComplete);
    }

    public void Exit()
    {
        transformPlayPanel.raycastTarget = true;
        transformPlayPanel.DOColor(Color.white, 0.2f);
        transformPlayPanel.material = null;
    }

    public void SetData(MaskEffectFigure figure, Material matPanel)
    {
        _effectFigure = figure;
        _matPanel = matPanel;
    }
}
