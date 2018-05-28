using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerItemType))]
public class PlayerItemEditor : Editor {

    private const int label_width = 50;
    public override void OnInspectorGUI()
    {

        //base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", GUILayout.Width(label_width));
        playerItem.display_name = EditorGUILayout.TextField(playerItem.display_name);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Sprite", GUILayout.Width(label_width));
        playerItem.sprite = (Sprite)EditorGUILayout.ObjectField(playerItem.sprite, typeof(Sprite), false);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Prefab", GUILayout.Width(label_width));
        playerItem.prefab = (GameObject)EditorGUILayout.ObjectField(playerItem.prefab, typeof(GameObject), false);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Size", GUILayout.Width(label_width));
        playerItem.width = EditorGUILayout.IntField(playerItem.width);
        GUILayout.Label("x");
        playerItem.height = EditorGUILayout.IntField(playerItem.height);
        GUILayout.Label("Max Stacks", GUILayout.Width(80));
        playerItem.max_stack = EditorGUILayout.FloatField(playerItem.max_stack);
        GUILayout.EndHorizontal();

        playerItem.size = playerItem.width * playerItem.height;

        GUILayout.Label("Description:");
        playerItem.description = EditorGUILayout.TextArea(playerItem.description);

        // show tags
        GUILayout.BeginHorizontal();
        for (int t = 0; t < playerItem.tags.Count; t++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(false));
            if (playerItem.tags[t].sprite != null)
                GUILayout.Label(PreviewUtil.RenderStaticPreview(playerItem.tags[t].sprite, 20, 20));
            GUILayout.Label(playerItem.tags[t].name);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                playerItem.tags.RemoveAt(t);
            }
            GUILayout.EndHorizontal();
        }
        ItemTag tt = (ItemTag)EditorGUILayout.ObjectField(null, typeof(ItemTag), false);
        if (tt != null)
        {
            if (!playerItem.tags.Contains(tt))
            {
                EditorUtility.SetDirty(target);
                playerItem.tags.Add(tt);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.EndVertical();
        if (playerItem.width > 3)
            playerItem.width = 3;
        if (playerItem.width < 1)
            playerItem.width = 1;

        if (playerItem.height > 3)
            playerItem.height = 3;
        if (playerItem.height < 1)
            playerItem.height = 1;
        playerItem.sprite = (Sprite)EditorGUILayout.ObjectField("", playerItem.sprite, typeof(Sprite), false, GUILayout.Width(128), GUILayout.Height(128));

        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.Label("Nouns and Verbs:");
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.BeginHorizontal();
        GUILayout.Label("You");
        playerItem.verb_past = EditorGUILayout.TextField(playerItem.verb_past);
        playerItem.indefinite_article = (ItemType.indefiniateArticle)EditorGUILayout.EnumPopup(playerItem.indefinite_article,GUILayout.Width(50));
        playerItem.display_name = EditorGUILayout.TextField(playerItem.display_name);
        GUILayout.Label("today.");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        //GUILayout.Label("You");
        playerItem.verb_present = EditorGUILayout.TextField(playerItem.verb_present);
        GUILayout.Label("some");
        playerItem.plural_name = EditorGUILayout.TextField(playerItem.plural_name);
        //GUILayout.Label("now.");
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("You will");
        playerItem.verb_future = EditorGUILayout.TextField(playerItem.verb_future);
        GUILayout.Label("more");
        playerItem.plural_name = EditorGUILayout.TextField(playerItem.plural_name);
        GUILayout.Label("tomorrow.");
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        EditorGUILayout.Space();

        GUILayout.Label("Use Affects:");
        for(int i = 0; i < playerItem.affect.Count; i++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            GUILayout.Label(playerItem.affect[i].display_name);
            GUILayout.Label(playerItem.affect[i].discription);
            playerItem.affect[i] = (ItemAffect)EditorGUILayout.ObjectField(playerItem.affect[i], typeof(ItemAffect), false, GUILayout.Width(80));
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                playerItem.affect.Remove(playerItem.affect[i]);
                i--;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUILayout.Label("Add Use Affect: ");
        ItemAffect itemAffect = (ItemAffect)EditorGUILayout.ObjectField(null, typeof(ItemAffect), false);
        GUILayout.EndHorizontal();
        if (itemAffect != null)
            playerItem.affect.Add(itemAffect);

        playerItem.name = playerItem.display_name;
    }

    private static string icon = "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";
    public PlayerItemType playerItem { get { return target as PlayerItemType; } }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        if (playerItem.sprite != null)
            return PreviewUtil.RenderStaticPreview(playerItem.sprite, width, height);
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
