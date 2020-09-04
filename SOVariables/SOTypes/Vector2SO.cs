using System;
using UnityEngine;
namespace SO
{
    [CreateAssetMenu(fileName = "Vector2SO", menuName = "SO/Variables/Vector2")]
    public class Vector2SO : VariableSO<Vector2>
    {
      public override void SetValue(string value)
        {
            value.Replace(" ","");//remove spaces

            if (value.Length > 9)
            {
                value = value.Substring(1, value.Length-2);
                var xy = value.Split(',');
                if (xy.Length >= 2)
                {
                    float x,y;
                    if (!float.TryParse(xy[0], out x)) return;
                    if (!float.TryParse(xy[1], out y)) return;
                    Value = new Vector2(x, y);
                }
            }
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString();
        }
    }
}
