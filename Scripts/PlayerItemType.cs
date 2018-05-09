using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Player Item Type")]
public class PlayerItemType : ItemType {
    public GameObject prefab;
    public int width = 1;
    public int height = 1;
    public ItemAffect[] affect;
}
