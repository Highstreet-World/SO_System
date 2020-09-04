using System;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
namespace SO
{
    [CreateAssetMenu(fileName = "GameObjectSO", menuName = "SO/Variables/GameObject")]
    public class GameObjectSO : VariableSO<GameObject>
    {
        public override void SetValue(string InstanceID)
        {
            int id = 0;
            if (int.TryParse(InstanceID, out id))
            {
#if UNITY_EDITOR 
                Value = (GameObject)EditorUtility.InstanceIDToObject(id);
#endif
            }
        }

        public override string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (String.IsNullOrEmpty(format)) format = "N";
            if (Value != null)
            {
                switch (format)
                {
                    case "N":
                    case "Name":
                        return Value.name;
                    case "P":
                    case "Position":
                        return Value.transform.position.ToString();
                    case "R":
                    case "Rotation":
                        return Value.transform.rotation.ToString();
                    case "S":
                    case "Scale":
                        return Value.transform.localScale.ToString();
                    case "ID":
                    case "InstanceID":
                        return Value.GetInstanceID().ToString();

                    default:
                        throw new FormatException(String.Format("The {0} format string is not supported.", format));
                }
            }
            else
            {
                return "Null";
            }
        }
    }
}