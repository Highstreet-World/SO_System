using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(BoolSO))]
    [CanEditMultipleObjects]
    public class boolSOEditor : VariableSOEditor<bool>
    {
        public override bool GetEditorGUILayoutValue(bool Value)
        {
            EditorGUILayout.LabelField("Modify current Value by");
            return EditorGUILayout.Toggle(Value);
        }
    }
}
