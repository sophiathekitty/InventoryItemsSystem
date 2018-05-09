using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Affects/Change IntVariable")]
public class ChangeIntVar : ItemAffect
{
    public IntVariable variable;
    public int delta;

    public override void TryDo()
    {
        if (variable == null)
            return;
        variable.RuntimeValue += delta;
    }
}
