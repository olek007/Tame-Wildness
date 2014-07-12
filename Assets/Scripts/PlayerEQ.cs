using UnityEngine;
using System.Collections;

public class PlayerEQ : MonoBehaviour {
	
	int buttonWidth = 50;
	int buttonHeigh = 50;
	int margin = 20;
	bool aaaaaaa;
	bool[] buttons;

	void OnGUI()
	{
		aaaaaaa = GUI.Button (new Rect (Screen.width/3, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
		//buttons[1]=GUI.Button (new Rect (Screen.width/3+buttonWidth+margin, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
		//buttons[2]=GUI.Button (new Rect (Screen.width/3+(buttonWidth+margin)*2, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
		//buttons[3]=GUI.Button (new Rect (Screen.width/3+(buttonWidth+margin)*3, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
		//buttons[4]=GUI.Button (new Rect (Screen.width/3+(buttonWidth+margin)*4, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
		//buttons[5]=GUI.Button (new Rect (Screen.width/3+(buttonWidth+margin)*5, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "lalala");
	}
	void Update()
	{
		Debug.Log (aaaaaaa);
	}
}
