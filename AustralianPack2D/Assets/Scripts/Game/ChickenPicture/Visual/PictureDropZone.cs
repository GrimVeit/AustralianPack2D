using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureDropZone : MonoBehaviour, IPictureDropZone
{
    public ChickenType Type => _type;
    public int IdZone => idZone;


    [SerializeField] private int idZone;
    private ChickenType _type;

    public void SetType(ChickenType type)
    {
        _type = type;
    }
}
