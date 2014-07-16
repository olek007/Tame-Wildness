using UnityEngine;
using System.Collections;

public class Shovel : MonoBehaviour {

	public Collider2D colHole;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Dig()
	{
		colHole.enabled = false;
	}
	
}
