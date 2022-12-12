using UnityEditor;
using UnityEngine;

public class ExcerciseCheckerWindow : EditorWindow
{
    public GameObject checker;

    [MenuItem("Tools/CheckExcercise")]
    private static void ShowWindow()
    {
        GetWindow(typeof(ExcerciseCheckerWindow));
    }

    private void OnGUI()
    {
        checker = EditorGUILayout.ObjectField(checker, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Check"))
        {
            checker.GetComponent<ExcerciseChecker>().CheckExcercise();
        }
    }
}
