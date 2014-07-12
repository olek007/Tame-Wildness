using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public GameObject player;
	public float movementSpeed = 10;
	public float jumpHeight = 1000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			player.transform.Translate(new Vector2(-1 * movementSpeed * Time.deltaTime, 0));
			//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 *  movementSpeed * 1000.0f * Time.deltaTime, 0));
		}
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			player.transform.Translate(new Vector2(1 * movementSpeed * Time.deltaTime, 0));
		}

		if (Input.GetButtonDown("Jump"))
	    {
			player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
		}

		
	
	
	}
}
