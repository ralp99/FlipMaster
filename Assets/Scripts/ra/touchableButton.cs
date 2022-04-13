using UnityEngine;
using System.Collections;

public class touchableButton : MonoBehaviour {

	public GameObject touchDown;
	public GameObject touchRelease;
//	public GameObject touchStay;
//	public GameObject touchExit;

	// SELECTED
	void OnTouchDown() 
	{
		touchDown.SetActive (true);
	}

	// DESELECTED
	void OnTouchUp() 
	{
		if (touchRelease != null) touchRelease.SetActive (true);

	}

	void OnTouchStay() // TOUCH DRAG OFF AND BACK ON TO GO DOWN AGAIN
	{

	}

	void OnTouchExit() // DESELECTED
	{

	}





}
