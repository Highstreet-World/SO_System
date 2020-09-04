using System;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(IVariableSO), true)]
    public class IVariableSODrawer : ScriptableObjectDrawer
    {
        IVariableSO variableSORef;
        protected override void OnBuildGui(ref Rect position, SerializedProperty property, GUIContent label)
        {
            bool drawCreateButton = GetPropertyTypeName(property) != "IVariableSO" && SO.SO_SystemSettings.Inistance.ShowAssignButton;
            bool drawVariabelValue = SO.SO_SystemSettings.Inistance.ShowVarSOValue;

            variableSORef = (IVariableSO)SORef;
            CalculateLAyout(ref position);

            //  uncomment to debug draw area ------------------------------------------------------
            // DebugDrawArea(position);

            //first field -------------------------------------- 
            if (variableSORef)//hase value
            {
                if (drawVariabelValue)
                {
                    //value inside variableSO
                    VraiableField(leftRow, label, variableSORef); //input field
                }
                else
                {
                    AddProberty(ref position, property, label);
                }
            }
            else //no value
            {
                if (drawCreateButton)
                {
                    AddProberty(ref leftRow, property, label);
                }
                else
                {
                    AddProberty(ref position, property, label);
                }
            }

            //secound field-------------------------------------------
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = .1f;
            if (variableSORef)//hase value
            {
                if (drawVariabelValue)
                {
                    //VariableSO refrence
                    AddProberty(ref rightRow, property, new GUIContent(" "));
                }
            }
            else
            {
                if (drawCreateButton)
                {
                    if (GUI.Button(rightRow, "Assign"))
                    {
                        AssignAsync(property, label.text);
                    }
                }            
            }
            AssignToPropertyIfTheObjectIsCreated(property, label.text);
        }

        protected override string getCreatePath()
        {
            return SO.SO_SystemSettings.Inistance.VarSOCreatePath;
        }
        protected virtual void VraiableField(Rect Position, GUIContent label, IVariableSO variableSO)
        {
            if (variableSO.ToString("ID") == "ID" || variableSO.ToString("ID") == "-ID") //check if formating made issue with data
            {
                float ParsedNum;
                if (float.TryParse(variableSO.ToString(), out ParsedNum))
                {
                    variableSO.SetValue(EditorGUI.FloatField(Position, label, ParsedNum).ToString());
                }
                else
                {
                    variableSO.SetValue(EditorGUI.TextField(Position, label, variableSO.ToString()));
                }
            }
            else
            {
                variableSO.SetValue(EditorGUI.TextField(Position, label, variableSO.ToString("ID")));
            }

        }

        protected virtual bool AllowMultiLine() { return false; }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (variableSORef != null && AllowMultiLine() && EditorGUIUtility.currentViewWidth <= 333)
            {
                return base.GetPropertyHeight(property, label) * 2;
            }
            else
            {
                return base.GetPropertyHeight(property, label);
            }
        }

    }
}