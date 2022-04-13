using UnityEngine;
using System.Collections;

public class sequenceEvents : MonoBehaviour {

	public GameObject[] events;
	int sequenceEvent;


	void OnEnable () {
	
		if (sequenceEvent >= events.Length)
			sequenceEvent = 0;

		events [sequenceEvent].SetActive (true);
		sequenceEvent++;
		gameObject.SetActive (false);

		
	}

}
		



