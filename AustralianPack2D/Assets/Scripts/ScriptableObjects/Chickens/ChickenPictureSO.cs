using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChickenPictureSO
{
    [SerializeField] private List<Sprite> pieces;

    public List<Sprite> Pieces => pieces;
}
