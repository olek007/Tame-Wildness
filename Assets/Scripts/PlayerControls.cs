using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public GameObject player;
	private float movementSpeed = 10;
	public float jumpHeight = 50;
	public static List<GameObject> boomableItems = new List<GameObject>();
	public static int pushForceLvl;
	public float CDtime = 3.0f;
	public static float timeSinceSpellCast = 0;
	public static bool isOnCD;
	private bool canJump = true;
	private Animator anim;
	public static float pushForce = 2000000;   //1000000
	public static bool canWalk;

	void Start ()
	{
		pushForceLvl = 3;
		isOnCD = false;
		anim = GetComponent<Animator> ();       
		canWalk = true;
	}
	
	void Update () 
	{
		anim.SetFloat("speed", player.GetComponent<Rigidbody2D>().velocity.x);
		Debug.Log(player.GetComponent<Rigidbody2D>().velocity.x);
		if (canWalk)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				anim.SetBool("move", true);
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
				transform.rotation = new Quaternion(0, 180, 0, 0);
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				anim.SetBool("move", true);
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
				transform.rotation = new Quaternion(0, 0, 0, 0);
			}
			else
			{
				anim.SetBool("move", false);
			}

			if (Input.GetButtonDown("Jump"))
			{
				if (canJump)
				{
					player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000 * jumpHeight);
				}
			}

			if ((Input.GetButtonDown("Fire1")) && (pushForceLvl == 1) && (!isOnCD))
			{
				Boom();
			}
		}
		else
		{
			anim.SetBool("move", false);
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
		bool wasBoomUsed = false;
		foreach(GameObject boomable in Radar.usables)
		{
			
			if(Vector2.Distance(boomable.transform.position, transform.position) < 20)
			{
				Vector2 Dimension;
				Dimension.x = boomable.transform.position.x - player.transform.position.x;
				Dimension.y = boomable.transform.position.y - player.transform.position.y;
				float Distance;
				Distance = Vector2.Distance (boomable.transform.position, player.transform.position);
				Dimension.x /= Distance;
				Dimension.y /= Distance;
				boomable.rigidbody2D.AddForce(Dimension * pushForce);
				wasBoomUsed = true;
			}
	   	}
	   	
		if(wasBoomUsed)
		{
			StartCD();
		}
	}
	
	public static void StartCD()
	{
		timeSinceSpellCast = 0;
		isOnCD = true;
	}
	
}
