using System;
using System.Collections.Generic;

public class StoreChickenPresenter : IStoreChickenListener, IStoreChickenProvider
{
    private readonly StoreChickenModel _model;

    public StoreChickenPresenter(StoreChickenModel model)
    {
        _model = model;
    }

    #region Output

    public event Action<List<ChickenType>> OnChooseChickens
    {
        add => _model.OnChooseChickens += value;
        remove => _model.OnChooseChickens -= value;
    }

    #endregion

    #region Input

    public void ChooseChickens() => _model.ChooseChickens();

    #endregion
}

public interface IStoreChickenProvider
{
    public void ChooseChickens();
}

public interface IStoreChickenListener
{
    public event Action<List<ChickenType>> OnChooseChickens;
}
