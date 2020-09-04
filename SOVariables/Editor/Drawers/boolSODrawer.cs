using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(BoolSO), true)]
    public class boolSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (BoolSO)variableSO;
            varRefrence.Value = EditorGUI.Toggle(position, label, varRefrence.Value);
        }
    }
}