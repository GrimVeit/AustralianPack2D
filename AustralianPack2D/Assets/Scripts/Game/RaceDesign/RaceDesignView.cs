using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaceDesignView : View
{
    [SerializeField] private Image imageRace;
    [SerializeField] private RacesDesign racesDesign;

    public void SetLocation(int loco)
    {
        imageRace.sprite = racesDesign.GetSprite(loco);
    }
}

[System.Serializable]
public class RacesDesign
{
    [SerializeField] private List<RaceDesign> raceDesigns = new List<RaceDesign>();

    public Sprite GetSprite(int id)
    {
        return raceDesigns.FirstOrDefault(data => data.Id == id).SpriteRace;
    }
}

[System.Serializable]
public class RaceDesign
{
    [SerializeField] private int id;
    [SerializeField] private Sprite raceSprite;

    public Sprite SpriteRace => raceSprite;
    public int Id => id;

}
