using UnityEngine;
using System.Collections;

public class WizardNPC : MonoBehaviour {

	private string[] dialogi = new string[5];
	private bool isTalking = false;
	private int dialogState = 0;



	void Start () {
	
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
			switch (dialogState)
			{
				case 1:
				{
					GUI.Label(new Rect(250,250,100,100),dialogi[0]);
					Debug.Log ("1");
				}
				break;
				
				case 2:
				{
					GUI.Label(new Rect(250,250,100,100),dialogi[1]);
				}
				break;
				
				case 3:
				{
					GUI.Label(new Rect(250,250,100,100),dialogi[2]);
				}
				break;
				
				case 4:
				{
					GUI.Label(new Rect(250,250,100,100),dialogi[3]);
				}
				break;
				
				case 5:
				{
					GUI.Label(new Rect(250,250,100,100),dialogi[4]);
				}
				break;
			}	
		}
	}

}
