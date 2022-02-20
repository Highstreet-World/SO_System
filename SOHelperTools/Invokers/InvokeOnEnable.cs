using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InvokeOnEnable : MonoBehaviour
{
    [FormerlySerializedAs("OnEnable")] public UnityEvent onEnable;
    bool isFirstTime = true;
    private void Start()
    {
        isFirstTime = false;
        OnEnable();
    }
    private void OnEnable()
    {
        if (isFirstTime) return;
        onEnable.Invoke();
    }
}
