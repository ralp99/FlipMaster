using UnityEngine;
using System.Collections;

public class platformSlide : MonoBehaviour {

//	public float min;
//	public float max;

	public float rightLimit;
	public float leftLimit;
	public float speed;
	int direction = 1;
	Vector3 movement;




	
	// Update is called once per frame
	void Update () {


		if (transform.position.x > rightLimit) {
			direction = -1;
		}
		else if (transform.position.x < leftLimit) {
			direction = 1;
		}
		movement = Vector3.right * direction * speed * Time.deltaTime; 
		transform.Translate(movement); 






//		float t2 = 0f;
//		while (t2 < 1f) {
//			t2 += Time.deltaTime / time; // sweeps from 0 to 1 in time seconds
//			//platform.transform.position = Vector3.Lerp (pointA, pointB, t); // set position proportional to t
//			LerpObject (platform, pointB, pointA, t2);
//			
//			
//			yield return 0; // leave the routine and return here in the next frame
//			returning  = false;
	}





//	
//	void LerpObject(GameObject platform, Vector3 pointA, Vector3 pointB, float t)
//	{
//		platform.transform.position = Vector3.Lerp (pointA, pointB, t); // set position proportional to t
//		
//	}
}
