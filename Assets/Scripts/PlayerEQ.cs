using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerEQ : MonoBehaviour {

	public static bool alive = true;
	int buttonWidth = 50;
	int buttonHeigh = 50;
	int margin = 20;
	public static bool[] buttons;
	public static List<GameObject> items = new List<GameObject>();
	public bool digable;

	void Start()
	{
		digable = false;
		buttons = new bool[1];
	}




	void OnGUI()
	{
		
		if (alive) 
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (i >= buttons.Length)
				{
					Array.Resize<bool>(ref buttons, i + 1);
				}
				buttons[i] = GUI.Button(new Rect(Screen.width / 3 + (buttonWidth + margin) * i, Screen.height - buttonHeigh - margin, buttonWidth, buttonHeigh), items[i].GetComponent<Item>().ikona);
			}
		} 
		else 
		{
			GUI.Label(new Rect(Screen.width / 2 - 800, Screen.height / 2 - 300, 800, 300), "GAME OVER");
		}

		if (buttons[0] && items[0] && digable)
		{
			items[0].GetComponent<Shovel>().Dig();
		}
		
	}

	public static void AddItem (GameObject item)
	{
		items.Add(item);
	}
}
