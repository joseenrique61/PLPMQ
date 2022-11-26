using System.Collections;
using System.Collections.Generic;
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
        SetBlockPosition(index);
    }

    public static void RemoveBlock(GameObject block)
    {
        BlocksInOrder.Remove(block);
        block.transform.SetParent(null);
    }

    private static void SetBlockPosition(int index)
    {
        for (int i = index; i < BlocksInOrder.Count; i++)
        {
            BlocksInOrder[i].transform.localPosition = new Vector3(BlocksInOrder[0].transform.localPosition.x + 0.005f, BlocksInOrder[0].transform.localPosition.y - 2.0116f * i, BlocksInOrder[0].transform.localPosition.z);
        }
    }

    public void Awake()
    {
        BlocksInOrder.Add(gameObject);
    }
}
