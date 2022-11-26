using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Properties : MonoBehaviour
{
    public enum TypeEnum
    {
        Process,
        Conditional,
        While,
        Assign,
        Inicio,
        Fin
    };

    //[HideInInspector]
    public TypeEnum Type;

    [HideInInspector]
    public string Condition;

    [HideInInspector]
    public string Name;

    [HideInInspector]
    public int Cant;

    [HideInInspector]
    public string Instruction;
}

[CustomEditor(typeof(Properties))]
public class MyEditorClass : Editor
{
    public override void OnInspectorGUI()
    {
        // If we call base the default inspector will get drawn too.
        // Remove this line if you don't want that to happen.
        base.OnInspectorGUI();

        Properties myBehaviour = target as Properties;

        if (myBehaviour.Type == Properties.TypeEnum.Process)
        {
            // Draw the optional fields here
            EditorGUI.indentLevel++;
            myBehaviour.Instruction = EditorGUILayout.TextField("Instruction", myBehaviour.Instruction);
            EditorGUI.indentLevel--;
        }
        else if (myBehaviour.Type == Properties.TypeEnum.Conditional)
        {
            // Draw the optional fields here
            EditorGUI.indentLevel++;
            myBehaviour.Condition = EditorGUILayout.TextField("Condition", myBehaviour.Condition);
            EditorGUI.indentLevel--;
        }
        else if (myBehaviour.Type == Properties.TypeEnum.While)
        {
            // Draw the optional fields here
            EditorGUI.indentLevel++;
            myBehaviour.Condition = EditorGUILayout.TextField("Condition", myBehaviour.Condition);
            EditorGUI.indentLevel--;
        }
        else if (myBehaviour.Type == Properties.TypeEnum.Assign)
        {
            // Draw the optional fields here
            EditorGUI.indentLevel++;
            myBehaviour.Name = EditorGUILayout.TextField("Name", myBehaviour.Name);
            myBehaviour.Instruction = EditorGUILayout.TextField("Instruction", myBehaviour.Instruction);
            myBehaviour.Cant = myBehaviour.Instruction.Length + 1;
            EditorGUI.indentLevel--;
        }
    }
}