using System;
using UnityEngine;
namespace SO
{
    [CreateAssetMenu(fileName = "floatSO", menuName = "SO/Variables/Float")]
    public class FloatSO : NumiricVariableSO<float>
    {
        public override void Divide(float num)
        {
            SetValue(this.Value / num);
        }
        public override void Mull(float num)
        {
            SetValue(this.Value * num);
        }
        public override void Add(float num)
        {
            SetValue(this.Value + num);
        }
        public override void Sub(float num)
        {
            SetValue(this.Value - num);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }

        public override void SetValue(string value)
        {
            float temp;
            if (float.TryParse(value, out temp))
                SetValue(temp);
        }
    }
}
