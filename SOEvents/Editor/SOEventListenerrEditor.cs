using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;
using UnityEditor.Rendering;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace SO
{
    [CustomEditor(typeof(EventListenerSO))]
    [CanEditMultipleObjects]
    public class GameEventListenerEditor : Editor
    {
#if UNITY_EDITOR
        int newSize = 0;
        EventListenerSO script;
        private ReorderableList eventList;
        private int index = -1;
        private void OnEnable()
        {
            script = (EventListenerSO)target;
            newSize = script.listeners.Count;
            eventList = new ReorderableList(serializedObject, serializedObject.FindProperty("listeners"), true, true, true, true);
            var list = script.listeners;
            var serialisedList = serializedObject.FindProperty("listeners");
            eventList.elementHeight = EditorGUIUtility.singleLineHeight;

            eventList.drawElementCallback = (rect, i, isActive, isFocused) =>
            {
                if (isActive)
                {       
                    index = i;
                }

                serialisedList.GetArrayElementAtIndex(i).isExpanded = false;
                var soEvent = list[i].Event;
                var evName = soEvent != null ? soEvent.name : ("Empty" + i);
                EditorGUI.PropertyField(rect,serialisedList.GetArrayElementAtIndex(i),  new GUIContent(evName));
            };
        }

        public override void OnInspectorGUI()
        {

            SO.SO_SystemSettings.Inistance.EventSOListenerDefultView = GUILayout.Toggle(SO.SO_SystemSettings.Inistance.EventSOListenerDefultView, new GUIContent("Expand All"));
            if (SO.SO_SystemSettings.Inistance.EventSOListenerDefultView)
            {
                //DrawDefaultInspector();
                EditorGUILayout.Separator();
                script = (EventListenerSO)target;
                serializedObject.Update();
                ShowList(serializedObject.FindProperty("listeners"), script.listeners, true, false);
                serializedObject.ApplyModifiedProperties();
            }
            else
            {

                index = -1;
                eventList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
                if (index >= 0)
                {
                    EditorGUILayout.Separator();
                    var serialisedList = serializedObject.FindProperty("listeners");
                    serialisedList.GetArrayElementAtIndex(index).isExpanded = true;
                    EditorGUILayout.PropertyField(serialisedList.GetArrayElementAtIndex(index));
                    serializedObject.ApplyModifiedProperties();
                }


            }
            EditorGUILayout.Separator();
            GuiLine(1);
            GuiLine(1);
        }
        void GuiLine(int i_height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
        public void ShowList(SerializedProperty serialisedList, List<SOListener> list, bool showListSize = true, bool showListLabel = true)
        {
            if (showListLabel)
            {
                EditorGUILayout.PropertyField(serialisedList);
                EditorGUI.indentLevel += 1;
            }
            if (!showListLabel || serialisedList.isExpanded)
            {
                if (showListSize)
                {
                    EditorGUILayout.PropertyField(serialisedList.FindPropertyRelative("Array.size"), new GUIContent("Listeners Number"));
                }

                //draw list element
                var oldLableWidth = EditorGUIUtility.labelWidth;
                for (int i = 0; i < list.Count; i++)
                {
                    var soEvent = list[i].Event;
                    var evName = soEvent != null ? soEvent.name : ("Empty" + i);
                    EditorGUILayout.Separator();
                    if (i < serialisedList.arraySize)
                    {
                        EditorGUILayout.Separator();
                       // var serialisedList = serializedObject.FindProperty("listeners");
                        serialisedList.GetArrayElementAtIndex(i).isExpanded = true;
                        EditorGUILayout.PropertyField(serialisedList.GetArrayElementAtIndex(i));
                        serializedObject.ApplyModifiedProperties();

                    }
                      //  EditorGUILayout.PropertyField(serialisedList.GetArrayElementAtIndex(i), new GUIContent(evName));



                    if (i != serialisedList.arraySize - 1)
                    {
                   //     EditorGUILayout.Separator();
                        GuiLine(1);
                        GuiLine(1);

                    }
                }
                EditorGUIUtility.labelWidth = oldLableWidth;
            }

            if (showListLabel)
            {
                EditorGUI.indentLevel -= 1;
            }
        }
#endif
    }
}
