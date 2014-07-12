using UnityEngine;
using System.Collections;

public class WizardNPC : MonoBehaviour {

	public Sprite[] dialog;
	private bool keyPressed = false;
	private bool isTalking = false;
	private int dialogState = 0;
	
	private string[] dialogi;

	// Use this for initialization
	void Start () {
	
		dialogi[0] = "Ach!  Nie spodziewalem sie, ze akurat Ty obudzisz w sobie tą moc.";
		dialogi[1] = "Ach!  Nie spodziewalem sie, ze akurat Ty obudzisz w sobie tą moc.";
		dialogi[2] = "Ach!  Nie spodziewalem sie, ze akurat Ty obudzisz w sobie tą moc.";
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isTalking)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				dialogState++;
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
			}	
		}
	}

}
