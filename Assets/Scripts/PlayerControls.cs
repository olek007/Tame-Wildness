using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public GameObject player;
	public float movementSpeed = 10;
	public float jumpHeight = 1000;
	public static List<GameObject> boomableItems = new List<GameObject>();  // trigger o ustalonym rozmiarze musi być na dziecku playera
	public static int pushForceLvl;

	// Use this for initialization
	void Start () {
	
		pushForceLvl = 1;
	
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
		
		if((Input.GetButtonDown("Fire1")) && (pushForceLvl == 1))
		{
			Boom();
		}
	}
	
	
	void OnTriggerEnter (Collider2D collider)
	{
		
		if(collider2D.gameObject.layer == 4)
		{
			boomableItems.Add(collider.gameObject);	
		}
	
	}
	
	void OnTriggerExit(Collider2D collider)
	{
		if(collider2D.gameObject.layer == 4)
		{
			boomableItems.Remove(collider.gameObject);	
		}
		
	}
	
	void Boom()
	{
		foreach(GameObject boomable in boomableItems)
		{
			Vector2 Dimension;
			Dimension.x = boomable.transform.position.x - player.transform.position.x;
			Dimension.y = boomable.transform.position.y - player.transform.position.y;
			float Distance;
			Distance = Vector2.Distance (boomable.transform.position, player.transform.position);
			Dimension.x /= Distance;
			Dimension.y /= Distance;
			gameObject.rigidbody2D.AddForce(Dimension);
	   	}
			
	}
}
