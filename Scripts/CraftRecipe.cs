using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Crafting Recipe")]
public class CraftRecipe : ScriptableObject {
    public Sprite icon;
    public string display_name;
    public float craft_time = 1f;
    public bool autoCraft;
    public RecipeItem[] needs;
    public RecipeItem[] makes;

    [System.Serializable]
    public class RecipeItem
    {
        public ItemType type;
        public float amount = 1f;
    }
}
