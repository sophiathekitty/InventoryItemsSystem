using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public FloatVariable CraftRate;
    public FloatVariable MoveRate;
    public ItemMover[] Movers;
    public Crafter[] Crafters;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(ItemMover mover in Movers)
            mover.DoUpdate(Time.deltaTime * MoveRate.RuntimeValue);
        foreach(Crafter crafter in Crafters)
            crafter.DoUpdate(Time.deltaTime * CraftRate.RuntimeValue);
    }
}
