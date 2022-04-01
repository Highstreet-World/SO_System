using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public abstract class ManagedScriptableObject : ScriptableObject
{
    abstract protected void OnBegin(bool isEditor);
    abstract protected void OnEnd(bool isEditor);

#if UNITY_EDITOR
    protected virtual void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayStateChange;
    }

    protected virtual void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayStateChange;
    }

    void OnPlayStateChange(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            OnBegin(true);
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            OnEnd(true);
        }
    }
#else
        protected virtual void OnEnable()
        {
            OnBegin(false);
        }
 
        protected virtual void OnDisable()
        {
            OnEnd(false);
        }
#endif
}