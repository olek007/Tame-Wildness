﻿using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{

	public  Texture ikona;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.layer == 8)
		{
			PlayerEQ.AddItem(gameObject, gameObject.GetComponent<Shovel>());
		}
	}
}