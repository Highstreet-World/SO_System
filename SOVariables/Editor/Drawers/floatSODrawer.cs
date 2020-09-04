using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(FloatSO), true)]
    public class floatSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (FloatSO)variableSO;
            varRefrence.Value = EditorGUI.FloatField(position, label, varRefrence.Value);
        }
    }
}