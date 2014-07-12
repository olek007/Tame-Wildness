using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	private bool wasDragUsed = false;


	void Start()
	{
		Radar.usables.Add(gameObject);
	}
	
	void Push()
	{
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject.transform.position = point;
		Screen.showCursor = false;
		
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
