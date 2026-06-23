using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Cards/Card Packs")]
public class CardPacksSO : ScriptableObject
{
    public List<CardPackSO> CardPackSOs = new();
}
