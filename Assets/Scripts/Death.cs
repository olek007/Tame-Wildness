using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject == player)
		{
			PlayerEQ.alive = false;
			Time.timeScale=0.00000000000001f;
			Destroy(player,5.0f);
		}
	}
}
