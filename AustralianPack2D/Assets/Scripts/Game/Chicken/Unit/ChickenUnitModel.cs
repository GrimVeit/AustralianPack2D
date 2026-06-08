using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChickenUnitModel
{
    public ChickenType Type => _type;

    private readonly ChickenType _type;

    public ChickenUnitModel(ChickenType type)
    {
        _type = type;
    }

    #region Output

    public event Action OnEndMove;
    public void EndMove()
    {
        OnEndMove?.Invoke();
    }

    #endregion

    #region Move

    public void SetSpeed(float speed)
    {
        OnSetSpeed?.Invoke(speed);
    }

    public void SetSpeed(float speed, float duration)
    {
        OnSetSpeed_Smooth?.Invoke(speed, duration);
    }

    public void StartMove()
    {
        OnStartMove?.Invoke();
    }

    public void StopMove()
    {
        OnStopMove?.Invoke();
    }

    #endregion

    #region ANIMATION

    public void Show()
    {
        OnShow?.Invoke();
    }

    public void Hide()
    {
        OnHide?.Invoke();
    }

    public void ActivateAnimation(ChickenAnimationType animationType)
    {
        OnActivateAnimation?.Invoke(animationType);
    }

    #endregion



    #region EVENT_GAME

    #region AUTO

    public void EventGame_Auto_Activate()
    {
        OnEventsGame_Auto_Activate?.Invoke();
    }

    public void EventGame_Auto_Clear()
    {
        OnEventsGame_Auto_Clear?.Invoke();
    }

    #endregion



    #region UFO

    public void EventGame_UFO_Activate()
    {
        OnEventsGame_UFO_Activate?.Invoke();
    }

    public void EventGame_UFO_Clear()
    {
        OnEventsGame_UFO_Clear?.Invoke();
    }

    #endregion



    #region Tornado

    public void EventGame_Tornado_Activate()
    {
        OnEventsGame_Tornado_Activate?.Invoke();
    }

    public void EventGame_Tornado_Clear()
    {
        OnEventsGame_Tornado_Clear?.Invoke();
    }

    #endregion



    #region Ghost

    public void EventGame_Ghost_Activate()
    {
        OnEventsGame_Ghost_Activate?.Invoke();
    }

    public void EventGame_Ghost_Clear()
    {
        OnEventsGame_Ghost_Clear?.Invoke();
    }

    #endregion

    #region Ghost

    public void EventGame_Hunter_Activate()
    {
        OnEventsGame_Hunter_Activate?.Invoke();
    }

    public void EventGame_Hunter_Clear()
    {
        OnEventsGame_Hunter_Clear?.Invoke();
    }

    #endregion



    #region Pigeon

    public void EventGame_Pigeon_ActivateStart()
    {
        OnEventsGame_Pigeon_ActivateStart?.Invoke();
    }

    public void EventGame_Pigeon_ActivateEnd()
    {
        OnEventsGame_Pigeon_ActivateEnd?.Invoke();
    }

    public void EventGame_Pigeon_Clear()
    {
        OnEventsGame_Pigeon_Clear?.Invoke();
    }

    #endregion


    #endregion

    #region Output

    public event Action<float> OnSetSpeed;
    public event Action<float, float> OnSetSpeed_Smooth;
    public event Action OnStartMove;
    public event Action OnStopMove;

    public event Action OnShow;
    public event Action OnHide;
    public event Action<ChickenAnimationType> OnActivateAnimation;



    public event Action OnEventsGame_Auto_Activate;
    public event Action OnEventsGame_Auto_Clear;

    public event Action OnEventsGame_UFO_Activate;
    public event Action OnEventsGame_UFO_Clear;

    public event Action OnEventsGame_Tornado_Activate;
    public event Action OnEventsGame_Tornado_Clear;

    public event Action OnEventsGame_Ghost_Activate;
    public event Action OnEventsGame_Ghost_Clear;

    public event Action OnEventsGame_Hunter_Activate;
    public event Action OnEventsGame_Hunter_Clear;

    public event Action OnEventsGame_Pigeon_ActivateStart;
    public event Action OnEventsGame_Pigeon_ActivateEnd;
    public event Action OnEventsGame_Pigeon_Clear;

    #endregion
}
