using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinSprite 
{
    public Sprite sprite;
    public string name;
    public int cost;
    [HideInInspector] 
    public bool isBought;
}
