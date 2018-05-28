using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Mover")]
public class ItemMover : ScriptableObject {
    public BoolVariable active;
    public Inventory source;
    public Inventory destination;
    public float transferRate = 1f;

    public List<ItemType> types;
	
	// Update is called once per frame
	public void Update () {
        if (active.RuntimeValue)
        {
            foreach(ItemType t in types)
            {
                float amount = source.GetItems(t, Time.deltaTime * transferRate);
                float overflow = destination.AddItems(t, amount);
                if (overflow > 0f)
                    overflow = source.AddItems(t, overflow);
                if (overflow > 0f)
                    Debug.Log("["+name+"]Mover Overflow: "+overflow.ToString());
            }
        }
	}
}
