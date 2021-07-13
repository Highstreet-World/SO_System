using System;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "boolSO", menuName = "SO/Variables/Bool")]
    public class BoolSO : VariableSO<bool>
    {
        public override void SetValue(string value)
        {
            var parsedVal = false;
            if (bool.TryParse(value,out parsedVal))
            {
                SetValue(parsedVal);
            }
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString();
        }

        public void CopyToToggle(Toggle toggle)
        {
            toggle.SetIsOnWithoutNotify(bool.Parse(this.ToString()));
        }

    }
}