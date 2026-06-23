using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Cards/Card Pack")]
public class CardPackSO : ScriptableObject
{
    public CardType type;
    public List<CardData> cards;
}

[System.Serializable]
public class CardData
{
    public Sprite sprite;
    public int page;
    public int index;
}
