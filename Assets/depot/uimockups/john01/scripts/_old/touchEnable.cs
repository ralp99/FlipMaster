using UnityEngine;
using System.Collections;

public class touchEnable : MonoBehaviour {
	public GameObject[] usedObject;


	
	void Update () {
	
		if ((Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Moved)|| (Input.GetMouseButtonDown(0)))
		{
				foreach (GameObject picked in usedObject) 
				{
					picked.SetActive (true);
				}
		}
	}
}
