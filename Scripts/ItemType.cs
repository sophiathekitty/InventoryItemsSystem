using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item Type")]
public class ItemType : ScriptableObject {
    public enum indefiniateArticle {A, An, None}
    public indefiniateArticle indefinite_article;
    public string display_name;
    public string plural_name;
    public string description;
    public Sprite sprite;
    public float size = 1f;

    public string Name_block
    {
        get
        {
            switch (indefinite_article)
            {
                case indefiniateArticle.A:
                    return "a " + display_name;
                case indefiniateArticle.An:
                    return "an " + display_name;
            }
            return display_name;
        }
    }
}
