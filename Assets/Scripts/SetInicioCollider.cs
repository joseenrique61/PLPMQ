using UnityEngine;

public class SetInicioCollider : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            GetComponent<Rigidbody>().detectCollisions = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
