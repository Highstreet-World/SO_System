using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(StringSO), true)]
    public class stringSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (StringSO)variableSO;
            varRefrence.Value = EditorGUI.TextField(position, label, varRefrence.Value);
        }
    }
}