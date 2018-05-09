using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public ItemSlot[] items;
    public float max;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ItemType[] AllTypes()
    {
        ItemType[] c = new ItemType[items.Length];
        for (int i = 0; i < items.Length; i++)
            c[i] = items[i].itemType;
        return c;
    }

    public float GetItems(ItemType t, float ammount)
    {
        foreach(ItemSlot i in items)
            if(i.itemType == t)
            {
                if (ammount > i.count.RuntimeValue)
                    ammount = i.count.RuntimeValue;
                i.count.RuntimeValue -= ammount;
                return ammount;
            }
        return 0f;
    }

    public float AddItems(ItemType t, float amount)
    {
        foreach (ItemSlot i in items)
            if (i.itemType == t)
            {
                if ((i.count.RuntimeValue * t.size) + (amount * t.size) > max)
                {
                    return amount;
                }
                i.count.RuntimeValue += amount;
                return 0f;
            }

        return amount;
    }

    public float fillPercent()
    {
        float fill = 0f;
        foreach (ItemSlot i in items)
        {
            fill += (i.count.RuntimeValue * i.itemType.size);
        }
        if (fill == 0)
            return 0;
        float p = fill / max;
        if (p > 1)
            return 1;
        return p;
    }

    [System.Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public FloatVariable count;

    }
}
