using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Exportwindow : EditorWindow
{
    [MenuItem("Tools/ExportButtonClicker")]
    private static void ShowWindow()
    {
        GetWindow(typeof(Exportwindow));
    }

    private void OnGUI()
    {
        string language = "";
        language = EditorGUILayout.TextField("Language", language);
        if (GUILayout.Button("Click"))
        {
            Exporter exporter = new(language);
            exporter.Export();
        }
    }
}
