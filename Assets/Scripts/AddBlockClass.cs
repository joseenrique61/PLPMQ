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
                if (FullProcessCommands.BlocksInOrder.IndexOf(block) % 2 != 0)
                {
                    block.GetComponent<MeshCollider>().enabled = false;
                    block.GetComponent<BoxCollider>().enabled = false;
                }

                block.transform.parent = transform.parent;
                block.transform.position = new Vector3(block.transform.position.x + 0.5f, block.transform.position.y, block.transform.position.z);
                Destroy(Flecha);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(Flecha);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(Flecha, new Vector3(gameObject.transform.position.x - 0.8f - gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z), gameObject.transform.rotation);
        Debug.Log($"Collision! {gameObject.name} skdf {other.name}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
    }
}
