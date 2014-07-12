using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public GameObject Player;

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject == Player)
		{
			PlayerEQ.Alive = false;
			Time.timeScale=0.00000000000001f;
			Destroy(Player,5.0f);
			Destroy(gameObject);
		}
	}
}
