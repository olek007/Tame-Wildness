using UnityEngine;
using System.Collections;

public class Digging : MonoBehaviour {

	public bool digable;

	void OnTriggerStay2D(Collider2D col)
	{
		
		if (col.gameObject.layer == 8)
		{
			col.gameObject.GetComponent<PlayerEQ>().digable = true;
		}
	}
}
