using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Inventory")]
public class Inventory : SavableVariable, ISerializationCallbackReceiver {

    public List<ItemSlot> default_items = new List<ItemSlot>();
    [System.NonSerialized()]
    public List<ItemSlot> items = new List<ItemSlot>();
    public float max;
    public List<ItemTag> canHoldTags = new List<ItemTag>();
    public List<ItemType> CanHoldItems = new List<ItemType>();

    public float GetItems(ItemType t, float amount)
    {
        foreach(ItemSlot i in items)
            if(i.itemType == t)
            {
                if (amount > i.count)
                    amount = i.count;
                i.count -= amount;
                return amount;
            }
        return 0f;
    }

    public float AddItems(ItemType t, float amount)
    {
        if (!CanHoldItems.Contains(t))
            return amount;

        foreach (ItemSlot i in items)
            if (i.itemType == t)
            {
                if ((i.count * t.size) + (amount * t.size) > max)
                {
                    return amount;
                }
                i.count += amount;
                return 0f;
            }

        return amount;
    }
    public void RemoveItems(ItemType t, float amount)
    {
        foreach (ItemSlot i in items)
            if (i.itemType == t)
            {
                if (amount > i.count)
                   i.count = 0;
                i.count -= amount;
            }
    }

    public float FillPercent()
    {
        float fill = 0f;
        foreach (ItemSlot i in items)
        {
            fill += (i.count * i.itemType.size);
        }
        if (fill == 0)
            return 0;
        float p = fill / max;
        if (p > 1)
            return 1;
        return p;
    }

    public float FillPercentDefaults()
    {
        float fill = 0f;
        foreach (ItemSlot i in default_items)
        {
            if(i.itemType != null)
                fill += (i.count * i.itemType.size);
        }
        if (fill == 0)
            return 0;
        float p = fill / max;
        return p;
    }

    public float MaxItemCount(ItemType type)
    {
        //float adjustedMax = max / type.size;
        float spaceAvailable = (max * FillPercent()) / type.size;
        float spaceUsed = 0;
        foreach (ItemSlot slot in items)
        {
            if (slot.itemType == type)
                spaceUsed = slot.count * type.size;
        }
        return spaceUsed + spaceAvailable;
    }

    public float MaxItemCountDefaults(ItemType type)
    {
        if (type == null)
            return 0f;
        float spaceAvailable = (max - (max * FillPercentDefaults())) / type.size;
        float spaceUsed = 0;
        foreach (ItemSlot slot in default_items)
        {
            if (slot.itemType == type)
            {
                spaceUsed += slot.count * type.size;
                Debug.Log(slot.itemType.name + ": " + spaceUsed.ToString());
            }
                
        }
        if (spaceUsed + spaceAvailable > type.max_stack)
            return type.max_stack;
        return spaceUsed + spaceAvailable;
    }

    public override string OnSaveData()
    {
        if (items.Count < 1)
            return "";
        string data = items[0].ToString();
        for(int i = 1; i < items.Count; i++)
        {
            data += "|"+items[i].ToString();
        }
        return data;
    }

    public override void OnLoadData(string data)
    {
        if (data == "")
            return;

        items.Clear();
        string[] itms = data.Split('|');
        foreach (string itm in itms)
        {
            items.Add(new ItemSlot(itm,CanHoldItems));
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        items.Clear();
        foreach(ItemSlot slot in default_items)
        {
            items.Add(new ItemSlot(slot));
        }
    }

    [System.Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public float count;
        public ItemSlot() { }
        public ItemSlot(ItemSlot source)
        {
            itemType = source.itemType;
            count = source.count;
        }
        public ItemSlot(ItemType type, float amount = 1)
        {
            itemType = type;
            count = amount;
        }
        public ItemSlot(string itm, List<ItemType> types)
        {
            string[] data = itm.Split(',');
            foreach(ItemType type in types)
            {
                if (type.GetInstanceID().ToString() == data[0])
                    itemType = type;
            }
            count = float.Parse(data[1]);
        }
        public override string ToString()
        {
            return itemType.GetInstanceID().ToString() + "," + count.ToString();
        }
    }
}
