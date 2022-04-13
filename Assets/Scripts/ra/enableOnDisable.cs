using UnityEngine;
using System.Collections;

public class enableOnDisable : MonoBehaviour {

	public GameObject[] objectsClosing;
	public bool show = true;


	void OnDisable () {

		foreach (GameObject picked in objectsClosing) 
		{
			if (picked != null)
				picked.SetActive (show);
		}

	}
}