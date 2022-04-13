using UnityEngine;
using System.Collections;

public class gotonamedPosition : MonoBehaviour {

	//public Transform patrolPoint;
	public GameObject movingObject;
	public float moveSpeed;
	public float threshold = 0.1f;
	//Transform patrolPoint;

	void Update () {
	//	MoveToObject ();

//		 movingObject.transform.position = Vector3.MoveTowards (movingObject.transform.position, patrolPoint.position, moveSpeed * Time.deltaTime);
//
//		if ((movingObject.transform.position == patrolPoint.position) || (Vector3.Distance (movingObject.transform.position, patrolPoint.position) < threshold)) 
//			gameObject.SetActive (false);
	}

	public void MoveToObject (Transform patrolPoint)
	{
		
		movingObject.transform.position = Vector3.MoveTowards (movingObject.transform.position, patrolPoint.position, moveSpeed * Time.deltaTime);
		
		if ((movingObject.transform.position == patrolPoint.position) || (Vector3.Distance (movingObject.transform.position, patrolPoint.position) < threshold)) 
			gameObject.SetActive (false);
		
		
	}






}
