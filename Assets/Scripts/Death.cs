using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public GameObject DeathCause;


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject == DeathCause)
		{
			PlayerEQ.AliveSet();
			Time.timeScale=0.00000000000001f;
			Destroy(gameObject,5.0f);

		}
	}

}
