using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    public class SOEnumVariable<T> : VariableSO<T> where T : System.Enum, IConvertible
    {
        public override void SetValue(string value)
        {
            Value = (T)Enum.Parse(typeof(T), value);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Enum.GetName(typeof(T), Value);
        }
    }
}