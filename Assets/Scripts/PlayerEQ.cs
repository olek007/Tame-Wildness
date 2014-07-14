using UnityEngine;
using System.Collections;

public class PlayerEQ : MonoBehaviour {

	public static bool alive = true;
	int buttonWidth = 50;
	int buttonHeigh = 50;
	int margin = 20;
	bool[] buttons;

	void Start()
	{
		buttons = new bool[6];
	}

	void OnGUI()
	{
		if (alive) 
		{
			buttons [0] = GUI.Button (new Rect (Screen.width / 3, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
			buttons[1] = GUI.Button(new Rect(Screen.width / 3 + buttonWidth + margin, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
			buttons[2] = GUI.Button(new Rect(Screen.width / 3 + (buttonWidth + margin) * 2, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
			buttons[3] = GUI.Button(new Rect(Screen.width / 3 + (buttonWidth + margin) * 3, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
			buttons[4] = GUI.Button(new Rect(Screen.width / 3 + (buttonWidth + margin) * 4, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
			buttons[5] = GUI.Button(new Rect(Screen.width / 3 + (buttonWidth + margin) * 5, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), "empty");
		} else 
		{
			GUI.Label(new Rect(Screen.width / 2 - 800, Screen.height / 2 - 300, 800, 300), "GAME OVER");
		}
	}

}
