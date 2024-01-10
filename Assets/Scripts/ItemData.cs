using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
 public Material material;
 public int itemID;
 public Sprite image;
 public string itemName;
}

