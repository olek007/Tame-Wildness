using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject == player)
		{
			PlayerEQ.alive = false;
			PlayerControls.canWalk = false;
			Destroy(player, 5.0f);	
		}
	}
}
