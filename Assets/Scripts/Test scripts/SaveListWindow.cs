using UnityEngine;
using UnityEditor;

public class SaveListWindow : EditorWindow
{
    [MenuItem("Tools/SaveList")]
    private static void ShowWindow()
    {
        GetWindow(typeof(SaveListWindow));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            ListCommands.SaveList();
        }
    }
}
