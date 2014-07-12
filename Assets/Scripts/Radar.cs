using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	public static List<GameObject> usables = new List<GameObject>();
	public GameObject illumination;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			foreach(GameObject usable in usables)
			{
				if(usable.renderer.isVisible)
				{
					Instantiate(illumination,usable.transform.position, usable.transform.rotation);
				}
			}
		}
	
	}
}
