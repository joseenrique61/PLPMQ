using Oculus.Interaction;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.XR.CoreUtils;
using UnityEngine;

[Serializable]
public class FullProcessCommands : MonoBehaviour {

    [DataMember]
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
            float otherOffset = BlocksInOrder[i - 1].GetComponent<BlockOffset>().downOffset;
            float thisOffset = BlocksInOrder[i].GetComponent<BlockOffset>().thisOffset;
            BlocksInOrder[i].transform.localPosition = new Vector3(BlocksInOrder[i - 1].transform.localPosition.x, BlocksInOrder[i - 1].transform.localPosition.y - 0.2012f, BlocksInOrder[i - 1].transform.localPosition.z - otherOffset - thisOffset);
            BlocksInOrder[i].transform.localRotation = BlocksInOrder[0].transform.localRotation;
        }
    }

    public void Awake()
    {
        BlocksInOrder.Add(gameObject);
    }
}
