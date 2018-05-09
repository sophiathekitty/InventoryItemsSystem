using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAffect : ScriptableObject {
    public string display_name;
    public string discription;
    public abstract void TryDo();
}
