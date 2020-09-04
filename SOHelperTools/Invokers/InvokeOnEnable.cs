using UnityEngine;
using UnityEngine.Events;

public class InvokeOnEnable : MonoBehaviour
{
    public UnityEvent OnEnable;

    private void Start()
    {
        OnEnable.Invoke();
    }
}
