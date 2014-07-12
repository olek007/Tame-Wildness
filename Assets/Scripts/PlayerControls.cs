using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public Sprite[] animation;
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
	public static float pushForce = 1000000;
	public static bool canWalk;
	int animationFrame;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 1.0f;
		animationFrame = 0;
		pushForceLvl = 1;
		isOnCD = false;
		anim = GetComponent<Animator> ();
		canWalk = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(canWalk)	
		{	
			if (Input.GetKey (KeyCode.LeftArrow))
			{
				timer-=Time.deltaTime;
				if(timer>0.75f)
				{
					animationFrame=1;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.5f)
				{
					animationFrame=2;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.25f)
				{
					animationFrame=3;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.0f)
				{
					animationFrame=4;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}
				if(timer<=0.0f)
				{
					timer=1.0f;
				}
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y );

			} 
			else if (Input.GetKey (KeyCode.RightArrow)) 
			{
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
				timer-=Time.deltaTime;
				if(timer>0.75f)
				{
					animationFrame=4;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.5f)
				{
					animationFrame=3;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.25f)
				{
					animationFrame=2;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}else if(timer>0.0f)
				{
					animationFrame=1;
					gameObject.GetComponent<SpriteRenderer>().sprite = animation[animationFrame];
				}
				if(timer<=0.0f)
				{
					timer=1.0f;
				}
			} else
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = animation[0];
				timer=1.0f;
			}
	
			if (Input.GetButtonDown("Jump"))
		    {
				if(canJump)
				{
					player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000 * jumpHeight);
				}
			}
	
			if((Input.GetButtonDown("Fire1")) && (pushForceLvl == 1) && (!isOnCD))
			{
				Boom();
			}
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
