using UnityEngine;
using UnityEditor;
using Unity.XR.CoreUtils;

public class AddBlockWindow : EditorWindow
{
    public Transform parent;

    public Transform block;

    [MenuItem("Tools/AddBlock")]
    private static void ShowWindow()
    {
        GetWindow(typeof(AddBlockWindow));
    }

    private void OnGUI()
    {
        parent = EditorGUILayout.ObjectField(parent, typeof(Transform), true) as Transform;
        block = EditorGUILayout.ObjectField(block, typeof(Transform), true) as Transform;

        if (GUILayout.Button("Add"))
        {
            FullProcessCommands.AddBlock(FullProcessCommands.BlocksInOrder.Count, block.gameObject, parent);
        }
        else if (GUILayout.Button("Remove"))
        {
            FullProcessCommands.RemoveBlock(block.gameObject, block.gameObject.GetNamedChild("Visuals").GetComponentInChildren<AddBlockClass>().initialPosition, block.gameObject.GetNamedChild("Visuals").GetComponentInChildren<AddBlockClass>().initialRotation);
        }
    }
}
