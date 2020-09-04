using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(GameObjectSO), true)]
    public class GameObjectSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, IVariableSO variableSO)
        {
            var varRefrence = (GameObjectSO)variableSO;
            varRefrence.Value = (GameObject)EditorGUI.ObjectField(position, label, varRefrence.Value, typeof(GameObject), true);
        }
    }
}