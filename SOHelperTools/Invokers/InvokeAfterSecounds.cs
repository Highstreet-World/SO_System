using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterSecounds : MonoBehaviour
{

    public float secounds;
    public bool CountOnEnable;
    public bool CountOnStart;
    public bool Loop;
    public UnityEvent OnTimeEnd;

    Coroutine co;
    IEnumerator InvokeAfterCO(float secounds)
    {
        do
        {
            yield return new WaitForSeconds(secounds);
            OnTimeEnd.Invoke();
        } while (Loop && secounds > 0);
        co = null;
    }

    private void OnEnable()
    {
        if (CountOnEnable)
        {
            co = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
        }
    }

    private void Start()
    {
        if (CountOnStart)
        {
            co = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
        }
    }

    public void ManualStart(float secounds)
    {
        ManualStop();
        co = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
    }
    
    public void ManualStart()
    {
        ManualStart(secounds);
    }

    public void ManualStop()
    {
        if (co != null)
        {
            CoRef.StopCoroutineAway(co);
        }
    }

    private void OnDisable()
    {
        if (co != null)
            CoRef.StopCoroutineAway(co);
    }


}
