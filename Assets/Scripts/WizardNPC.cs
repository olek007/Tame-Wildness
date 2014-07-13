using UnityEngine;
using System.Collections;

public class WizardNPC : MonoBehaviour {

	public Transform player;
	private string[] dialogi = new string[5];
	private bool isTalking = false;
	private int dialogState = 0;
	private int dialogWidth = 300;
	private int dialogHeigh = 150;


	void Start ()
	{
		dialogi[0] = "Ach!  Nie spodziewalem sie, ze akurat Ty obudzisz w sobie tą moc.";
		dialogi[1] = "Jaką moc? O czym Ty mówisz?";
		dialogi[2] = "Nie mam zbyt wiele czasu. Nauczę Cię używać tej mocy, lecz nie licz na nic wielkiego.";
		dialogi[3] = "Powiesz mi co się stało?!";
		dialogi[4] = "Nie mamy czasu! Pospiesz się!";
	
	}
	
	void Update () 
	{
		if(isTalking)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				dialogState++;
			}
			
			if(dialogState>5)
			{
				EndDialog ();
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.layer == 8)
		{
			PlayerControls.canWalk = false;
			PlayerControls.pushForceLvl = 1;
			isTalking = true;
			dialogState = 1;
		}
	}
	
	void EndDialog()
	{
		PlayerControls.canWalk = true;
		isTalking = false;
	}
	
	void OnGUI()
	{
		if(isTalking)
		{
			GUI.color = Color.black;
			GUI.skin.label.fontSize = 24;
			switch (dialogState)
			{
				case 1:
				{
					GUI.Label(new Rect(Camera.current.WorldToScreenPoint(gameObject.transform.position).x-dialogWidth/2, Camera.current.WorldToScreenPoint(gameObject.transform.position).y - 150, dialogWidth, dialogHeigh), dialogi[0]);
				}
				break;
				
				case 2:
				{
					GUI.Label(new Rect(Camera.current.WorldToScreenPoint(player.position).x - dialogWidth / 2, Camera.current.WorldToScreenPoint(player.position).y, dialogWidth, dialogHeigh), dialogi[1]);
				}
				break;
				
				case 3:
				{
					GUI.Label(new Rect(Camera.current.WorldToScreenPoint(gameObject.transform.position).x - dialogWidth / 2, Camera.current.WorldToScreenPoint(gameObject.transform.position).y - 150, dialogWidth, dialogHeigh), dialogi[2]);
				}
				break;
				
				case 4:
				{
					GUI.Label(new Rect(Camera.current.WorldToScreenPoint(player.position).x - dialogWidth / 2, Camera.current.WorldToScreenPoint(player.position).y, dialogWidth, dialogHeigh), dialogi[3]);
				}
				break;
				
				case 5:
				{
					GUI.Label(new Rect(Camera.current.WorldToScreenPoint(gameObject.transform.position).x - dialogWidth / 2, Camera.current.WorldToScreenPoint(gameObject.transform.position).y - 150, dialogWidth, dialogHeigh), dialogi[4]);
				}
				break;
			}	
		}
	}

}
