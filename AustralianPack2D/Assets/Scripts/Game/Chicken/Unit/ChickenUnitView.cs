using System;
using UnityEngine;

public class ChickenUnitView : View
{
    public ChickenUnit_Auto Auto => chickenUnit_Auto;
    public ChickenUnit_UFO UFO => chickenUnit_UFO;
    public ChickenUnit_Tornado Tornado => chickenUnit_Tornado;
    public ChickenUnit_Pigeon Pigeon => chickenUnit_Pigeon;
    public ChickenUnit_Ghost Ghost => chickenUnit_Ghost;
    public ChickenUnit_Hunter Hunter => chickenUnit_Hunter;

    public Vector3 LocalPosition => move.LocalPosition;

    [SerializeField] private ChickenUnit_Move move;
    [SerializeField] private ChickenUnit_Animations animations;

    [SerializeField] private ChickenUnit_Auto chickenUnit_Auto;
    [SerializeField] private ChickenUnit_UFO chickenUnit_UFO;
    [SerializeField] private ChickenUnit_Tornado chickenUnit_Tornado;
    [SerializeField] private ChickenUnit_Pigeon chickenUnit_Pigeon;
    [SerializeField] private ChickenUnit_Ghost chickenUnit_Ghost;
    [SerializeField] private ChickenUnit_Hunter chickenUnit_Hunter;

    public void Initialize()
    {
        move.OnEndMove += EndMove;
        move.Initialize();
    }

    public void Dispose()
    {
        move.OnEndMove -= EndMove;
    }

    #region MOVE

    public void SetTarget(Vector3 target)
    {
        move.SetTarget(target);
    }

    public void SetSpeed(float speed)
    {
        move.SetSpeed(speed);
    }

    public void SetSpeed(float speed, float duration)
    {
        move.SetSpeed(speed, duration);
    }

    public void StartMove()
    {
        move.StartMove();
    }

    public void StopMove()
    {
        move.StopMove();
    }

    #endregion

    #region ANIMATION

    public void Show()
    {
        animations.Show();
    }

    public void HideDestroy()
    {
        animations.HideDestroy();
    }

    public void SetOrder(int order)
    {
        animations.SetOrder(order);
    }

    public void SetSkin(string name)
    {
        animations.SetSkin(name);
    }

    public void ActivateAnimation(ChickenAnimationType animationType)
    {
        animations.ActivateAnimation(animationType);
    }

    #endregion

    #region Output

    public event Action OnEndMove;

    private void EndMove()
    {
        OnEndMove?.Invoke();
    }

    #endregion
}
