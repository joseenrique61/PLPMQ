using UnityEngine;

public class SetInicioCollider : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector3(boxCollider.size.x + 2, boxCollider.size.y, boxCollider.size.z);
        }
	}
}
