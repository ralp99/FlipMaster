using UnityEngine;
using System.Collections;

public class gotoPositionRange : MonoBehaviour {

	public Transform patrolPoint;
	public GameObject movingObject;
	public float moveSpeed;
	public float threshold = 0.1f;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

	public float offsetX;
	public float offsetY;
	public float offsetZ;

	float newPosX;
	float newPosY;
	float newPosZ;


//	void Update ()
//	{
//		GoToObject ();
//	}


//	void Start () {
//	
//		if (movingObject == null)
//			movingObject = gameObject;
//			//	GoToObject ();
//
//	}

	//public void GoToObject () {
	public void OnEnable () {
		if (movingObject == null)
			movingObject = gameObject;
		

		float randomOffsetX = Random.Range (minX, maxX);
		float randomOffsetY = Random.Range (minY, maxY);
		float randomOffsetZ = Random.Range (minZ, maxZ);
//		print ("offsetX "+offsetX);
//
//
//		print ("patrol X "+patrolPoint.position.x);
//		print ("patrol Y "+patrolPoint.position.y);
//		print ("patrol Z "+patrolPoint.position.z);

		
		
		//[Random.Range (0, events.Count)];
		newPosX = patrolPoint.position.x + randomOffsetX + offsetX ;
		newPosY = patrolPoint.position.y + randomOffsetY + offsetY;
		newPosZ = patrolPoint.position.z + randomOffsetZ + offsetZ;

		//print ("newPosX "+newPosX);

//		print ("patrol y "+patrolPoint.position.y);
//		print ("new pos y "+newPosY);


		Vector3 newPosition = new Vector3 (newPosX, newPosY, newPosZ);

//		print ("new dif "+newPosition.y );

		movingObject.transform.position = Vector3.MoveTowards (movingObject.transform.position, newPosition, moveSpeed * Time.deltaTime);

			//	if ((movingObject.transform.position == patrolPoint.position) || (Vector3.Distance (movingObject.transform.position, patrolPoint.position) < threshold)) 
			if ((movingObject.transform.position == newPosition) || (Vector3.Distance (movingObject.transform.position, newPosition) < threshold)) 

		
		GetComponent<gotoPositionRange> ().enabled = false;

		
		
		

	}












//	public void MoveToObject (Transform patrolPoint)
//
//	{
//		
//		movingObject.transform.position = Vector3.MoveTowards (movingObject.transform.position, patrolPoint.position, moveSpeed * Time.deltaTime);
//		
//		if ((movingObject.transform.position == patrolPoint.position) || (Vector3.Distance (movingObject.transform.position, patrolPoint.position) < threshold)) 
//		//	gameObject.SetActive (false);
//		
//		
//	}






}