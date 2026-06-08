using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskEffectSequence_Intro : MonoBehaviour, IMaskEffectSequence
{
    public string Id => id;

    [SerializeField] private string id;
    [SerializeField] private Image transformBlackPanel;
    [SerializeField] private RawImage transformIntroPanel;
    [SerializeField] private Transform transformPosFigure;

    private MaskEffectFigure _effectFigure;
    private Material _matPanel;

    public void Enter(Action OnComplete)
    {
        _effectFigure.transform.localScale = Vector3.zero;
        _effectFigure.SetType(MaskFigureType.Square);
        _effectFigure.SetParent(transformIntroPanel.transform);
        _effectFigure.SetPosition(transformPosFigure.transform.localPosition);

        transformBlackPanel.material = _matPanel;
        _effectFigure.Show(1f, 4, OnComplete);
    }

    public void Exit()
    {
        transformBlackPanel.material = null;
    }

    public void SetData(MaskEffectFigure figure, Material matPanel)
    {
        _effectFigure = figure;
        _matPanel = matPanel;
    }
}
