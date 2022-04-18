using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class touchableButton : MonoBehaviour {

	public GameObject touchDown;
	public GameObject touchRelease;
	//	public GameObject touchStay;
	//	public GameObject touchExit;

	public UnityEvent touchDownEvent;
	public UnityEvent touchReleaseEvent;

	// SELECTED
	void OnTouchDown() 
	{
		if (touchDown)
        {
			touchDown.SetActive(true);
		}

		touchDownEvent.Invoke();

	}

	// DESELECTED
	void OnTouchUp() 
	{
		if (touchRelease != null)
		{
			touchRelease.SetActive(true);
		}

		touchReleaseEvent.Invoke();

	}

	void OnTouchStay() // TOUCH DRAG OFF AND BACK ON TO GO DOWN AGAIN
	{

	}

	void OnTouchExit() // DESELECTED
	{

	}





}
