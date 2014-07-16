using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if (col.gameObject.layer == 8)
		{
			col.gameObject.GetComponent<PlayerEQ>().digable = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.layer == 8)
		{
			col.gameObject.GetComponent<PlayerEQ>().digable = false;
		}
	}
}
