using SO.Events;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorList
{


    private static GUIContent
    moveButtonContent = new GUIContent("\u21b4", "move down"),
    duplicateButtonContent = new GUIContent("+", "duplicate"),
    deleteButtonContent = new GUIContent("-", "delete");

    public static void Show(SerializedProperty list, List<SOListener> listeners, bool showListSize = true, bool showListLabel = true)
    {
        if (showListLabel)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
        }
        if (!showListLabel || list.isExpanded)
        {
            if (showListSize)
            {

              //  if (ListSize == -1) { }
                EditorGUI.indentLevel -= 1;
                //  EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                EditorGUILayout.BeginHorizontal();
                var size = EditorGUILayout.IntField("Listeners Number", listeners.Count);
                int diffrence = size - listeners.Count;
                if (diffrence != 0)
                    if (GUILayout.Button(new GUIContent("Apply")))
                    {
                        //    int diffrence = size - listeners.Count;

                        //    if (diffrence == 0)
                        //    {
                        //        //do nothing
                        //    }
                        //    else if (diffrence > 0)
                        //    {
                        //        for (int i = 0; i < diffrence; i++)
                        //        {
                        //            listeners.Add(new SOListener());
                        //        }
                        //    }
                        //    else if (diffrence < 0)
                        //    {
                        //        listeners.RemoveRange(listeners.Count - 1 + diffrence, diffrence * -1);
                        //    }
                    }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel += 1;
            }
            //draw list element
            for (int i = 0; i < list.arraySize; i++)
            {
                var soEvent = listeners[i].Event;
                EditorGUIUtility.labelWidth = 50;
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(soEvent != null ? soEvent.name : ("No Event" + i)));
                ShowButtons();
            }
        }
        if (showListLabel)
        {
            EditorGUI.indentLevel -= 1;
        }
    }
    private static void ShowButtons()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Button(moveButtonContent);
        GUILayout.Button(duplicateButtonContent);
        GUILayout.Button(deleteButtonContent);
        EditorGUILayout.EndHorizontal();
    }

}