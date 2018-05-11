using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour {
    public CrafterSlot[] recipes;
    public BoolVariable lockCraft;   // lock/unlock crafter. if missing assume unlocked
    private bool doCraft;   // currently crafting
    public Inventory inventory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // if the can't craft
        if (lockCraft != null && lockCraft == false)
            return;
		
	}

    [System.Serializable]
    public class CrafterSlot
    {
        public CraftRecipe recipe;
        public float timeCrafting;
        [System.NonSerialized]
        public bool crafting;
        public float TimePercent(float deltaTime)
        {
                return deltaTime / recipe.craft_time;
        }

        public void Craft(Inventory inventory, float deltaTime)
        {
            if (!crafting)
                return;
            timeCrafting += deltaTime;
            int i;
            for (i = 0; i < recipe.needs.Length; i++)
                inventory.RemoveItems(recipe.needs[i].type, recipe.needs[i].amount * TimePercent(deltaTime));
            for (i = 0; i < recipe.makes.Length; i++)
                inventory.AddItems(recipe.makes[i].type, recipe.makes[i].amount * TimePercent(deltaTime));

        }
    }
}
