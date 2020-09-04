using UnityEngine;
using UnityEngine.Events;

public class InvokeOnStart : MonoBehaviour
{
    public UnityEvent OnStart;

    private void Start()
    {
        OnStart.Invoke();
    }
}
