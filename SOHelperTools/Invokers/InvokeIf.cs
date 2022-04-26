using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO.tools
{
    public class InvokeIf : MonoBehaviour
    {
        [SerializeField] SO.BoolSO Condition;
        [SerializeField] bool invert = false;
        [Space()]
        [SerializeField] bool invokeOnStart = false;
        [SerializeField] bool invokeOnEnable = false;
        [SerializeField] bool invokeOnValueChange = false;
        [Space()]
        [SerializeField] UnityEvent OnValid;
        [SerializeField] UnityEvent OninInvalid;

        private void Awake()
        {
            if (invokeOnValueChange)
            {
                Condition.Subscripe(OnValChanged);
            }
        }

        private void OnEnable()
        {
            if (invokeOnEnable)
            {
                Invoke();
            }
        }
        private void Start()
        {
            if (invokeOnStart)
            {
                Invoke();
            }
        }
        private void OnValChanged(object sender, EventArgs e)
        {
            Invoke();
        }
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