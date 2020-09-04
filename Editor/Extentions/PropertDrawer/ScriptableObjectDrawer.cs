using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawerExtended
{

    protected ScriptableObject SORef;
    protected int OriginalIndent;
    protected Rect leftRow;
    protected Rect rightRow;
    protected Color color;
    protected bool isInArrayView;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //init --------------------------------------------------
        label = EditorGUI.BeginProperty(position, label, property);
        SORef = (ScriptableObject)property.objectReferenceValue;
        isInArrayView = OriginalIndent > 0 && label.text.StartsWith("Element");
        label.text = (SORef && isInArrayView) ? SORef.name : label.text; // check if the property is in arrray or if no variable assigned to show the variable name as label instead of 'Element' word
        OriginalIndent = EditorGUI.indentLevel;

        //build GUI--------------------------------------------------
        OnBuildGui(ref position, property, label);

        //finalize --------------------------------------------------
        EditorGUI.indentLevel = OriginalIndent;
        EditorGUI.EndProperty();
    }

    protected virtual void OnBuildGui(ref Rect position, SerializedProperty property, GUIContent label)
    {
        if (!SORef && SO.SO_SystemSettings.Inistance.ShowAssignButton)
        {
            CalculateLAyout(ref position);

            //  uncomment to debug draw area ------------------------------------------------------
            // Debug(position);

            //first field ----------------------------------------------
            CreateFirstFIeld(ref position, property, label);

            //secound field-------------------------------------------
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = .1f;
            CreateSecoundField(property, label);
        }
        else
        {
            AddProberty(ref position, property, label);
        }
        AssignToPropertyIfTheObjectIsCreated(property, label.text);
    }

    protected void CalculateLAyout(ref Rect position)
    {
        float padding = 2f;
        //left row rect
        leftRow = position;
        var totalWidth = position.width;
        leftRow.width = totalWidth * .80f - padding;
        //right row rect
        rightRow = position;
        rightRow.width = totalWidth - padding - leftRow.width;
        rightRow.x += padding + leftRow.width;
    }

    protected void DebugDrawArea(Rect position)
    {
        //if (debug)
        //{
        color = Color.green;
        color.a = .5f;
        DrawColorBox(position, color);
        color = Color.blue;
        color.a = .5f;
        DrawColorBox(leftRow, color);
        color = Color.red;
        color.a = .5f;
        DrawColorBox(rightRow, color);
        //}
    }

    private void CreateFirstFIeld(ref Rect position, SerializedProperty property, GUIContent label)
    {
        if (!isInArrayView)
        {
            AddProberty(ref leftRow, property, label);
        }
        else
        {
            AddProberty(ref position, property, label);
        }
    }

    static UnityEngine.Object objectToBeAssignedRef = null;
    private void CreateSecoundField(SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue == null && !isInArrayView)
        {
            if (GUI.Button(rightRow, "Assign"))
            {
                AssignAsync(property, label.text);
            }
        }
        AssignToPropertyIfTheObjectIsCreated(property, label.text);
    }

    //this logic implemented as when new SO is created it is not created on same frame so this logic is to wait for it then assign it when created
    private protected void AssignToPropertyIfTheObjectIsCreated(SerializedProperty property, string label)
    {
        if (objectToBeAssignedRef != null)// if object was found or created using async assign func
        {
            if (property.objectReferenceValue == null)//if property not assigned yet
            {
                if (label == objectToBeAssignedRef.name)// if the object that was found or created have same name of the current property
                {
                    property.objectReferenceValue = objectToBeAssignedRef; // assign the property
                }
            }
            else
            {
                if (objectToBeAssignedRef == property.objectReferenceValue) // if this properrty finally assigned to the same found or created obj
                {
                    objectToBeAssignedRef = null; // clear the refrence as property already assignd
                }                          //couldn't do that in same like of assign functions as property is not pass ny refrence so i end up using old version of property
                                           //what makes me get old property is the popup that confirm the creation 
            }
        }
    }

    async protected void AssignAsync(SerializedProperty property, string label)
    {
        await Task.Delay(1);
        objectToBeAssignedRef = AutoAssignSO(label, getCreatePath(), property);
        property.objectReferenceValue = objectToBeAssignedRef; //try to assign directly after it "will fail if popup appeared"
    }

    protected virtual string getCreatePath()
    {
        return SO.SO_SystemSettings.Inistance.SOCreatePath;
    }
}
