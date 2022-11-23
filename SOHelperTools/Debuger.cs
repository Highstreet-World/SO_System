using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuger : MonoBehaviour
{
    static bool enableDebug = false;
    public static void Log(object message)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Debug.Log(message);
#else
        if(enableDebug) Debug.Log(message);
#endif
    }
    public static void Log(object message, UnityEngine.Object context)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Debug.Log(message, context);
#else
        if(enableDebug) Debug.Log(message, context);
#endif
    }


    public static void LogWarning(object message)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
      //  Debug.LogWarning(message);
#else
        if(enableDebug) Debug.LogWarning(message);
#endif
    }
    public static void LogWarning(object message, UnityEngine.Object context)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Debug.LogWarning(message, context);
#else
        if(enableDebug) Debug.LogWarning(message, context);
#endif
    }


    public static void LogError(object message)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Debug.LogError(message);
#else
        if(enableDebug) Debug.LogError(message);
#endif
    }

    internal static void LogException(Exception e)
    {
        Debug.LogException(e);
    }

    public static void LogError(object message, UnityEngine.Object context)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Debug.LogError(message, context);
#else
        if(enableDebug) Debug.LogError(message, context);
#endif
    }

}
