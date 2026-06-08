using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChickenAllPicturesSO", menuName = "Game/SO/ChickenAllPictures")]
public class ChickenAllPicturesSO : ScriptableObject
{
    [SerializeField] private List<ChickenPicturesSO> chickenTypePictures;

    public List<ChickenPicturesSO> TypePictures => chickenTypePictures;
}
