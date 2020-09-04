using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invoke specefic action when a variable Value changes 
/// </summary>
public class InvokeOnValueChange : MonoBehaviour
{
    [Header("ListenTo")]
    public SO.IVariableSO SoVar;
    [Header("Result")]
    public UnityEvent OnChange;
    private void OnEnable()
    {
        SoVar.Subscripe(OnValChanged);
    }
    private void OnDisable()
    {
        SoVar.UnSubscripe(OnValChanged);
    }
    private void OnValChanged(object sender, EventArgs e)
    {
        OnChange.Invoke();
    }
}
