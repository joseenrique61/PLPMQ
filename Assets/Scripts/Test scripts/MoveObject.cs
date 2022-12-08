using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveObject : MonoBehaviour
{
    public Transform parent;

    public Vector3 increment;

    AddBlockClass addBlockClass;

    private void Awake()
    {
        AddBlockClass addBlockClass = gameObject.GetComponent<AddBlockClass>();
        this.addBlockClass = addBlockClass;
    }

    void FixedUpdate()
    {
        if (addBlockClass.initialParent.parent != parent)
        {
            addBlockClass.initialParent.position += new Vector3(increment.x, increment.y, increment.z);
            addBlockClass.CheckColliders();
        }
        else
        {
            DeleteBlock();
        }
    }

    private void DeleteBlock()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            FullProcessCommands.RemoveBlock(addBlockClass.initialParent.gameObject, addBlockClass.initialPosition, addBlockClass.initialRotation);
            increment = Vector3.zero;
        }
    }
}
