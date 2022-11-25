using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockClass : MonoBehaviour
{
    public GameObject Flecha;

    private GameObject flechaInst;

    public Transform parent;

    public bool selected = false;

    public void EnableSelected()
    {
        selected = true;
    }

    public void DisableSelected()
    {
        selected = false;
    }

    public void AddThisBlock(GameObject otherBlock)
    {
        if (!FullProcessCommands.BlocksInOrder.Contains(gameObject))
        {
            if (FullProcessCommands.BlocksInOrder.Contains(otherBlock))
            {
                FullProcessCommands.AddBlock(FullProcessCommands.BlocksInOrder.IndexOf(otherBlock) + 1, gameObject, otherBlock.transform.parent);

                if (gameObject.CompareTag("Fin"))
                {
                    SyntaxChecker syn = new();
                }

                //if (FullProcessCommands.BlocksInOrder.IndexOf(gameObject) % 2 != 0)
                //{
                //    gameObject.GetComponent<BoxCollider>().enabled = false;
                //}

                Destroy(flechaInst);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (selected)
        {
            flechaInst = Instantiate(Flecha, new Vector3(gameObject.transform.position.x - 0.4f - gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2, gameObject.transform.position.y + gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2, gameObject.transform.position.z), new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + 90, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
            Debug.Log($"Trigger! {gameObject.name} with {other.name}");
            AddThisBlock(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Destroy(flechaInst);
    }
}
