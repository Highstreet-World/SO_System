using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(Vector3SO), true)]
    public class Vector3SODrawer : IVariableSODrawer
    {
        protected override bool AllowMultiLine()
        {
            return true;
        }
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (Vector3SO)variableSO;
            varRefrence.Value = EditorGUI.Vector3Field(position, label, varRefrence.Value);
        }
    }
}