using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SharedVariableSaveSystem;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {

    private bool showSetting;
    private bool showRuntime;
    private Inventory inventory { get { return target as Inventory; } }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        if (showSetting)
        {
            if (GUILayout.Button("Contents", GUILayout.Width(100)))
                showSetting = false;
            GUILayout.Box("Settings",GUILayout.ExpandWidth(true));
        }
        else
        {
            GUILayout.Box("Contents", GUILayout.ExpandWidth(true));
            if (GUILayout.Button("Settings", GUILayout.Width(100)))
                showSetting = true;
        }
        GUILayout.EndHorizontal();

        if (showSetting)
            DrawSettings();
        else
            DrawContents();


    }


    private void DrawSettings()
    {
        inventory.max = EditorGUILayout.Slider("Storage Space",inventory.max, 1, 1000);
        EditorGUILayout.Space();
        GUILayout.Label("Content Filter:",EditorStyles.largeLabel);
        GUILayout.Label("Tags:");

        // show tags
        GUILayout.BeginHorizontal();
        for (int t = 0; t < inventory.canHoldTags.Count; t++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(false));
            if (inventory.canHoldTags[t].sprite != null)
                GUILayout.Label(PreviewUtil.RenderStaticPreview(inventory.canHoldTags[t].sprite, 20, 20));
            GUILayout.Label(inventory.canHoldTags[t].name);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                inventory.canHoldTags.RemoveAt(t);
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();
        }
        ItemTag tt = (ItemTag)EditorGUILayout.ObjectField(null, typeof(ItemTag), false);
        if (tt != null)
        {
            if (!inventory.canHoldTags.Contains(tt))
            {
                inventory.canHoldTags.Add(tt);
                EditorUtility.SetDirty(target);
            }
        }
        GUILayout.EndHorizontal();


        EditorGUILayout.Space();
        GUILayout.Label("Item Types:");
        // show types
        foreach(ItemType itm in inventory.CanHoldItems)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            if(itm.sprite != null)
                GUILayout.Label(PreviewUtil.RenderStaticPreview(itm.sprite,20,20));
            GUILayout.Label(itm.display_name);
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Search for Items with Tags"))
            FindItemTypes();

    }

    private void FindItemTypes()
    {
        Debug.Log("InventoryEditor::FindItemTypes()");
        string[] ass = AssetDatabase.FindAssets("t:ItemType");
        Debug.Log(ass.Length);
        foreach (string a in ass)
        {
            Debug.Log(a);
            ItemType itm = (ItemType)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(a), typeof(ItemType));
            bool shouldHave = false;
            foreach (ItemTag t in inventory.canHoldTags)
                if (itm.tags.Contains(t))
                {
                    shouldHave = true;
                    break;
                }
            Debug.Log(itm.display_name + ": " + shouldHave.ToString());
            if (shouldHave && !inventory.CanHoldItems.Contains(itm))
                inventory.CanHoldItems.Add(itm);
            else if (!shouldHave && inventory.CanHoldItems.Contains(itm))
                inventory.CanHoldItems.Remove(itm);
        }
        EditorUtility.SetDirty(target);
    }

    private void DrawContents()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Contents:",GUILayout.ExpandWidth(false));
        if (showRuntime)
        {
            if (GUILayout.Button("Default",GUILayout.Width(100)))
                showRuntime = false;
            GUILayout.Box("Runtime", GUILayout.Width(100));
        }
        else
        {
            GUILayout.Box("Default", GUILayout.Width(100));
            if (GUILayout.Button("Runtime", GUILayout.Width(100)))
                showRuntime = true;
        }
        GUILayout.EndHorizontal();
        if (showRuntime)
            DrawRuntimeContents();
        else
            DrawDefaultContents();

    }
    private void DrawDefaultContents()
    {
        GUILayout.Label("Space: " + (inventory.FillPercentDefaults() * inventory.max).ToString() + " / " + inventory.max.ToString());
        for(int i = 0; i < inventory.default_items.Count; i++)
        {
            if(inventory.default_items[i].itemType == null)
            {
                inventory.default_items.RemoveAt(i);
                if (i >= inventory.default_items.Count)
                    break;
            }
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            if (inventory.default_items[i].itemType.sprite == null)
                GUILayout.Label(new GUIContent(inventory.default_items[i].itemType.display_name,Base64ToTexture(item_icon)),GUILayout.Height(20), GUILayout.Width(150));
            else
                GUILayout.Label(new GUIContent(inventory.default_items[i].itemType.display_name, PreviewUtil.RenderStaticPreview(inventory.default_items[i].itemType.sprite,20,20)), GUILayout.Width(150));
            GUILayout.Box(inventory.default_items[i].itemType.size.ToString());
            inventory.default_items[i].itemType = (ItemType)EditorGUILayout.ObjectField(inventory.default_items[i].itemType, typeof(ItemType), false, GUILayout.Width(50));
            inventory.default_items[i].count = EditorGUILayout.Slider(inventory.default_items[i].count, 1, inventory.MaxItemCountDefaults(inventory.default_items[i].itemType));
            if (GUILayout.Button("X",GUILayout.Width(20)))
            {
                inventory.default_items.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        ItemType item = (ItemType)EditorGUILayout.ObjectField("Add New Item: ",null, typeof(ItemType), false);
        if(item != null)
        {
            bool isNew = true;
            foreach (Inventory.ItemSlot slot in inventory.default_items)
                if (slot.itemType == item && slot.count+1 < slot.itemType.max_stack && isNew)
                {
                    isNew = false;
                    slot.count++;
                    break;
                }

            if (isNew)
            {
                inventory.default_items.Add(new Inventory.ItemSlot(item));
                EditorUtility.SetDirty(inventory);
            }
        }
    }
    private void DrawRuntimeContents()
    {
        foreach (Inventory.ItemSlot slot in inventory.items)
        {

        }
    }

    private static string item_icon = "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";
    private static string icon = "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";

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        return Base64ToTexture(icon);
    }

    private static Texture2D Base64ToTexture(string base64)
    {
        Texture2D t = new Texture2D(1, 1)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        t.LoadImage(System.Convert.FromBase64String(base64));
        return t;
    }
}
