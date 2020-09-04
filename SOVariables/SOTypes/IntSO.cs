using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "intSO", menuName = "SO/Variables/Integer")]
    public class IntSO : NumiricVariableSO<int>
    {
        public override void Divide(int num)
        {
            SetValue((int)((float)this.Value / num));
        }

        public override void Mull(int num)
        {
            SetValue(this.Value * num);
        }


        public override void Add(int num)
        {
            SetValue(this.Value + num);
        }


        public override void Sub(int num)
        {
            SetValue(this.Value - num);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }

        public override void SetValue(string value)
        {
            int temp;
            if (int.TryParse(value, out temp))
                SetValue(temp);
        }
    }
}