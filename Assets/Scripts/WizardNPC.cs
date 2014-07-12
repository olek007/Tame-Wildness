using UnityEngine;
using System.Collections;

public class WizardNPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		PlayerControls.canWalk = false;
	}
}
