using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Player Item Type")]
public class PlayerItemType : ItemType {
    public string verb_future = "use";
    public string verb_present = "use";
    public string verb_past = "used";
    public int width = 1;
    public int height = 1;
    public List<ItemAffect> affect;
}
