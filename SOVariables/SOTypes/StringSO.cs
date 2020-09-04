using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "stringSO", menuName = "SO/Variables/String")]
    public class StringSO : VariableSO<string>
    {


        public override void SetValue(string value)
        {
            Value = value;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value;
        }
    }
}