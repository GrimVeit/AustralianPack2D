using System;
using System.Collections.Generic;

public class CameraFollowModel
{
    private readonly ISpawnerChickenListener _spawnerChickenListener;

    public CameraFollowModel(ISpawnerChickenListener spawnerChickenListener)
    {
        _spawnerChickenListener = spawnerChickenListener;

        _spawnerChickenListener.OnSpawnChickens += SetChickens;
    }

    public void Dispose()
    {
        _spawnerChickenListener.OnSpawnChickens -= SetChickens;
    }

    #region Output

    public event Action<List<IChickenUnit>> OnSetChickens;

    private void SetChickens(List<IChickenUnit> chickens)
    {
        OnSetChickens?.Invoke(chickens);
    }

    #endregion
}
