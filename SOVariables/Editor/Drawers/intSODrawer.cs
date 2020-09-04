using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(IntSO), true)]
    public class intSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (IntSO)variableSO;
            varRefrence.Value = EditorGUI.IntField(position, label, varRefrence.Value);
        }
    }
}