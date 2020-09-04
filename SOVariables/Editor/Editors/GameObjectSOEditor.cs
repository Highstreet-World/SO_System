#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace SO
{
    [CustomEditor(typeof(GameObjectSO))]
    [CanEditMultipleObjects]
    public class GameObjectSOEditor : VariableSOEditor<GameObject>
    {
        public override GameObject GetEditorGUILayoutValue(GameObject Value)
        {
             return (GameObject) EditorGUILayout.ObjectField(new GUIContent("Modify current Value by: "), Value,typeof(GameObject),true); ;
        }
    }
}
