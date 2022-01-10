using System;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
namespace SO
{
    [CreateAssetMenu(fileName = "GameObjectSO", menuName = "SO/Variables/DirectionEnumSO")]
    public class DirectionSO : VariableSO<Direction>
    {
        public override void SetValue(string value)
        {
            Direction temp;
            if (Enum.TryParse(value, out temp))
                SetValue(temp);
        }

        public override string ToString(string format, IFormatProvider formatProvider = null)
        {
            return Value.ToString(format, formatProvider);
        }
    }
}

public enum Direction
{
    None,
    Right,
    Left,
    Up,
    Down
}