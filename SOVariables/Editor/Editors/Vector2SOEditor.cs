#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace SO
{
    [CustomEditor(typeof(Vector2SO))]
    [CanEditMultipleObjects]
    public class Vector2SOEditor : VariableSOEditor<Vector2>
    {
        public override Vector2 GetEditorGUILayoutValue(Vector2 Value)
        {
            return EditorGUILayout. Vector2Field("Modify current Value by: ", Value); ;
        }
    }
}
