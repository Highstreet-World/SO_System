using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public abstract class PropertyDrawerExtended : PropertyDrawer
{
    protected Rect position;
    protected void SetPosition(Rect Position)
    {
        position = Position;
    }

    protected float GetPropertyHeight(SerializedProperty property, bool includeChildren)
    {
        return EditorGUI.GetPropertyHeight(property, includeChildren);
    }
    protected float AddHelp(string text, MessageType type = MessageType.None, float overrideHeight = -1)
    {
        return AddHelp(ref position, text, type, overrideHeight);
    }


    protected float AddHelp(ref Rect _position, string text, MessageType type = MessageType.None, float overrideHeight = -1)
    {
        GUIStyle myStyle = new GUIStyle(EditorStyles.helpBox);
        myStyle.richText = true;
        myStyle.fixedWidth = _position.width;
        myStyle.fontSize = 9;
        myStyle.wordWrap = false;

        GUIContent guiContent = new GUIContent(text);

        if (overrideHeight == -1) _position.height = myStyle.CalcHeight(guiContent, _position.width);
        else _position.height = overrideHeight;

        // myStyle.fixedHeight = _position.height;

        // GUIStyle myStyle = new GUIStyle(EditorStyles.textArea);
        //  _position.y += 4f;
        // text = EditorGUI.TextArea(_position, guiContent.text, myStyle);

        //Debug.Log(myStyle.CalcHeight(guiContent, _position.width));
        //Debug.Log("position.width: "+ _position.width);
        EditorGUI.HelpBox(_position, text, type);


        position.y += _position.height;
        return _position.height;
    }

    protected Rect AddLabel(GUIContent label, float overrideHeight = -1)
    {
        return AddLabel(ref position, label, overrideHeight);
    }
    protected Rect AddLabel(ref Rect _position, GUIContent label, float overrideHeight = -1)
    {
        if (overrideHeight == -1) overrideHeight = calculateTextHeight(label.text, _position);
        _position.height = overrideHeight;
        position.y += _position.height;
        return EditorGUI.PrefixLabel(_position, label);
    }

    protected float calculateTextHeight(string text, Rect _position)
    {
        GUIStyle myStyle = new GUIStyle(EditorStyles.textArea);
        myStyle = new GUIStyle(EditorStyles.helpBox);
        myStyle.fixedWidth = _position.width;
        myStyle.fontSize = 9;
        GUIContent guiContent = new GUIContent(text);

        return myStyle.CalcHeight(guiContent, _position.width);
    }

    protected void AddProberty(SerializedProperty property, GUIContent label = null, float overrideHeight = -1)
    {
        AddProberty(ref position, property, label, overrideHeight);
    }
    protected void AddProberty(ref Rect _position, SerializedProperty property, GUIContent label = null, float overrideHeight = -1)
    {
        if (overrideHeight == -1) overrideHeight = EditorGUI.GetPropertyHeight(property);
        _position.height = overrideHeight;
        if (label == null)
        {
            EditorGUI.PropertyField(_position, property);
        }
        else
        {
            EditorGUI.PropertyField(_position, property, label);
        }
        position.y += _position.height;
    }

    protected static void DrawBorderBox(Color fillColor, float borderWidth, Color borderColor, Rect boxRect)
    {
        DrawColorBox(boxRect, borderColor);
        boxRect.x += borderWidth;
        boxRect.y += borderWidth;
        boxRect.width -= 2 * borderWidth;
        boxRect.height -= 2 * borderWidth;
        DrawColorBox(boxRect, fillColor);
    }

    protected static void DrawColorBox(Rect position, Color fillColor)
    {
        Texture2D texture = CreateTexture(fillColor);
        EditorGUI.DrawTextureTransparent(position, texture);
    }

    private static Texture2D CreateTexture(Color fillColor)
    {
        var texture = new Texture2D((int)1, (int)1);
        var fillColorArray = texture.GetPixels();
        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            fillColorArray[i] = fillColor;
        }
        texture.SetPixels(fillColorArray);
        texture.Apply();
        return texture;
    }

    private static System.Collections.Generic.IEnumerable<Type> cashedAssembliesTypes;
    private static readonly Dictionary<string, Type> cahsedTypesNames = new Dictionary<string, Type>();
    protected Type GetPropertyTypeFromName(string propertyTypeName)
    {
        if (!cahsedTypesNames.ContainsKey(propertyTypeName))
        {
            if (cashedAssembliesTypes == null)
            {
                cashedAssembliesTypes = AppDomain.CurrentDomain
                           .GetAssemblies()
                           .SelectMany(x => x.GetTypes());
            }
            cahsedTypesNames[propertyTypeName] = cashedAssembliesTypes.FirstOrDefault(t => t.Name == propertyTypeName);
        }
        return cahsedTypesNames[propertyTypeName];
    }

    protected string GetPropertyTypeName(SerializedProperty property)
    {
        var type = property.type;
        var match = System.Text.RegularExpressions.Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success)
            type = match.Groups[1].Value;
        return type;
    }

    protected UnityEngine.Object AutoAssignSO(string newAssetname, string path, SerializedProperty property)
    {
        newAssetname = newAssetname.Trim();
        var propertyType = getSoType(property);

        //check if avaliable before
        UnityEngine.Object foundSo = SearchForAsset(newAssetname, propertyType);
        if (!foundSo && newAssetname.IndexOf(" ") > -1) //try search for same name withought spaces
        {
            foundSo = SearchForAsset(newAssetname.Replace(" ", ""), propertyType);
        }
        //ask if return the old one 
        if (foundSo)
        {
            return foundSo;
        }
        else
        {
            var answer = EditorUtility.DisplayDialog("Auto Assign SO", "Couldn't find SOAsset: ( " + newAssetname + ".asset ). ", "Create New", "Cancel");
            Debug.Log(answer);
            if (answer)//create new
            {
                return CreateNewSO(newAssetname, ref path, property); ;
            }
            else
            {
                return null;
            }
        }
    }

    private UnityEngine.Object CreateNewSO(string newAssetname, ref string path, SerializedProperty property)
    {
        //create new one
        if (path.Length > 0 && path[path.Length - 1] != '/') path += '/';
        path = "Assets/" + path + "{0}.asset";
        UnityEngine.Object newSO = null;
        var j = 0;
        var generatedName = newAssetname;
        do
        {
            generatedName = newAssetname + (j == 0 ? "" : (" " + j));
            newSO = AssetDatabase.LoadAssetAtPath(
                string.Format(path, generatedName),
                    typeof(ScriptableObject)
                );
            j++;
        } while (newSO != null);

        newSO = ScriptableObject.CreateInstance(getSoType(property));
        AssetDatabase.CreateAsset(newSO, string.Format(path, generatedName));
       
        return newSO;
    }

    private static UnityEngine.Object SearchForAsset(string newAssetname, Type propertyType)
    {
        bool isFound = false;
        UnityEngine.Object foundSo = null;
        var alreadyExistAssets = AssetDatabase.FindAssets(string.Format("{0} t:{1}", newAssetname, propertyType));
        for (int i = 0; i < alreadyExistAssets.Length && !isFound; i++)
        {
            foundSo = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(alreadyExistAssets[i]), propertyType);
            if (foundSo.name.Trim().ToLower() == newAssetname.Trim().ToLower())
            {
                isFound = true;
                break;
            }
        }
        if (isFound)
            return foundSo;
        else
            return null;
    }

    protected Type getSoType(SerializedProperty property)
    {
        var propertyTypeName = GetPropertyTypeName(property);
        return GetPropertyTypeFromName(propertyTypeName);
    }

}
