using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cards", menuName = "Cards")]
public class Cards : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite icon;
    public int cardID;
}
