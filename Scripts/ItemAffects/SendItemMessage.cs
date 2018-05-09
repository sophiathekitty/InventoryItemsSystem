using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Affects/Send Message")]
public class SendItemMessage : ItemAffect
{
    public string findTag;
    public string methodName;

    public override void TryDo()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag(findTag);
        if (gameObject == null)
            return;
        gameObject.SendMessage(methodName);
    }
}
