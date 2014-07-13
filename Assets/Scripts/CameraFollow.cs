using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;

	void Update () 
	{
		transform.position = new Vector3(player.transform.position.x, 0, -8);
		
	}

}
