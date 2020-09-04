using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace SO.Events
{
    [System.Serializable] public class ObjectEvent : UnityEvent<object> { }
    [System.Serializable]
    public class SOListener
    {
        public EventSO Event;
        public bool listenWhenDisabled;
        public ObjectEvent Response;
        public EventListenerSO source;
    }
    public class EventListenerSO : MonoBehaviour
    {
        [HideInInspector]public List<SOListener> listeners = new List<SOListener>();
        private void OnEnable()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].source = this;
                if (listeners[i].Event != null) listeners[i].Event.RegisterListener(this, listeners[i].Response);
            }

        }
        private void OnDestroy()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i].Event != null) listeners[i].Event.UnregisterListener(this);
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i].listenWhenDisabled == false)
                    if (listeners[i].Event != null) listeners[i].Event.UnregisterListener(this);
            }
        }
    }
}
