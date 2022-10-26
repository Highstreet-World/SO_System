using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "stringsSO", menuName = "SO/Variables/Strings")]
    public class StringsSO : ScriptableObject
    {
        public List<string> strings;

        public void AddValue(string val)
        {
            strings.Add(val);
        }

        public void SetValue(int index, string val)
        {
            strings[index] += val;
        }

        public void ReplaceValue(int index, string val)
        {
            strings[index] = val;
        }

        public string ToString(int index)
        {
            return strings[index];
        }

        public void ClearStrings()
        {
            strings.Clear();
        }
    }
}