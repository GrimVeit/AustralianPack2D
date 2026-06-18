using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsOrchestrationPresenter
{
    private readonly CardsOrchestrationModel _model;

    public CardsOrchestrationPresenter(CardsOrchestrationModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _model.Dispose();
    }
}
