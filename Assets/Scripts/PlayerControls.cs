using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public GameObject player;
	public float movementSpeed = 10;
	public float jumpHeight = 50;
	public static List<GameObject> boomableItems = new List<GameObject>();
	public static int pushForceLvl;
	public float CDtime = 3.0f;
	public static float timeSinceSpellCast = 0;
	public static bool isOnCD;
	private bool canJump = true;
	public Animator anim;

	// Use this for initialization
	void Start () {
	
		pushForceLvl = 1;
		isOnCD = false;
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow))
		{
			player.transform.Translate (new Vector2 (-1 * movementSpeed * Time.deltaTime, 0));
			//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 *  movementSpeed * 1000.0f * Time.deltaTime, 0));
			anim.SetBool ("walking", true);

		} else
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			player.transform.Translate (new Vector2 (1 * movementSpeed * Time.deltaTime, 0));
			//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 *  movementSpeed * 1000.0f * Time.deltaTime, 0));
			anim.SetBool ("walking", true);
		} else 
		{
			anim.SetBool("walking", false);
		}

		if (Input.GetButtonDown("Jump"))
	    {
			if(canJump)
			{
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
			}
		}

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
	}
	
	void LateUpdate()
	{
		transform.rotation = new Quaternion(0,0,0,0);
		
		//if(gameObject.collider2D.
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
	
}
