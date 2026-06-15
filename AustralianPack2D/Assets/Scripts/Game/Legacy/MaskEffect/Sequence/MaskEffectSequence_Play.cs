using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskEffectSequence_Play : MonoBehaviour, IMaskEffectSequence
{
    public string Id => id;

    [SerializeField] private string id;
    [SerializeField] private RawImage transformIntroPanel;
    [SerializeField] private RawImage transformPlayPanel;
    [SerializeField] private Transform transformPosFigure;

    private MaskEffectFigure _effectFigure;
    private Material _matPanel;

    public void Enter(Action OnComplete)
    {
        _effectFigure.transform.localScale = Vector3.zero;
        _effectFigure.SetType(MaskFigureType.Square);
        _effectFigure.SetParent(transformPlayPanel.transform);
        _effectFigure.SetPosition(transformPosFigure.transform.localPosition);

        transformIntroPanel.material = _matPanel;
        transformIntroPanel.DOColor(Color.black, 0.5f);
        transformIntroPanel.raycastTarget = false;
        _effectFigure.Show(1f, 4, OnComplete);
    }

    public void Exit()
    {
        transformIntroPanel.DOColor(Color.white, 0.2f);
        transformIntroPanel.raycastTarget = true;
        transformIntroPanel.material = null;
    }

    public void SetData(MaskEffectFigure figure, Material matPanel)
    {
        _effectFigure = figure;
        _matPanel = matPanel;
    }
}
