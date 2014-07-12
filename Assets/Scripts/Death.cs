using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public GameObject DeathCause;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject == DeathCause)
		{
			Destroy(gameObject);

		}
	}

}
