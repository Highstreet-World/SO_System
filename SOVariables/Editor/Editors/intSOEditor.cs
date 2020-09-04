using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(IntSO))]
    [CanEditMultipleObjects]
    public class intSOEditor : VariableSOEditor<int>
    {
        public override int GetEditorGUILayoutValue(int Value)
        {
           return EditorGUILayout.IntField("Modify current Value by: ", Value); ;
        }
    }
}