using UnityEngine;
using System.Collections;

public class alphaFade : MonoBehaviour {

	public GameObject[] fadeObjects; 
	public float duration = 1.0f;
	//public bool hide;
	public float goalAlpha;
	float startalpha;
	bool closeObject;


	void OnEnable ()
	{
		closeObject = false;
	}


	void Update () {


		foreach (GameObject picked in fadeObjects) 

		{
			StartCoroutine(FadeTo(picked,goalAlpha, duration));
			StartCoroutine(Wait());

		}
		if (closeObject) gameObject.SetActive (false);

	}



	IEnumerator FadeTo(GameObject currentObject, float aValue, float aTime)
	{
	
		float colorr = currentObject.GetComponent<Renderer>().material.color.r;
		float colorg = currentObject.GetComponent<Renderer>().material.color.g;
		float colorb = currentObject.GetComponent<Renderer>().material.color.b;
		float startalpha = currentObject.GetComponent<Renderer>().material.color.a;

			for (float t = 0f; t < 2.0f; t+= Time.deltaTime / aTime) 

		{
			Color newColor = new Color(colorr, colorg, colorb, Mathf.Lerp (startalpha, aValue, t));
			currentObject.GetComponent<Renderer>().material.color = newColor;

			yield return null;

		}

	}


	private IEnumerator Wait ()
	{
		yield return new WaitForSeconds(duration);
		closeObject = true;
	}





}
