using System;
using System.Collections.Generic;
using System.Linq;

public class StoreChickenModel
{
    public event Action<List<ChickenType>> OnChooseChickens;

    private readonly Random _random = new();

    private readonly List<ChickenType> _types = new()
    {
        ChickenType.Alien,
        ChickenType.Yellow,
        ChickenType.Blue,
        ChickenType.Business,
        ChickenType.Cowboy,
        ChickenType.Gold,
        ChickenType.Red,
        ChickenType.Samurai,
        ChickenType.White,
        ChickenType.Zombie
    };

    public void ChooseChickens()
    {
        var chosenChickens = _types
            .OrderBy(_ => _random.Next())
            .Take(5)
            .ToList();

        OnChooseChickens?.Invoke(chosenChickens);
    }
}
