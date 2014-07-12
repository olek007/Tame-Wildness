using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	public GameObject Player;


	public void Push()
	{
		Vector2 Dimension;
		Dimension.x = gameObject.transform.position.x - Player.transform.position.x;
		Dimension.y = gameObject.transform.position.y - Player.transform.position.y;
		float Distance;
		Distance = Vector2.Distance (gameObject.transform.position, Player.transform.position);
		Dimension.x /= Distance;
		Dimension.y /= Distance;
		gameObject.rigidbody2D.AddForce (Dimension);
	}

	void OnMouseDown()
	{
		switch (pushForceLvl)
		{
			case 1:
			{
				Push(
			}
			break;
		}

	}
}
