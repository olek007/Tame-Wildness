using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

<<<<<<< HEAD
	public GameObject Player;
	public Collider2D boomRange;	
=======
	public GameObject player;
	private bool wasDragUsed = false;
>>>>>>> origin/master

	void Boom()
	{
		/*
		Vector2 Dimension;
		Dimension.x = gameObject.transform.position.x - Player.transform.position.x;
		Dimension.y = gameObject.transform.position.y - Player.transform.position.y;
		float Distance;
		Distance = Vector2.Distance (gameObject.transform.position, Player.transform.position);
		Dimension.x /= Distance;
		Dimension.y /= Distance;
		gameObject.rigidbody2D.AddForce (Dimension);
		
		*/
		
		
	}
	
	void Push()
	{
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject.transform.position = point;
		Screen.showCursor = false;
		
	}
	
	void OnMouseDown()
	{	
		if((PlayerControls.pushForceLvl == 2) && (!PlayerControls.isOnCD))
		{
			Push();
			PlayerControls.StartCD();
		}
	}
	
	void OnMouseDrag()
	{
		if(PlayerControls.pushForceLvl == 3 && (!PlayerControls.isOnCD))
		{
<<<<<<< HEAD
			case 1:
			{
				Boom();
			}
			break;

			case 2:
			{
				Push();
			}
			break;
			
			case 3:
			{
				Drag();
			}
			break;

=======
			Drag();
			
>>>>>>> origin/master
		}
	}
	
	void OnMouseUp()
	{
		Screen.showCursor = true;
		
		if(wasDragUsed)      // żeby cooldown nie był liczony od rozpoczęcia przeciągania przedmiotu
		{
			PlayerControls.StartCD();
			wasDragUsed = false;
		}
		
	}
	
	
}
