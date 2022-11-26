using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform parent;

    public Vector3 increment;

    void FixedUpdate()
    {
        if (transform.parent != parent)
        {
            transform.position += new Vector3(increment.x, increment.y, increment.z);
        }
    }
}
