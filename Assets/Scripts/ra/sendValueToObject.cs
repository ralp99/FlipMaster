using UnityEngine;
using System.Collections;

public class sendValueToObject : MonoBehaviour {

	public GameObject objectSend;
	public string method;
//	public int value;
	public string message;
	
	void OnEnable () {
		


		//lastHitByRay.SendMessage ("animateNoticed", false);
//		objectSend.SendMessage (method, value);
		objectSend.SendMessage (method, message);
		//	objectSend.SendMessage (message);

		
		gameObject.SetActive(false);
		
	}
}
