using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Affects/Change AttributeVariable")]
public class ChangeAttributeVar : ItemAffect {
    public AttributeVariable variable;
    public float value_delta;
    public float max_delta;
    public float unavailable_delta;

    public override void TryDo()
    {
        if (variable == null)
            return;
        variable.RuntimeValue       +=  value_delta;
        variable.RuntimeMax         +=  max_delta;
        variable.RuntimeUnavailable +=  unavailable_delta;
    }
}
