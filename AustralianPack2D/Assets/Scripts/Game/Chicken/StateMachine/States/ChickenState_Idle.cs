using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState_Idle : IState
{
    private readonly ChickenUnitModel _model;

    public ChickenState_Idle(ChickenUnitModel model)
    {
        _model = model;
    }

    public void EnterState()
    {
        _model.SetSpeed(0, 0.1f);
        _model.ActivateAnimation(ChickenAnimationType.Idle);
    }

    public void ExitState()
    {

    }
}
