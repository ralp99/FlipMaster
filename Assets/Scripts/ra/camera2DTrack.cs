using UnityEngine;
using System.Collections;

public class camera2DTrack : MonoBehaviour {


	public GameObject followObject;
	public float threshold;
	public float increment;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {




		if (followObject.transform.position.y >= gameObject.transform.position.y + threshold)
		{

			gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y+increment), gameObject.transform.position.z);

		}
	
	}
}
