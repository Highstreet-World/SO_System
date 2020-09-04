using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(StringSO))]
    [CanEditMultipleObjects]
    public class stringSOEditor : VariableSOEditor<string>
    {
        public override string GetEditorGUILayoutValue(string Value)
        {
            Value = EditorGUILayout.TextField("Modify Value: ", Value);

            return Value;
        }
    }
}
