using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		
	}
	
	void LateUpdate()
	{
		if(transform.position.y < 0.25f)
		{
			transform.position = new Vector3(transform.position.x, 0, -10);
		}
		
	}

}
