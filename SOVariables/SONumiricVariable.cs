using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    public abstract class NumiricVariableSO<T> : VariableSO<T>
    {
        //public static VariableSO<T> operator +(NumiricVariableSO<T> a, T b)
        //{
        //    a.Add(b);
        //    return a;
        //}

        public abstract void Divide(T num);
        public void Divide(NumiricVariableSO<T> numSO)
        {
            Divide(numSO.Value);
        }

        public abstract void Mull(T num);
        public void Mull(NumiricVariableSO<T> numSO)
        {
            Mull(numSO.Value);
        }

        public abstract void Add(T num);
        public void Add(NumiricVariableSO<T> numSO)
        {
            Add(numSO.Value);
        }

        public abstract void Sub(T num);
        public void Sub(NumiricVariableSO<T> numSO)
        {
            Sub(numSO.Value);
        }
    }

}