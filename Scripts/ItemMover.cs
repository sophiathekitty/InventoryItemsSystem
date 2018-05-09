using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour {
    public BoolVariable active;
    public Inventory source;
    public Inventory destination;
    public float transferRate = 1f;

    private ItemType[] types;
	// Use this for initialization
	void Start () {
        types = source.AllTypes();
	}
	
	// Update is called once per frame
	void Update () {
        if (active.RuntimeValue)
        {
            foreach(ItemType t in types)
            {
                float amount = source.GetItems(t, Time.deltaTime * transferRate);
                Debug.Log(amount);
                float overflow = destination.AddItems(t, amount);
                Debug.Log(overflow);
                if (overflow > 0f)
                    source.AddItems(t, overflow);
            }
        }
	}
}
