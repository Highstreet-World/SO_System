using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    //  [CustomPropertyDrawer(typeof(SOEnumVariable<T>), true)]
    public class SOEnumVariableDrawer<SOEnumVariable, T> : IVariableSODrawer where SOEnumVariable : IVariableSO where T : System.Enum
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var enumStringVal = variableSO.ToString();
            var originalValue = (T)Enum.Parse(typeof(T), enumStringVal);
            var newValue = EditorGUI.EnumPopup(position, label, originalValue); ;

            variableSO.SetValue(Enum.GetName(typeof(T), newValue));
        }
    }
}