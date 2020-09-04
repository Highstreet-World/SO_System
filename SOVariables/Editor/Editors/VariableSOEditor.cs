using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(IVariableSO))]
    [CanEditMultipleObjects]
    public abstract class VariableSOEditor<T> : Editor
    {
#if UNITY_EDITOR
        private T ModifyValue = default(T);

        public override void OnInspectorGUI()
        {
            //Draw the defualt inspector options
            DrawDefaultInspector();

            VariableSO<T> script = (VariableSO<T>)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Debugging Options", EditorStyles.centeredGreyMiniLabel);

            EditorGUILayout.LabelField("Current Value: " + script.Value, EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            //Display a int input field and button to add the inputted Value to the current Value
            ModifyValue = GetEditorGUILayoutValue(ModifyValue);

            //  EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
            //  serializedObject.ApplyModifiedProperties();
           // script.se
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (!EqualityComparer<T>.Default.Equals(script.GetValue(), ModifyValue))
            {
                if (GUILayout.Button("Modify"))
                {

                    script.SetValue(ModifyValue);
                }
            }
            EditorGUILayout.EndHorizontal();

            //Display button that resets the Value to the starting Value
            if (GUILayout.Button("Reset Value"))
            {
                if (EditorApplication.isPlaying)
                {
                    script.ResetValue();
                }
            }
            EditorGUILayout.EndVertical();
        }

        public abstract T GetEditorGUILayoutValue(T Value);


#endif

    }
}