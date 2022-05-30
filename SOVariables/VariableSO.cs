using SO.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    //  [CreateAssetMenu(fileName = "SOEvent", menuName = "SO/Event")]
    public abstract class VariableSO<T> : IVariableSO, ISerializationCallbackReceiver
    {

        //Value
        [HideInInspector]
        public T Value { get { return GetValue(); } set { SetValue(value); } }
        [SerializeField]
        private T _value;

        //When the game starts, the starting Value we use (so we can reset if need be)
        [SerializeField]
        private T startingValue = default(T);

        public static implicit operator T(VariableSO<T> v)
        {
            return v.Value;
        }
        //public static implicit operator VariableSO<T>(T v)
        //{
        //    this.value = v;
        //    return this;
        //}

        public virtual void SetValue(T newValue, bool forceUpdate = false, bool log = false)
        {
            if (log) Debuger.Log("SetValue: " + newValue + " on " + name);
            if ((_value == null && newValue != null) || forceUpdate || (_value != null && !_value.Equals(newValue)))
            {
                _value = newValue;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    startingValue = _value;
                }
#endif
                if (allowCache)
                {
                    CasheValue();
                }
                RaisEvents();
            }
        }

        public virtual void SetValue(VariableSO<T> variableSo)
        {
            SetValue(variableSo.Value);
        }



        public virtual T GetValue()
        {
            if (!isCacheRetrived && allowCache)
            {
                RetriveCache();
            }
            return _value;
        }

        // <summary>
        // Recieve callback after unity deseriallzes the object
        // </summary>
        public void OnAfterDeserialize()
        {
            //if (!Application.isPlaying)
            //{
            //    _value = startingValue;
            //    UnSubscripeAll();
            //}
        }
        public void OnBeforeSerialize()
        {
            //if (!Application.isPlaying)
            //{
            //    UnSubscripeAll(); ResetValue();
            //}
        }


        /// <summary>
        /// Reset the Value to it's inital Value if it's resettable
        /// </summary>
        public override void ResetValue()
        {
            Value = startingValue;
            UnSubscripeAll();
        }
        public T GetDefultValue()
        {
            return Value;
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("value", Value);
        }

    }

    public abstract class IVariableSO : ManagedScriptableObject, IFormattable, System.Runtime.Serialization.ISerializable
    {
        public EventSO OnChanged;
        [Header("Cache value if changed \"dont use in update\"")]
        public bool allowCache = false;

        protected List<EventHandler> valChanged;
        List<EventHandler> supEvents = new List<EventHandler>();
        static List<IVariableSO> refToSoVars = new List<IVariableSO>();
        protected override void OnEnable()
        {
            base.OnEnable();
            Initialize();
        }
        protected virtual void Awake()
        {
            Initialize();
        }
        protected void Initialize()
        {
            if (refToSoVars == null) { refToSoVars = new List<IVariableSO>(); }
            //#if UNITY_EDITOR
            if (!refToSoVars.Contains(this))
            {
                refToSoVars.Add(this);
            }
            //#endif
        }

        protected virtual void RaisEvents()
        {
            if (this.valChanged != null)
            {
                for (int i = 0; i < valChanged.Count; i++)
                {
                    try
                    {
                        valChanged[i].Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

            }
            if (OnChanged != null)
            {
                try
                {
                    OnChanged.Raise();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void Subscripe(System.EventHandler onValChanged)
        {
            if (valChanged == null) valChanged = new List<EventHandler>();
            valChanged.Add(onValChanged);
            supEvents.Add(onValChanged);
        }
        public void UnSubscripe(System.EventHandler onValChanged)
        {
            if (valChanged != null) valChanged.Remove(onValChanged);
            supEvents.Remove(onValChanged);
        }
        public void UnSubscripeAll()
        {
            if (valChanged != null) valChanged.Clear();
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

        protected override void OnBegin(bool isEditor)
        {
            if (allowCache)
            {
                RetriveCache();
            }
        }

        protected override void OnEnd(bool isEditor)
        {
            if (allowCache)
            {
                CasheValue();
            }
            ResetValue();
        }


        protected bool isCacheRetrived = false;
        protected void RetriveCache()
        {
#if !UNITY_EDITOR
            if (PlayerPrefs.HasKey($"SOV{name}"))
                SetValue(PlayerPrefs.GetString($"SOV{name}"));
            isCacheRetrived = true;
#endif
        }
        protected void CasheValue()
        {
#if !UNITY_EDITOR
            PlayerPrefs.SetString($"SOV{name}", this.ToString());
#endif
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

        public void CopyToScrollbar(Scrollbar ScrollbarComponent)
        {
            ScrollbarComponent.value = float.Parse(this.ToString());
        }

        public void CopyToSlider(Slider SliderComponent)
        {
            SliderComponent.value = float.Parse(this.ToString());
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

