using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	public GameObject Player;

	public void Force(GameObject ForceableItem)
	{
		Vector2 Dimension;
		Dimension.x = ForceableItem.transform.position.x - Player.transform.position.x;
		Dimension.y = ForceableItem.transform.position.y - Player.transform.position.y;
		float Distance;
		Distance = Vector2.Distance (ForceableItem.transform.position, Player.transform.position);
		Dimension.x /= Distance;
		Dimension.y /= Distance;
		ForceableItem.rigidbody2D.AddForce (Dimension);
	}
}
