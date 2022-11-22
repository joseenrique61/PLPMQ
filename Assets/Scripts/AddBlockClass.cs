using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockClass : MonoBehaviour
{
    public GameObject Flecha;

    public void AddBlock(GameObject block)
    {
        if (FullProcessCommands.BlocksInOrder.Contains(gameObject))
        {
            if (!FullProcessCommands.BlocksInOrder.Contains(block))
            {
                FullProcessCommands.BlocksInOrder.Insert(FullProcessCommands.BlocksInOrder.IndexOf(gameObject) + 1, block);
                block.transform.parent = transform.parent;
                block.transform.position = new Vector3(block.transform.position.x + 0.5f, block.transform.position.y, block.transform.position.z);
                Destroy(Flecha);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Flecha, new Vector3(gameObject.transform.position.x - 0.4f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
    }
}
