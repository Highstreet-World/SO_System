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

    public static Coroutine InvokeWhen(Action callback, Func<bool> predect)
    {
        return CoRef.StartCoroutineAway(_WaitUntil(callback, predect));
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
    static IEnumerator _WaitUntil(Action callback, Func<bool> predect)
    {
        yield return new WaitUntil(predect);
        callback.Invoke();
    }
}
