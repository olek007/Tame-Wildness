using UnityEngine;
using System.Collections;

public class RespawnController : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public bool Respawn(GameObject target)
    {
        // TODO: Put your respawn logic here.
        if (target != null)
        {
            if (target.rigidbody2D != null)
                target.rigidbody2D.velocity = Vector2.zero;
            target.transform.position = transform.position;
            return true;
        }
        return false;
    }
}
