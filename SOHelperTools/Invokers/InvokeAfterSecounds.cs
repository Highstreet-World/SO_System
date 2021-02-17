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

    Coroutine CO;
    IEnumerator InvokeAfterCO(float secounds)
    {
        do
        {
            yield return new WaitForSeconds(secounds);
            OnTimeEnd.Invoke();
        } while (Loop && secounds > 0);
        CO = null;
    }

    private void OnEnable()
    {
        if (CountOnEnable)
        {
            CO = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
        }
    }

    private void Start()
    {
        if (CountOnStart)
        {
            CO = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
        }
    }

    public void ManualStart(float secounds)
    {
        if (CO != null)
        {
            StopCoroutine(CO);
        }
        CO = CoRef.StartCoroutineAway(InvokeAfterCO(secounds));
    }
    public void ManualStart()
    {
        ManualStart(secounds);
    }

    public void ManualStop()
    {
        StopCoroutine(CO);
    }

    private void OnDisable()
    {
        if (CO != null)
            StopCoroutine(CO);
    }


}
