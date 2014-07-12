using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	public GameObject player;

	void Push()
	{
		Vector2 Dimension;
		Dimension.x = transform.position.x - player.transform.position.x;
		Dimension.y = transform.position.y - player.transform.position.y;
		float Distance;
		Distance = Vector2.Distance (transform.position, player.transform.position);
		Dimension.x /= Distance;
		Dimension.y /= Distance;
		gameObject.rigidbody2D.AddForce(Dimension);
		
	}
	
	void Drag()
	{
		
	
	}

	void OnMouseDown()
	{
		
		switch (PlayerControls.pushForceLvl)
		{
			
			case 2:
			{
				Push();
			}
			break;
			
			case 3:
			{
				//Drag();
			}
			break;

		}

	}
}
