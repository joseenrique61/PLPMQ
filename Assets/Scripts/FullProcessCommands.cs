using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullProcessCommands : MonoBehaviour {
    
    private static List<GameObject> blocksInOrder = new List<GameObject>();
    
    public static List<GameObject> BlocksInOrder
    {
        get { return blocksInOrder; }
        set { blocksInOrder = value; }
    }

    public static void AddBlock(int index, GameObject block, Transform parent)
    {
        BlocksInOrder.Insert(index, block);
        block.transform.SetParent(parent);
        for (int i = index; i < blocksInOrder.Count; i++)
        {
            BlocksInOrder[i].transform.localPosition = new Vector3(BlocksInOrder[0].transform.localPosition.x, BlocksInOrder[0].transform.localPosition.y - 2.02f * i, BlocksInOrder[0].transform.localPosition.z);
        }
    }

    public void Awake()
    {
        BlocksInOrder.Add(gameObject);
    }
}
