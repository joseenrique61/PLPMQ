using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockClass : MonoBehaviour
{
    public GameObject Flecha;

    private GameObject flechaInst;

    public Transform parent;

    [HideInInspector]
    public bool selected = false;

    [HideInInspector]
    public bool hovered = false;

    private List<Collider> colliders = new();

    private GameObject mainCollider;

    public Transform initialParent;

    [HideInInspector]
    public Vector3 initialPosition;

    [HideInInspector]
    public Quaternion initialRotation;

    public void Awake()
    {
        mainCollider = new();
        initialPosition = initialParent.transform.position;
        initialRotation = initialParent.transform.rotation;
    }

    public void Update()
    {
        if (initialParent.parent != null && !initialParent.CompareTag("Inicio") && OVRInput.GetUp(OVRInput.RawButton.A) && hovered)
        {
            FullProcessCommands.RemoveBlock(initialParent.gameObject, initialPosition, initialRotation);
        }
    }

    public void Selected()
    {
        selected = true;
    }

    public void Unselected()
    {
        selected = false;
        CheckColliders();
    }

    public void CheckColliders()
    {
        if (colliders.Count > 0)
        {
            AddThisBlock(mainCollider);
        }
    }

    public void Hovered()
    {
        hovered = true;
    }

    public void Unhovered()
    {
        hovered = false;
    }

    public void AddThisBlock(GameObject otherBlock)
    {
        if (!FullProcessCommands.BlocksInOrder.Contains(initialParent.gameObject))
        {
            if (FullProcessCommands.BlocksInOrder.Contains(otherBlock))
            {
                FullProcessCommands.AddBlock(FullProcessCommands.BlocksInOrder.IndexOf(otherBlock) + 1, initialParent.gameObject, otherBlock.transform.parent);

                //if (gameObject.CompareTag("Fin"))
                //{
                //    SyntaxChecker sy = new();
                //    Debug.LogWarning(sy.CheckSyntax());
                //}

                Destroy(flechaInst);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hasAddBlockClass = other.gameObject.TryGetComponent(out AddBlockClass otherAddBlockClass);
        if (hasAddBlockClass)
        {
            if (other.gameObject.layer == 6 && otherAddBlockClass.initialParent != initialParent && otherAddBlockClass.initialParent.parent == parent && otherAddBlockClass.initialParent.gameObject.GetComponent<Properties>().Type != Properties.TypeEnum.Fin)
            {
                colliders.Add(other);
                if (colliders.Count > 0 || FullProcessCommands.BlocksInOrder.Find(x => x == otherAddBlockClass.initialParent.gameObject) == FullProcessCommands.BlocksInOrder[^1])
                {
                    if (selected)
                    {
                        Collider collider = colliders.Count > 1 ? colliders[0].GetComponent<AddBlockClass>().initialParent.position.y > colliders[1].GetComponent<AddBlockClass>().initialParent.position.y ? colliders[0] : colliders[1] : colliders[0];
                        Destroy(flechaInst);
                        flechaInst = Instantiate(Flecha, new Vector3(collider.transform.position.x - 0.2f - collider.GetComponent<MeshRenderer>().bounds.size.x / 2, collider.transform.position.y - collider.GetComponent<MeshRenderer>().bounds.size.y / 2, collider.transform.position.z), new Quaternion(collider.transform.rotation.x, collider.transform.rotation.y + 90, collider.transform.rotation.z, collider.transform.rotation.w));
                        mainCollider = collider.GetComponent<AddBlockClass>().initialParent.gameObject;
                    } 
                }
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(flechaInst);
        colliders.Remove(other);
    }
}
