using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SO.Events
{
    [CustomEditor(typeof(EventSO))]
    public class SOEventEditor : Editor
    {
        EventSO script;

        SerializedProperty listeners;
        SerializedProperty description;
        SerializedProperty Value;

        bool showDetails = false;
        void OnEnable()
        {
            script = (EventSO)target;
            listeners = serializedObject.FindProperty("listeners");
            description = serializedObject.FindProperty("eventDescription");
            Value = serializedObject.FindProperty("Value");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //base.OnInspectorGUI();

            EditorGUILayout.PropertyField(description);
            if (GUILayout.Button("Raise"))
            {
                script.Raise();
            }
            GuiLine(1);
            // EditorGUILayout.PropertyField(listeners);
         //   ShowList(listeners, script.listeners);
            serializedObject.ApplyModifiedProperties();
        }

        public void ShowList(SerializedProperty serialisedList, List<SOListener> list)
        {

            //label
            //EditorGUILayout.PropertyField(serialisedList);
            // 

            if (serialisedList.isExpanded)
            {
                showDetails = EditorGUILayout.Toggle(new GUIContent("Show Details?"), showDetails);
                if (showDetails)
                {
                    SO.SO_SystemSettings.Inistance.AllowEditListenersFromEvents = EditorGUILayout.Toggle(new GUIContent("Enable Edit Listeners"), SO.SO_SystemSettings.Inistance.AllowEditListenersFromEvents);
                    if (SO.SO_SystemSettings.Inistance.AllowEditListenersFromEvents)
                    {
                        EditorGUILayout.HelpBox("Editing listeners from events not Tested", MessageType.Warning);
                    }
                }

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextField(new GUIContent("Size"), list.Count.ToString());
                EditorGUI.EndDisabledGroup();
                //draw list element
                var oldLableWidth = EditorGUIUtility.labelWidth;

                for (int i = 0; i < list.Count; i++)
                {
                    var soEvent = list[i].Event;
                    var evName = soEvent != null ? soEvent.name : ("Empty" + i);
                    EditorGUILayout.Separator();
                    if (i < serialisedList.arraySize)
                    {
                        var serialiasedlistener = serialisedList.GetArrayElementAtIndex(i);
                        var listener = list[i];
                        if (listener == null) { continue; }
                        if (!showDetails)
                        {
                            EditorGUILayout.ObjectField(new GUIContent(listener.source.name), listener.source, typeof(GameObject), true);
                        }
                        else
                        {
                            serialiasedlistener.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(serialiasedlistener.isExpanded, listener.source.name);
                            EditorGUI.indentLevel += 1;
                            if (serialiasedlistener.isExpanded)
                            {
                                EditorGUI.BeginDisabledGroup(true);
                                EditorGUILayout.ObjectField(new GUIContent("Listener: "), listener.source, typeof(GameObject), true);
                                EditorGUI.EndDisabledGroup();
                                EditorGUI.BeginDisabledGroup(!SO.SO_SystemSettings.Inistance.AllowEditListenersFromEvents);
                                EditorGUILayout.PropertyField(serialiasedlistener.FindPropertyRelative("listenWhenDisabled"));
                                EditorGUILayout.PropertyField(serialiasedlistener.FindPropertyRelative("Response"));
                                EditorGUILayout.PropertyField(serialiasedlistener.FindPropertyRelative("WhatTheEventDO"));
                                EditorGUI.EndDisabledGroup();

                                // EditorGUILayout.PropertyField(serialiasedlistener, new GUIContent(evName), true);
                            }
                            EditorGUI.indentLevel -= 1;
                            EditorGUILayout.EndFoldoutHeaderGroup();
                        }
                    }


                    if (i != serialisedList.arraySize - 1)
                    {
                        EditorGUILayout.Separator();
                        GuiLine(1);
                        GuiLine(1);
                    }
                }
                EditorGUIUtility.labelWidth = oldLableWidth;
            }




        }
        void GuiLine(int i_height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
    }

}