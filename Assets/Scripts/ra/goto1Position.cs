using UnityEngine;
using System.Collections;

public class goto1Position : MonoBehaviour {

	public Transform patrolPoint;
	public GameObject movingObject;
	public float moveSpeed;
	public float threshold = 0.1f;

	void Update () {


		 movingObject.transform.position = Vector3.MoveTowards (movingObject.transform.position, patrolPoint.position, moveSpeed * Time.deltaTime);

		if ((movingObject.transform.position == patrolPoint.position) || (Vector3.Distance (movingObject.transform.position, patrolPoint.position) < threshold)) 
			gameObject.SetActive (false);
	}



}
