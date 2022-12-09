using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Exportwindow : EditorWindow
{
    //[SerializeField]
    //public string language;

    //[SerializeField]
    //public TextMeshPro text;

    //[MenuItem("Tools/ExportButtonClicker")]
    //private static void ShowWindow()
    //{
    //    GetWindow(typeof(Exportwindow));
    //}

    //private void OnGUI()
    //{
    //    //ExportWindowClass exportWindowClass = (ExportWindowClass)target;
    //    //if (GUI.changed)
    //    //{
    //    //    EditorUtility.SetDirty(castedTarget);
    //    //    EditorSceneManager.MarkSceneDirty(castedTarget.gameObject.scene);
    //    //}
        
    //    language = EditorGUILayout.TextField("Language", language);
    //    text = EditorGUILayout.ObjectField("Text", text, typeof(TextMeshPro), true) as TextMeshPro;

    //    if (GUILayout.Button("Click"))
    //    {
    //        Exporter exporter = ScriptableObject.CreateInstance("Exporter") as Exporter;
    //        exporter.Init(language, text);
    //        string text1 = exporter.Export();
    //    }
    //}
}
