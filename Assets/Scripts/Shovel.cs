using UnityEngine;
using System.Collections;

public class Shovel : MonoBehaviour {

	public Collider2D colHole;
	public GameObject hole;
	public Transform holePlace;
	private GameObject dziura;

	public void Dig()
	{
		colHole.enabled = false;
	}

	public void Digged()
	{
		if (!dziura)
		{
			dziura = Instantiate(hole, new Vector2(holePlace.position.x, holePlace.position.y), colHole.transform.rotation) as GameObject;
		}
	}
	
}
