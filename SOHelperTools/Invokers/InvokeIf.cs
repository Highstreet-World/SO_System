using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO.tools
{
    public class InvokeIf : MonoBehaviour
    {
        [SerializeField] SO.BoolSO Condition;
        [SerializeField] bool invert =false;

        [SerializeField] UnityEvent OnValid;
        [SerializeField] UnityEvent OninInvalid;

        public void Invoke()
        {
            if (Condition.Value)
            {
                if (!invert) OnValid.Invoke(); else OninInvalid.Invoke();
            }
            else
            {
                if (!invert) OninInvalid.Invoke(); else OnValid.Invoke();
            }
        }
    }
}