using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO.Events
{
    [CustomPropertyDrawer(typeof(EventSO), true)]
    public class EventSODrawer : ScriptableObjectDrawer
    {
        float helpHeight;
        protected override void OnBuildGui(ref Rect _position, SerializedProperty property, GUIContent label)
        {
            SetPosition(_position);
            EventSO variableSORef = (EventSO)SORef;
            if (variableSORef && SO.SO_SystemSettings.Inistance.ShowEventDiscription)//hase value
            {
                Rect prefixPos = EditorGUI.PrefixLabel(position, new GUIContent(" "));
                AddProberty(property);

                prefixPos.y = position.y;
                if (variableSORef.eventDescription != "[When does this event trigger]")
                    helpHeight = AddHelp(variableSORef.eventDescription, MessageType.Info);
            }
            else
            {
                base.OnBuildGui(ref position, property, label);
            }
        }

        protected override string getCreatePath()
        {
            return SO.SO_SystemSettings.Inistance.EventSOCreatePath;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (SORef && SO.SO_SystemSettings.Inistance.ShowEventDiscription)//hase value
            {
                return EditorGUIUtility.singleLineHeight + helpHeight;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }
    }
}