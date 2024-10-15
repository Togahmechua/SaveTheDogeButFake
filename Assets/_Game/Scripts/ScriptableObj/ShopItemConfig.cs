using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ScriptableObjects/ShopItem", order = 1)]
public class ShopItemConfig : ScriptableObject
{
    public string itemName;
    public int id;
    public Sprite spr;
    public Sprite bgr;
}
