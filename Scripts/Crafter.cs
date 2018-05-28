using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : ScriptableObject {
    public CrafterSlot[] recipes;
    public BoolVariable lockCraft;   // lock/unlock crafter. if missing assume unlocked
    private bool doCraft;   // currently crafting
    public Inventory inventory;

	void Update () {
        // if the can't craft
        if (lockCraft != null && lockCraft.RuntimeValue == false)
            return;

        // go through all the recipies and work on crafting them
        foreach (CrafterSlot recipe in recipes)
        {
            recipe.Craft(inventory, Time.deltaTime);
        }
	}

    [System.Serializable]
    public class CrafterSlot
    {
        public CraftRecipe recipe;
        [System.NonSerialized]
        public float timeCrafting;
        [System.NonSerialized]
        public bool crafting;
        // what percent of the craf time does this delta time represent
        public float TimePercent(float deltaTime)
        {
                return deltaTime / recipe.craft_time;
        }
        // craft a recipe
        public void Craft(Inventory inventory, float deltaTime)
        {
            // return if we're not crafting this recipe
            if (!crafting && !recipe.autoCraft)
                return;
            // add delta t
            timeCrafting += deltaTime;
            // use up the needs
            for (int i = 0; i < recipe.needs.Length; i++)
                inventory.RemoveItems(recipe.needs[i].type, recipe.needs[i].amount * TimePercent(deltaTime));
            // add the makes
            for (int i = 0; i < recipe.makes.Length; i++)
                inventory.AddItems(recipe.makes[i].type, recipe.makes[i].amount * TimePercent(deltaTime));
            // are we done?
            if(timeCrafting >= recipe.craft_time)
            {
                timeCrafting = 0;
                if (!recipe.autoCraft)
                    crafting = false;
            }
        }
    }
}
