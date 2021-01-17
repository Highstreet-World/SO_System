using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Z
{
    public static Coroutine InvokeAfterDelay(float delay, Action callback)
    {
        return CoRef.StartCoroutineAway(_WaitForSeconds(delay, callback));
    }
    public static Coroutine InvokeEndOfFrame(Action callback)
    {
        return CoRef.StartCoroutineAway(_WaitForEndOfFrame(callback));
    }
    static IEnumerator _WaitForSeconds(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();
    }
    static IEnumerator _WaitForEndOfFrame(Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback.Invoke();
    }
}
