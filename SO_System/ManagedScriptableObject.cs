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
    protected void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayStateChange;
    }

    protected void OnDisable()
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
        protected void OnEnable()
        {
            OnBegin(false);
        }
 
        protected void OnDisable()
        {
            OnEnd(false);
        }
#endif
}