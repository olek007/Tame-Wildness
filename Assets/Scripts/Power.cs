using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	public GameObject player;
	private bool wasDragUsed = false;

	void Push()
	{
		Vector2 Dimension;
		Dimension.x = transform.position.x - player.transform.position.x;
		Dimension.y = transform.position.y - player.transform.position.y;
		float Distance;
		Distance = Vector2.Distance (transform.position, player.transform.position);
		Dimension.x /= Distance;
		Dimension.y /= Distance;
		gameObject.rigidbody2D.AddForce(Dimension);
		
	}
	
	void Drag()
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
			Drag();
			
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
