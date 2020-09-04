using UnityEngine;
using SO;
using UnityEngine.UI;

namespace DebugStuff
{
    public class ConsoleToGUI : MonoBehaviour
    {
        public Text log;
        //#if !UNITY_EDITOR
        static string myLog = "";
        private string output;
        private string stack;

        void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;

            string tColor = (type == LogType.Error ? "red" : (type == LogType.Warning ? "yellow" : "white"));
            stack = type == LogType.Error? "\n"+stack : "";
              myLog = string.Format("<color={0}>{1}</color>{2}\n{3}", tColor, output, stack, myLog );//= output + "\n" + myLog;
            if (myLog.Length > 5000)
            {
                myLog = myLog.Substring(0, 4000);
            }
            log.text =myLog;
        }


        public void TestError()
        {
            Debug.LogError("error test");
        }      
        public void TestWarning()
        {
            Debug.LogWarning("warning test");
        }
        public void TestLog()
        {
            Debug.Log("Log test");
        }

        //void OnGUI()
        //{
        //    //if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
        //    {
        //        myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
        //    }
        //}
        //#endif
    }
}