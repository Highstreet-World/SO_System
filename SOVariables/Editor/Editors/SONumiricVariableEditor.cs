using System;
using UnityEditor;

namespace SO
{
    public class SONumiricVariableEditor<T> : VariableSOEditor<T> where T : System.Enum, IConvertible
    {
        public override T GetEditorGUILayoutValue(T Value)
        {
           return (T)EditorGUILayout.EnumPopup("Modify current Value by: ", Value);
        }
    }
}