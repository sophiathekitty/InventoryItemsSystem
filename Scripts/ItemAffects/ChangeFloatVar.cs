using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Affects/Change FloatVariable")]
public class ChangeFloatVar : ItemAffect
{
    public FloatVariable variable;
    public float delta;

    public override void TryDo()
    {
        if (variable == null)
            return;
        variable.RuntimeValue += delta;
    }
}
