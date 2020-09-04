using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using TMPro;
using System;

public class SetUiText : MonoBehaviour
{
    enum TextType
    {
        text, textMP
    }


    [Header("data Source")]
    public IVariableSO[] DataSources;
    [Header("Settings")]
    [TextArea]
    public string OutputFormat = "{0}/{1}";
    [Header("Text to update")]
    [SerializeField]
    private TextType textType;
    public GameObject[] UiTextRefrences;
    [Header("Update mode")]
    public bool OnVariablesUpdated;
    public bool OnStart;

    private void Awake()
    {
        for (int i = 0; i < DataSources.Length; i++)
        {
            DataSources[i].Subscripe(OnVariableUpdated);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < DataSources.Length; i++)
        {
            DataSources[i].UnSubscripe(OnVariableUpdated);
        }
    }

    private void OnVariableUpdated(object sender, EventArgs e)
    {
        if (OnVariablesUpdated)
        {
            UpdateText();
        }
    }

    private void Start()
    {
        if (OnStart)
        {
            UpdateText();
        }
    }
    public void UpdateText()
    {
        List<string> StringData = new List<string>();

        for (int i = 0; i < DataSources.Length; i++)
        {
            if (DataSources[i] is StringSO)
            {
                StringData.Add(((StringSO)DataSources[i]).GetValue());
            }
            else if (DataSources[i] is IntSO)
            {
                StringData.Add(((IntSO)DataSources[i]).GetValue().ToString());
            }
            else if (DataSources[i] is FloatSO)
            {
                StringData.Add(((FloatSO)DataSources[i]).GetValue().ToString());
            }
            else
            {
                Debug.LogError("Unhandled SO type");
            }
        }
        if (StringData.Count != UiTextRefrences.Length)
        {

            for (int i = 0; i < UiTextRefrences.Length; i++)
            {
                if (textType == TextType.text)
                {
                    UiTextRefrences[i].GetComponent<Text>().text = string.Format(OutputFormat, StringData.ToArray());
                }
                else
                {
                    var text = UiTextRefrences[i].GetComponent<TMP_Text>();
                    if (text != null)
                        text.text = string.Format(OutputFormat, StringData.ToArray());
                }

                Debug.Log(string.Format(OutputFormat, StringData.ToArray()));
            }
        }
        else
        {
            Debug.LogError("Not all the variables converted to string successfully");
        }
    }

}
