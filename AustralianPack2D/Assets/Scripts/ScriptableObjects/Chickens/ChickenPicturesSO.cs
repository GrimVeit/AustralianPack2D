using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChickenPicturesSO", menuName = "Game/SO/ChickenPictures")]
public class ChickenPicturesSO : ScriptableObject
{
    [SerializeField] private ChickenType type;
    [SerializeField] private List<ChickenPictureSO> pictures;

    public ChickenType Type => type;
    public List<ChickenPictureSO> Pictures => pictures;
}
