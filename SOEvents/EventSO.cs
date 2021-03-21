using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SO.Events
{
    [CreateAssetMenu(fileName = "SOEvent", menuName = "SO/Event")]
    public class EventSO : ScriptableObject
    {
        public class ListenerEventPair
        {
            public EventListenerSO listener;
            public ObjectEvent objectEvent;

            public ListenerEventPair(EventListenerSO listener, ObjectEvent objectEvent)
            {
                this.listener = listener;
                this.objectEvent = objectEvent;
            }
        }

        public List<ListenerEventPair> listenersCallbacks = new List<ListenerEventPair>();

        public bool debug = true;

        [TextArea]
        [Tooltip("When is this event raised")]
        public string eventDescription = "[When does this event trigger]";


        public void Raise()
        {
            Raise(null);
        }

        public void RaiseValue(string value)
        {
            Raise(value);
        }

        public void RaiseValue(int value)
        {
            Raise(value);
        }

        public void RaiseValue(bool value)
        {
            Raise(value);
        }

        public void RaiseValue(float value)
        {
            Raise(value);
        }

        public void Raise(object value)
        {
            if (debug)Debuger.LogWarning("Raise: " + name);
            Z.InvokeEndOfFrame(() =>
            {
                for (int i = listenersCallbacks.Count - 1; i >= 0; i--)
                {
                    if (debug)Debuger.LogWarning("event: " + name + " invoke " + listenersCallbacks[i].listener.name);
                    listenersCallbacks[i].objectEvent.Invoke(value);
                }
            });
        }



        public void RegisterListener(EventListenerSO listener, ObjectEvent callback)
        {
            if (!IsRegistered(listener))
            {
                listenersCallbacks.Add(new ListenerEventPair(listener, callback));
            }
        }



        public void UnregisterListener(EventListenerSO listener)
        {
            ListenerEventPair listenerEventPair = null;
            if (Find(listener, out listenerEventPair))
            {
                listenersCallbacks.Remove(listenerEventPair);
            }
        }

        private bool Find(EventListenerSO listener, out ListenerEventPair listenerEventPair)
        {
            listenerEventPair = null;
            for (int i = listenersCallbacks.Count - 1; i >= 0; i--)
            {
                if (listenersCallbacks[i].listener == listener)
                {
                    listenerEventPair = listenersCallbacks[i];
                    return true;
                }
            }
            return false;
        }

        private bool IsRegistered(EventListenerSO listener)
        {
            for (int i = listenersCallbacks.Count - 1; i >= 0; i--)
            {
                if (listenersCallbacks[i].listener == listener) return true;
            }
            return false;
        }
        public void Awake()
        {
            listenersCallbacks.Clear();
        }
        public void OnAfterDeserialize()
        {
            listenersCallbacks.Clear();
        }
        public void OnBeforeSerialize()
        {
            listenersCallbacks.Clear();
        }
    }
}


