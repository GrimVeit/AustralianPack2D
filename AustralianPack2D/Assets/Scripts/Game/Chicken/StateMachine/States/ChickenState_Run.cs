using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenState_Run : IState
{
    private readonly ChickenUnitModel _model;

    public ChickenState_Run(ChickenUnitModel model)
    {
        _model = model;
    }

    public void EnterState()
    {
        _model.SetSpeed(Random.Range(300, 350), 0.1f);
        _model.ActivateAnimation(ChickenAnimationType.Run);
        _model.StartMove();
    }

    public void ExitState()
    {

    }
}
