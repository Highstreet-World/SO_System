using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(Vector2SO), true)]
    public class Vector2SODrawer : IVariableSODrawer
    {
        protected override bool AllowMultiLine()
        {
            return true;
        }
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (Vector2SO)variableSO;
            varRefrence.Value = EditorGUI.Vector2Field(position, label, varRefrence.Value);
        }
    }
}