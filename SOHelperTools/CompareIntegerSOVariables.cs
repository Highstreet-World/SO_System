using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;
using UnityEngine.Events;

public class CompareIntegerSOVariables : MonoBehaviour
{

    public enum CompareRules
    {
        Equal, NotEqual, Larger, LargerEqual, Less, LessEqual
    }

    [Header("CompareRule")]
    public IntSO var1;
    public CompareRules compareRules;
    public IntSO var2_SO;
    public int var2_int;
    [Header("settings")]
    public bool AutoListen = true;
    [Header("IfTheRuleApplied")]
    public UnityEvent OnValid;


    //private
    int val1;
    int val2;

    private void OnEnable()
    {
        if (AutoListen)
        {
            var1.Subscripe(OnValChange);
            var2_SO.Subscripe(OnValChange);
        }
    }
    private void OnDisable()
    {
        {
            if (AutoListen) { }
            var1.UnSubscripe(OnValChange);
            var2_SO.UnSubscripe(OnValChange);
        }
    }

    public void CheckIfRuleApply()
    {
        val1 = (var1 != null) ? var1.Value : 0;
        val2 = (var2_SO != null) ? var2_SO.Value : var2_int;
        switch (compareRules)
        {
            case CompareRules.Equal:
                if (val1 == val2) OnValid.Invoke();
                break;
            case CompareRules.NotEqual:
                if (val1 != val2) OnValid.Invoke();
                break;
            case CompareRules.Larger:
                if (val1 > val2) OnValid.Invoke();
                break;
            case CompareRules.LargerEqual:
                if (val1 >= val2) OnValid.Invoke();
                break;
            case CompareRules.Less:
                if (val1 < val2) OnValid.Invoke();
                break;
            case CompareRules.LessEqual:
                if (val1 <= val2) OnValid.Invoke();
                break;
            default:
                break;
        }
    }
    private void OnValChange(object sender, EventArgs e)
    {
        CheckIfRuleApply();
    }
}
