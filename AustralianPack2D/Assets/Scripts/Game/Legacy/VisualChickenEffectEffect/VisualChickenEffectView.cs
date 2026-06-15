using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisualChickenEffectView : View
{
    [SerializeField] private MaskEffectFigure figure;
    [SerializeField] private float durationShowHide;
    [SerializeField] private Vector3 offsetPos;

    [SerializeField] private VisualChickenEffectTransforms chickenEffectTransforms;

    [SerializeField] private Transform transformParent;

    public void Activate(int id)
    {
        figure.SetParent(transformParent);
        figure.SetPosition(chickenEffectTransforms.Point(id).localPosition + offsetPos, durationShowHide);
        figure.Show(durationShowHide / 2, 0.1f, () => { figure.Show(durationShowHide/2, 0.22f); } );
        
    }

    public void Deactivate()
    {
        figure.SetParent(transformParent);
        figure.Hide(durationShowHide);
    }
}

[System.Serializable]
public class VisualChickenEffectTransforms
{
    [SerializeField] private List<VisualChickenEffectTransform> visualChickenEffectTransforms = new();

    public Transform Point(int id)
    {
        return visualChickenEffectTransforms.FirstOrDefault(vis => vis.Id == id).Point;
    }
}

[System.Serializable]
public class VisualChickenEffectTransform
{
    [SerializeField] private int id;
    [SerializeField] private Transform point;

    public int Id => id;
    public Transform Point => point;
}
