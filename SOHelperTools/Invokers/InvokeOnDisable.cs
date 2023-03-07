using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InvokeOnDisable : MonoBehaviour
{
    [FormerlySerializedAs("OnDisable")] public UnityEvent onDisable;
    bool isFirstTime = true;
    private void Start()
    {
        isFirstTime = false;
        OnDisable();
    }

    private void OnDisable()
    {
        if (isFirstTime) return;
        onDisable.Invoke();
    }
}
