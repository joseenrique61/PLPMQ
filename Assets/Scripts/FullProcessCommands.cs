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

    private void OnCollisionEnter(Collision collision)
    {
    }
}
