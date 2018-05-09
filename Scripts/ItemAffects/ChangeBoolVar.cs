using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Affects/Change BoolVariable")]
public class ChangeBoolVar : ItemAffect
{
    public BoolVariable variable;
    public enum ChangeBoolVarMode {On,Off,Toggle}
    public ChangeBoolVarMode mode;
    public override void TryDo()
    {
        if (variable == null)
            return;
        switch (mode)
        {
            case ChangeBoolVarMode.On:
                variable.RuntimeValue = true;
                return;
            case ChangeBoolVarMode.Off:
                variable.RuntimeValue = false;
                return;
            case ChangeBoolVarMode.Toggle:
                variable.RuntimeValue = !variable.RuntimeValue;
                return;
        }
    }
}
