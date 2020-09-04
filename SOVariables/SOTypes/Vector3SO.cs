using System;
using UnityEngine;
namespace SO
{
    [CreateAssetMenu(fileName = "Vector3SO", menuName = "SO/Variables/Vector3")]
    public class Vector3SO : VariableSO<Vector3>
    {
        public override void SetValue(string value)
        {
            value.Replace(" ", "");//remove spaces

            if (value.Length > 13)
            {
                value = value.Substring(1, value.Length - 2);
                var xyz = value.Split(',');
                if (xyz.Length >= 3)
                {
                    float x, y, z;
                    if (!float.TryParse(xyz[0], out x)) return;
                    if (!float.TryParse(xyz[1], out y)) return;
                    if (!float.TryParse(xyz[2], out z)) return;
                    Value = new Vector3(x, y, z);
                }
            }
        }
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString();
        }

    }
}
