using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System;
using SO.Events;

namespace SO
{

    [CustomPropertyDrawer(typeof(SOListener))]
    public class SOListenerDrawer : PropertyDrawerExtended
    {
        SerializedProperty sOEvent;
        SerializedProperty listenWhenDisabled;
        SerializedProperty Response;
        SerializedProperty WhatTheEventDO;
        public override void OnGUI(Rect _position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            //base.OnGUI(position, property, label);
            SetPosition(_position);
            sOEvent = property.FindPropertyRelative("Event");
            listenWhenDisabled = property.FindPropertyRelative("listenWhenDisabled");
            Response = property.FindPropertyRelative("Response");
            WhatTheEventDO = property.FindPropertyRelative("WhatTheEventDO");

            if (sOEvent != null)
            {//event assigned ----------------------------------------------------
                var val = (EventSO)sOEvent.objectReferenceValue;
                if (val != null)
                {
                    if (property.isExpanded)
                    {//expanded ----------------------------------------------------
                        Rect prefixPos = EditorGUI.PrefixLabel(position, GUIContent.none);
                        AddProberty(property, new GUIContent(val.name), EditorGUIUtility.singleLineHeight); // draw the elemnt Arrow   
                        AddProberty(sOEvent, GUIContent.none);
                        //prefixPos.y = position.y;
                        //AddHelp(ref prefixPos,val.eventDescription, MessageType.Info);

                        AddProberty(listenWhenDisabled);

                        prefixPos.y = position.y;
                        AddProberty(ref prefixPos, Response);

                        //AddProberty(WhatTheEventDO);
                    }
                    else
                    {//minimized ----------------------------------------------------
                        Rect prefixPos = position;
                        AddProberty(property, GUIContent.none, EditorGUIUtility.singleLineHeight); // draw the elemnt Arrow   
                        AddProberty(ref prefixPos, sOEvent, GUIContent.none);
                    }
                }
                else
                {//no event assigned ----------------------------------------------------
                    Rect prefixPos = position;
                    AddProberty(property, GUIContent.none, EditorGUIUtility.singleLineHeight); // draw the elemnt Arrow   
                    AddHelp(ref prefixPos, "Event not assigned", MessageType.Error);
                    if (property.isExpanded)
                    {
                        prefixPos.y += EditorGUIUtility.singleLineHeight;
                        AddProberty(ref prefixPos, sOEvent);
                    }
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            sOEvent = property.FindPropertyRelative("Event");
            listenWhenDisabled = property.FindPropertyRelative("listenWhenDisabled");
            Response = property.FindPropertyRelative("Response");
            WhatTheEventDO = property.FindPropertyRelative("WhatTheEventDO");

            if (sOEvent != null)
            {
                var val = (EventSO)sOEvent.objectReferenceValue;
                if (val != null)
                {//event assigned ----------------------------------------------------
                    if (property.isExpanded)
                    { //expanded ----------------------------------------------------
                        return EditorGUI.GetPropertyHeight(sOEvent)
                                //  + calculateTextHeight(val.eventDescription,position)
                                + EditorGUI.GetPropertyHeight(listenWhenDisabled)
                                 + EditorGUI.GetPropertyHeight(Response)
                                  //+ EditorGUI.GetPropertyHeight(WhatTheEventDO)
                                    + EditorGUIUtility.singleLineHeight;
                    }
                    else
                    {//minimized ----------------------------------------------------
                        return EditorGUI.GetPropertyHeight(sOEvent);
                    }
                }
                else
                {//no event assigned ----------------------------------------------------
                    if (property.isExpanded)
                    { //expanded ----------------------------------------------------
                        return EditorGUI.GetPropertyHeight(sOEvent)
                                + EditorGUIUtility.singleLineHeight;
                    }
                    else
                    {//minimized ----------------------------------------------------
                        return EditorGUIUtility.singleLineHeight;
                    }
                }
            }
            return 0; // impossible case
        }
    }
}