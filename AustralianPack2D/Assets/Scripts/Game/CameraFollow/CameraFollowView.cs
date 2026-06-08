using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollowView : View
{
    [SerializeField] private Transform transformPanel;

    private readonly List<IChickenUnit> _chickens = new();

    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float screenCenterX = 0f;

    private IChickenUnit _leader;

    public void SetChickens(List<IChickenUnit> chickens)
    {
        Clear();

        _chickens.AddRange(chickens);

        foreach (var chicken in _chickens)
        {
            chicken.OnEndMove += HandleChickenRemoved;
        }

        RecalculateLeader(true);
    }

    public void ClearTargets()
    {
        Clear();
    }

    private void Update()
    {
        if (_chickens.Count == 0) return;

        RecalculateLeader();

        if (_leader == null) return;


        float leaderX = _leader.LocalPosition.x;

        Vector3 target = new Vector3(screenCenterX - leaderX, 0, 0);

        transformPanel.localPosition = Vector3.Lerp(
            transformPanel.localPosition,
            target,
            Time.deltaTime * followSpeed
        );
    }

    private void HandleChickenRemoved(IChickenUnit chicken)
    {
        if (!_chickens.Contains(chicken))
            return;

        chicken.OnEndMove -= HandleChickenRemoved;

        _chickens.Remove(chicken);

        if (_leader == chicken)
            RecalculateLeader(true);
    }

    private void RecalculateLeader(bool force = false)
    {
        if (_chickens.Count == 0)
        {
            _leader = null;
            return;
        }

        IChickenUnit best = null;
        float bestX = float.MinValue;

        for (int i = 0; i < _chickens.Count; i++)
        {
            var c = _chickens[i];
            float x = c.LocalPosition.x;

            if(c == null) continue;

            if (x > bestX)
            {
                bestX = x;
                best = c;
            }
        }

        _leader = best;
    }

    private void Clear()
    {
        foreach (var c in _chickens)
            c.OnEndMove -= HandleChickenRemoved;

        _chickens.Clear();
        _leader = null;
    }

    private void OnDestroy()
    {
        Clear();
    }
}
