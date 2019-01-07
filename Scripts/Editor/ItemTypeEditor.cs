using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ItemType))]
public class ItemTypeEditor : Editor {
    private static string icon = "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";
    public ItemType itemType { get { return target as ItemType; } }
    private const int label_width = 50;


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", GUILayout.Width(label_width));
        itemType.display_name = EditorGUILayout.TextField(itemType.display_name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Sprite", GUILayout.Width(label_width));
        itemType.sprite = (Sprite)EditorGUILayout.ObjectField(itemType.sprite, typeof(Sprite), false);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Size", GUILayout.Width(label_width));
        itemType.size = EditorGUILayout.FloatField(itemType.size);
        GUILayout.Label("Max Stacks", GUILayout.Width(80));
        itemType.max_stack = EditorGUILayout.FloatField(itemType.max_stack);
        GUILayout.EndHorizontal();


        GUILayout.Label("Description:");
        itemType.description = GUILayout.TextArea(itemType.description);

        if(itemType.tags == null)
            itemType.tags = new List<ItemTag>();
        // show tags
        GUILayout.BeginHorizontal();
        for(int t = 0; t < itemType.tags.Count; t++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(false));
            if (itemType.tags[t].sprite != null)
                GUILayout.Label(PreviewUtil.RenderStaticPreview(itemType.tags[t].sprite, 20, 20));
            GUILayout.Label(itemType.tags[t].name);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                itemType.tags.RemoveAt(t);
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();
        }
        ItemTag tt = (ItemTag)EditorGUILayout.ObjectField(null, typeof(ItemTag), false);
        if(tt != null)
        {
            if (!itemType.tags.Contains(tt))
            {
                itemType.tags.Add(tt);
                EditorUtility.SetDirty(target);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.EndVertical();        

        itemType.sprite = (Sprite)EditorGUILayout.ObjectField("", itemType.sprite, typeof(Sprite), false, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.Label("Sentences:");
        GUILayout.BeginHorizontal();
        GUILayout.Label("There is");
        itemType.indefinite_article = (ItemType.indefiniateArticle)EditorGUILayout.EnumPopup(itemType.indefinite_article, GUILayout.Width(50));
        itemType.display_name = EditorGUILayout.TextField(itemType.display_name);
        GUILayout.Label("on the table.");
        GUILayout.EndHorizontal();

        if(itemType.indefinite_article != ItemType.indefiniateArticle.None)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("There are two");
            itemType.plural_name = EditorGUILayout.TextField(itemType.plural_name);
            GUILayout.Label("on the table.");
            GUILayout.EndHorizontal();

        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("There are two units of");
            itemType.plural_name = EditorGUILayout.TextField(itemType.plural_name);
            GUILayout.Label("on the table.");
            GUILayout.EndHorizontal();

        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (itemType.sprite != null)
            return PreviewUtil.RenderStaticPreview(itemType.sprite, width, height);
        foreach(ItemTag t in itemType.tags)
            if(t.sprite != null)
                return PreviewUtil.RenderStaticPreview(t.sprite, width, height);
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
