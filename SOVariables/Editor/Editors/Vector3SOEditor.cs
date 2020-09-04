#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace SO
{
    [CustomEditor(typeof(Vector3SO))]
    [CanEditMultipleObjects]
    public class Vector3SOEditor : VariableSOEditor<Vector3>
    {
        public override Vector3 GetEditorGUILayoutValue(Vector3 Value)
        {
            return EditorGUILayout. Vector3Field("Modify current Value by: ", Value); ;
        }
    }
}
