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
    IEnumerator InvokeAfterCO()
    {
        do
        {
            yield return new WaitForSeconds(secounds);
            OnTimeEnd.Invoke();
        } while (Loop);
        CO = null;
    }

    private void OnEnable()
    {
        if (CountOnEnable)
        {
            CO = StartCoroutine(InvokeAfterCO());
        }
    }

    private void Start()
    {
        if (CountOnStart)
        {
            CO = StartCoroutine(InvokeAfterCO());
        }
    }

    public void ManualStart()
    {
        if (CO != null)
        {
            StopCoroutine(CO);
        }
        CO = StartCoroutine(InvokeAfterCO());

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
