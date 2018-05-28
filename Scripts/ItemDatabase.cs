using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database")]
public class ItemDatabase : ScriptableObject {
    public List<ItemType> types;
    public List<Inventory> inventories;
    public List<ItemMover> movers;
    public List<Crafter> crafters;
}
