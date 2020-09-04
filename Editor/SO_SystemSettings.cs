using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SO.Events;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace SO
{

    //settings serializable object 
    [System.Serializable]
    public class SO_SystemSettings
    {
        //singelton -------------------------------------
        private static SO_SystemSettings inistance = null;
        public static SO_SystemSettings Inistance { get { return Load(); } }
        private SO_SystemSettings() { }

        //Stored data -----------------------------------

        //ScriptableObjects Drawer
        public bool ShowAssignButton { get => showAssignButton; set { if (showAssignButton != value) { showAssignButton = value; isDirty = true; } } }
        [SerializeField]
        private bool showAssignButton = true;

        public string SOCreatePath { get => soCreatePath; set { if (soCreatePath != value) { soCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string soCreatePath = "";

        public string EventSOCreatePath { get => eventSOCreatePath; set { if (eventSOCreatePath != value) { eventSOCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string eventSOCreatePath = "";

        public string VarSOCreatePath { get => varSOCreatePath; set { if (varSOCreatePath != value) { varSOCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string varSOCreatePath = "";




        //eventSO Drawer
        public bool ShowEventDiscription { get => showEventDiscription; set { if (showEventDiscription != value) { showEventDiscription = value; isDirty = true; } } }
        [SerializeField]
        private bool showEventDiscription = true;
        public bool EventSOListenerDefultView { get => eventSOListenerDefultView; set { if (eventSOListenerDefultView != value) { eventSOListenerDefultView = value; isDirty = true; } } }
        [SerializeField]
        private bool eventSOListenerDefultView = false;
        public bool AllowEditListenersFromEvents { get => allowEditListenersFromEvents; set { if (allowEditListenersFromEvents != value) { allowEditListenersFromEvents = value; isDirty = true; } } }
        [SerializeField]
        private bool allowEditListenersFromEvents = false;

        //varSO
        public bool ShowVarSOValue { get => showVarSOValue; set { if (showVarSOValue != value) { showVarSOValue = value; isDirty = true; } } }



        [SerializeField]
        private bool showVarSOValue = true;



        //save & load SO_SystemSettings --------------------------------
        private bool isDirty = false;
        private readonly static string playerPrefsKey = "SO_SYS_Settings";


        internal static void Save()
        {
            //SO_SystemSettings save if inistance.isDirty
            if (Inistance.isDirty)
            {
                var json = JsonUtility.ToJson(inistance, true);
                PlayerPrefs.SetString(playerPrefsKey, json);
                inistance.isDirty = false;
            }
        }
        private static SO_SystemSettings Load()
        {
            if (inistance == null)
            {
                if (PlayerPrefs.HasKey(playerPrefsKey))
                {
                    var json = PlayerPrefs.GetString(playerPrefsKey);
                    if (json == null) //json updated
                    {
                        //delete old deprecated data
                        PlayerPrefs.DeleteKey(playerPrefsKey);
                    }
                    else
                    {
                        //SO_SystemSettings was found"
                        inistance = JsonUtility.FromJson<SO_SystemSettings>(json);
                    }
                }
                else
                {
                    //SO_SystemSettings first time
                    inistance = new SO_SystemSettings();
                }
            }
            //SO_SystemSettings cashed inistance"
            return inistance;
        }
    }


    //Display all GameEvents in the current scene - show which objects are using them
    //Include a description of what the Event does
    [ExecuteInEditMode]
    public class SO_SystemSettingsEditor : EditorWindow
    {
#if UNITY_EDITOR
        //Values for scrollPos
        private Vector2 scrollPos;

        public static SO_SystemSettings sO_SystemSettings;

        //Toolbar variables
        private int toolbarInt = 0;
        private string[] toolbarStrings = { "Settings", "debug" };

        [MenuItem("Window/SO_SYSTEM Settings")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one does not exist, make one
            SO_SystemSettingsEditor window = (SO_SystemSettingsEditor)EditorWindow.GetWindow(typeof(SO_SystemSettingsEditor));
            window.Show();
        }

        private void OnEnable()
        {
            sO_SystemSettings = SO_SystemSettings.Inistance;
        }

        private void OnDisable()
        {
            SO_SystemSettings.Save();
        }

        private void OnGUI()
        {
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            sO_SystemSettings = SO_SystemSettings.Inistance;
            const float padding = 1f;
            GUILayout.BeginHorizontal();//layout 
            GUILayout.Label("", GUILayout.Width(padding));//left padding

            GUILayout.BeginVertical();//main content
            //Switch based on the current selected toolbar button
            switch (toolbarInt)
            {
                case 0:
                    GUILayout.Label("General");
                    sO_SystemSettings.ShowAssignButton = GUILayout.Toggle(sO_SystemSettings.ShowAssignButton, new GUIContent("Show Assign button"));

                    if (sO_SystemSettings.ShowAssignButton)
                    {
                        sO_SystemSettings.SOCreatePath = SelectPath("Create ScriptableObjects  at:", sO_SystemSettings.SOCreatePath);
                        sO_SystemSettings.VarSOCreatePath = SelectPath("Create SO Variables  at:", sO_SystemSettings.VarSOCreatePath);
                        sO_SystemSettings.EventSOCreatePath = SelectPath("Create SO Events  at:", sO_SystemSettings.EventSOCreatePath);
                    }

                    GUILayout.Space(5f);
                    GUILayout.Label("eventSO");
                    sO_SystemSettings.ShowEventDiscription = GUILayout.Toggle(sO_SystemSettings.ShowEventDiscription, new GUIContent("Show soEvent discription"));
                    sO_SystemSettings.EventSOListenerDefultView = GUILayout.Toggle(sO_SystemSettings.EventSOListenerDefultView, new GUIContent("EventSO listener native view"));
                    sO_SystemSettings.AllowEditListenersFromEvents = GUILayout.Toggle(sO_SystemSettings.AllowEditListenersFromEvents, new GUIContent("Allow Edit Listeners From Events directly"));

                    GUILayout.Space(5f);
                    GUILayout.Label("soVariable");
                    sO_SystemSettings.ShowVarSOValue = GUILayout.Toggle(sO_SystemSettings.ShowVarSOValue, new GUIContent("Show varSO value"));
                    break;
                case 1:
                    GUILayout.TextArea(JsonUtility.ToJson(sO_SystemSettings, true));
                    break;
            }
            GUILayout.EndVertical();

            GUILayout.Label("", GUILayout.Width(padding));//right padding
            GUILayout.EndHorizontal();
        }

        private static string SelectPath(string label, string originalPath, string buttonText = "select")
        {
            var _label = new GUIContent(label);
            var _originalPath = new GUIContent("Assets/" + originalPath);
            var _buttonText = new GUIContent(buttonText);
            GUIStyle style = new GUIStyle();
            float labelWIdth = 300f;
            float originalPathWIdth = 300f;
            float buttonTextWIdth = 300f;
            float temp;
            style.CalcMinMaxWidth(_label, out labelWIdth, out temp);
            style.CalcMinMaxWidth(_originalPath, out originalPathWIdth, out temp);
            style.CalcMinMaxWidth(_buttonText, out buttonTextWIdth, out temp);

            GUILayout.BeginHorizontal();
            GUILayout.Label(_label, style, GUILayout.Width(labelWIdth + 5));
            GUILayout.Label(_originalPath, style, GUILayout.Width(originalPathWIdth));
            GUILayout.Label("");

            if (GUILayout.Button(buttonText, GUILayout.Width(buttonTextWIdth + 20)))
            {
                string newPath = EditorUtility.OpenFolderPanel("Create SoObjects at?", "Assets/" + originalPath, "");
                newPath = ReformatThePath(newPath);
                if (newPath != null)
                {
                    originalPath = newPath;
                }
            }
            GUILayout.EndHorizontal();
            return originalPath;
        }

        private static string ReformatThePath(string path)
        {
            var assetsDir = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Assets";
            if (path.Length > 0)
            {
                if (path.Contains(assetsDir))
                {
                    path = path.Replace(assetsDir, "");
                    if (path.Length > 0)
                    {
                        if (path[0] == '/')
                        { path = path.Remove(0, 1); }
                        else
                        { path = ""; }
                    }
                    return path;
                }
                else
                {
                    Debug.LogError("You cant select folder outside Assets/");
                }
            }
            return null;
        }

        // Add an item to context menu.
        [MenuItem("Assets/SO_System/SetCreatePath/Events")]
        static void setPath(MenuCommand command)
        {
            if (sO_SystemSettings == null) { sO_SystemSettings = SO_SystemSettings.Inistance; }
            sO_SystemSettings.EventSOCreatePath = getSelectedPath();
        }

        // Add an item to context menu.
        [MenuItem("Assets/SO_System/SetCreatePath/Variables")]
        static void setPathVarSO(MenuCommand command)
        {
            if (sO_SystemSettings == null) { sO_SystemSettings = SO_SystemSettings.Inistance; }
            sO_SystemSettings.VarSOCreatePath = getSelectedPath();
        }

        // Add an item to context menu.
        [MenuItem("Assets/SO_System/SetCreatePath/Other")]
        static void setPathSO(MenuCommand command)
        {
            if (sO_SystemSettings == null) { sO_SystemSettings = SO_SystemSettings.Inistance; }
            sO_SystemSettings.SOCreatePath = getSelectedPath();
        }


        private static string getSelectedPath()
        {
            var selected = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(selected);
            if (path.Contains("."))//is file
            {
                var fileNameSTartIndex = path.LastIndexOf('/');
                path = path.Remove(fileNameSTartIndex, path.Length - 1 - fileNameSTartIndex);
            }
            path = path.Remove(0, "Assets/".Length);
            return path;
        }



        /// <summary>
        /// If the scene is changed then call the GetAllScriptableEventsInScene
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void SceneUpdated(Scene current, Scene next)
        {

        }

        /// <summary>
        /// If the Hierarchy has changed then call the GetAllScriptableEventsInScene
        /// </summary>
        private void HierarchyChanged()
        {

        }


#endif
    }
}