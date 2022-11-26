using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockClass : MonoBehaviour
{
    public GameObject Flecha;

    private GameObject flechaInst;

    public Transform parent;

    public bool selected = false;

    private List<Collider> colliders = new List<Collider>();

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
                    SyntaxChecker sy = new();
                    sy.CheckSyntax();
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
        colliders.Add(other);
        if (selected)
        {
            Collider collider = colliders.Count > 1 ? colliders[0].gameObject.transform.position.y < colliders[1].gameObject.transform.position.y ? colliders[0] : colliders[1] : colliders[0];
            flechaInst = Instantiate(Flecha, new Vector3(collider.transform.position.x - 0.4f - collider.GetComponent<MeshRenderer>().bounds.size.x / 2, collider.transform.position.y + collider.GetComponent<MeshRenderer>().bounds.size.y / 2, collider.transform.position.z), new Quaternion(collider.transform.rotation.x, collider.transform.rotation.y + 90, collider.transform.rotation.z, collider.transform.rotation.w));
            Debug.Log($"Trigger! {gameObject.name} with {collider.name}");
            AddThisBlock(collider.gameObject);
            DisableSelected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(flechaInst);
        colliders.Remove(other);
    }
}
