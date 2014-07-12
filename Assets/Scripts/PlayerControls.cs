using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public GameObject player;
	public float movementSpeed = 10;
	public float jumpHeight = 1000;
	public static List<GameObject> boomableItems = new List<GameObject>();
	public static int pushForceLvl;
	public float CDtime = 3.0f;
	public static float timeSinceSpellCast = 0;
	public static bool isOnCD;

	// Use this for initialization
	void Start () {
	
		pushForceLvl = 1;
		isOnCD = false;
	
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
<<<<<<< HEAD
=======
		
		if((Input.GetButtonDown("Fire1")) && (pushForceLvl == 1) && (!isOnCD))
		{
			Boom();
		}
		
		
		if(isOnCD)
		{
			timeSinceSpellCast += Time.deltaTime;
			
			if(timeSinceSpellCast >= CDtime)
			{
				isOnCD = false;
			}	
		}
>>>>>>> origin/master
	}
	
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider2D.gameObject.layer == 4)
		{
			boomableItems.Add(collider.gameObject);	
		}
	
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider2D.gameObject.layer == 4)
		{
			boomableItems.Remove(collider.gameObject);	
		}
		
	}
<<<<<<< HEAD
=======
	
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
		
		StartCD();
	}
	
	public static void StartCD()
	{
		timeSinceSpellCast = 0;
		isOnCD = true;
	}
	
	
>>>>>>> origin/master
}
