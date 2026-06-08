using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskEffectView : View
{
    [SerializeField] private List<MonoBehaviour> maskEffectSequenceScripts;
    [SerializeField] private MaskEffectFigure maskEffectFigure;
    [SerializeField] private Material matPanel;

    private List<IMaskEffectSequence> _maskEffectSequenceScripts;

    public void Initialize()
    {
        _maskEffectSequenceScripts = new List<IMaskEffectSequence>();

        foreach (var mb in maskEffectSequenceScripts)
        {
            if (mb is IMaskEffectSequence seq)
            {
                seq.SetData(maskEffectFigure, matPanel);
                _maskEffectSequenceScripts.Add(seq);
            }
        }
    }

    public void Play(string id, Action OnComplete)
    {
        _maskEffectSequenceScripts.Find(x => x.Id == id)?.Enter(OnComplete);
    }

    public void Stop(string id)
    {
        _maskEffectSequenceScripts.Find(x => x.Id == id)?.Exit();
    }
}

public interface IMaskEffectSequence
{
    public  string Id { get; }
    public void SetData(MaskEffectFigure figure, Material matPanel);

    public void Enter(Action OnComplete);
    public void Exit();
}
