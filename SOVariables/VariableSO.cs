using SO.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    //  [CreateAssetMenu(fileName = "SOEvent", menuName = "SO/Event")]
    public abstract class VariableSO<T> : IVariableSO, ISerializationCallbackReceiver
    {

        //Value

        public T Value { get { return GetValue(); } set { SetValue(value); } }
        [SerializeField]
        private T _value;

        //When the game starts, the starting Value we use (so we can reset if need be)
        [SerializeField]
        private T startingValue = default(T);

        public static implicit operator T(VariableSO<T> v)
        {
            return v.Value ;
        }

        //public static implicit operator VariableSO<T>(T v)
        //{
        //    return 
        //}

        public virtual void SetValue(T newValue, bool log = false)
        {
            if (log) Debug.Log("SetValue: " + newValue + " on " + name);
            _value = newValue;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                startingValue = _value;
            }
#endif
            if (OnChanged != null)
            {
                RaisEvents();
            }
        }

        public void SetValue(VariableSO<T> numSO)
        {
            SetValue(numSO.Value);
        }

        public virtual T GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Recieve callback after unity deseriallzes the object
        /// </summary>
        public void OnAfterDeserialize()
        {
            _value = startingValue;
            UnSubscripeAll();
        }

        public void OnBeforeSerialize() { UnSubscripeAll(); }

        /// <summary>
        /// Reset the Value to it's inital Value if it's resettable
        /// </summary>
        public override void ResetValue()
        {
            Value = startingValue;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("value", Value);
        }

    }

    public abstract class IVariableSO : ScriptableObject, IFormattable , System.Runtime.Serialization.ISerializable
    {
        public EventSO OnChanged;
        protected event System.EventHandler valChanged;
        List<EventHandler> supEvents = new List<EventHandler>();
        protected virtual void RaisEvents()
        {
            if (this.valChanged != null) valChanged(this, EventArgs.Empty);
            if (OnChanged != null) OnChanged.Raise();
        }

        public void Subscripe(System.EventHandler onValChanged)
        {
            valChanged += onValChanged;
            supEvents.Add(onValChanged);
        }
        public void UnSubscripe(System.EventHandler onValChanged)
        {
            valChanged -= onValChanged;
            supEvents.Remove(onValChanged);
        }
        public void UnSubscripeAll()
        {
            for (int i = 0; i < supEvents.Count; i++)
            {
                valChanged -= supEvents[i];
            }
            supEvents.Clear();
        }

        public abstract string ToString(string format, IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public abstract void SetValue(string value);

        public static IVariableSO Parse(string inistanceID)
        {
            int id = 0;
            if (int.TryParse(inistanceID, out id))
            {
#if UNITY_EDITOR
                try
                {
                    return (IVariableSO)EditorUtility.InstanceIDToObject(id);
                }
                catch (Exception)
#endif
                {
                    throw new Exception("cant find inistanceID: " + inistanceID);
                }
            }
            else
            {
                throw new Exception("string is not inistanceID: " + inistanceID);
            }
        }

        public static bool TryParse(string inistanceID, out IVariableSO variableSO)
        {
            try
            {
                variableSO = IVariableSO.Parse(inistanceID);
                return true;
            }
            catch (Exception)
            {

                variableSO = null;
                return false;
            }
        }

        public void CopyToText(Text textComponent)
        {
            textComponent.text = this.ToString();
        }

        public void CopyToTMP_Text(TMPro.TMP_Text TMP_textComponent)
        {
            TMP_textComponent.text = this.ToString();
        }

        public void CopyToInputField(InputField InputFieldComponent)
        {
            InputFieldComponent.text = this.ToString();
        }

        public void CopyToTMP_InputField(TMPro.TMP_InputField TMP_InputFieldComponent)
        {
            TMP_InputFieldComponent.text = this.ToString();
        }

        /// <summary>
        /// Reset the Value to it's inital Value if it's resettable
        /// </summary>
        public abstract void ResetValue();
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }

}