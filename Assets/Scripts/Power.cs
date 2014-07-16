using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	private bool wasDragUsed = false;
	public Camera dragCamera;
	private bool dragging;
	private float startingMass;



	void Start()
	{
		Radar.usables.Add(gameObject);
		dragging = false;
		startingMass = gameObject.rigidbody2D.mass;
	}
	
	void FixedUpdate()
	{
		if (dragging)
		{
			Vector2 Dimension;
			Dimension.x = (dragCamera.ScreenToWorldPoint(Input.mousePosition).x) - (gameObject.transform.position.x);
			Dimension.y = (dragCamera.ScreenToWorldPoint(Input.mousePosition).y) - (gameObject.transform.position.y);
			float distance;
			distance = Vector2.Distance (gameObject.transform.position, dragCamera.ScreenToWorldPoint(Input.mousePosition));
			//Dimension.x /= distance;
			//Dimension.y /= distance;
			gameObject.rigidbody2D.AddForce(Dimension * 100);
			
			if(distance < 5.0f)
			{
				gameObject.rigidbody2D.velocity -= gameObject.rigidbody2D.velocity;
			}
		}
	
	}
	void Push()
	{
		Vector2 point = Camera.current.ScreenToWorldPoint(Input.mousePosition);
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
		
		if(PlayerControls.pushForceLvl == 3 && !PlayerControls.isOnCD)
		{
			gameObject.rigidbody2D.gravityScale = 0;
			gameObject.rigidbody2D.mass  = 1;
		}
		
	}
	
	void OnMouseDrag()
	{
		if(PlayerControls.pushForceLvl == 3 && !PlayerControls.isOnCD)
		{
			wasDragUsed = true;
			dragging = true;
		}
	}
	
	
	void OnMouseUp()
	{
		Screen.showCursor = true;
		if(wasDragUsed)      // żeby cooldown nie był liczony od rozpoczęcia przeciągania przedmiotu
		{
			PlayerControls.StartCD();
			wasDragUsed = false;
			gameObject.rigidbody2D.gravityScale = 1;
			dragging = false;
			gameObject.rigidbody2D.mass  = startingMass;
		}
		
	}
	
}
