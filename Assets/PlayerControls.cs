using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public static Rigidbody2D player;
	public float movementSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			player.transform.Translate(new Vector2(-1 * movementSpeed * Time.deltaTime, 0));
		}
	
	}
}
