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

        [TextArea]
        [Tooltip("When is this event raised")]
        public string eventDescription = "[When does this event trigger]";



        public void Raise()
        {
            Raise(null);
        }

        public void Raise(object value)
        {
            Debug.LogWarning("Raise: " + name);
            for (int i = listenersCallbacks.Count - 1; i >= 0; i--)
            {
                Debug.LogWarning("event: " + name + " invoke " + listenersCallbacks[i].listener.name);
                listenersCallbacks[i].objectEvent.Invoke(value);
            }
        }

        public void RaiseAsync()
        {
            RaiseAsync(null);
        }
#if !UNITY_WEBGL 
        public async void RaiseAsync(object value)
        {
            await Task.Delay(1);
            Raise(value);
        }
#else
        public void RaiseAsync(object value)
        {
            Raise(value);
        }
#endif

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


