using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FullProcessCommands : MonoBehaviour {
    
    private static List<GameObject> blocksInOrder = new();
    
    public static List<GameObject> BlocksInOrder
    {
        get { return blocksInOrder; }
        set { blocksInOrder = value; }
    }

    public static void AddBlock(int index, GameObject block, Transform parent)
    {
        BlocksInOrder.Insert(index, block);
        block.transform.SetParent(parent);
        block.GetComponent<Grabbable>().MaxGrabPoints = 0;
        block.GetNamedChild("GrabInteractable").GetComponent<Grabbable>().MaxGrabPoints = 0;
        SetBlockPosition(index);
    }

    public static void RemoveBlock(GameObject block, Vector3 initialPosition, Quaternion initialRotation)
    {
        BlocksInOrder.Remove(block);
        block.GetComponent<Grabbable>().MaxGrabPoints = 1;
        block.GetNamedChild("GrabInteractable").GetComponent<Grabbable>().MaxGrabPoints = -1;
        block.transform.SetParent(null);
        block.transform.SetPositionAndRotation(initialPosition, initialRotation);
        SetBlockPosition(1);
    }

    private static void SetBlockPosition(int index)
    {
        for (int i = index; i < BlocksInOrder.Count; i++)
        {
            BlocksInOrder[i].transform.localPosition = new Vector3(BlocksInOrder[0].transform.localPosition.x + 0.00055f, BlocksInOrder[0].transform.localPosition.y - 0.2012f * i, BlocksInOrder[0].transform.localPosition.z);
            BlocksInOrder[i].transform.localRotation = BlocksInOrder[0].transform.localRotation;
        }
    }

    public void Awake()
    {
        BlocksInOrder.Add(gameObject);
    }
}
