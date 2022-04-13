using UnityEngine;
using System.Collections;

public class distanceComparenew : MonoBehaviour {

	public GameObject objectA;
	public GameObject objectB;
	//public string taggedB = "";
	GameObject[] taggedObject;
	public GameObject[] enableInRange;
	public MonoBehaviour[] enableMonoInRange;
	public GameObject[] disableInRange;
	public GameObject[] enableOutOfRange;
	public MonoBehaviour[] enableMonoOutOfRange;
	public GameObject[] disableOutOfRange;
	public float range;
	float distanceDifferenceObjects;
	float distanceDifferenceTagged;

	bool withinRange;

	//col.gameObject.tag == collideWithTag
	
	void Update () {

//		GameObject useB = null;
//		if (taggedB != "") 
//			{
//			taggedObject = GameObject.FindGameObjectsWithTag(taggedB);
//
//			//taggedObject = GameObject.FindGameObjectsWithTag (taggedB);
//		//	useB = GameObject.FindGameObjectsWithTag (taggedB);
//
//			} 
////		else useB = objectB;
//		 useB = objectB;

		Transform distanceA = objectA.transform;
		Transform distanceB = objectB.transform;



//		if (objectB != null) Transform distanceB = taggedObject.transform;
//		else 		Transform distanceB = objectB.transform;


		distanceDifferenceObjects = Vector3.Distance (distanceA.position, distanceB.position);
		//if (taggedB != "") distanceDifferenceTagged = Vector3.Distance (distanceA.position, distanceB.position);



		if (distanceDifferenceObjects > range)
			withinRange = false;
		if (distanceDifferenceObjects <= range)
			withinRange = true;


		if (withinRange) {
			if (enableInRange != null)

				foreach (GameObject picked in enableInRange) 
			{
				picked.SetActive (true);
			}

			foreach (MonoBehaviour picked in enableMonoInRange)
			{
				picked.enabled = true;
			}

				 
			if (disableInRange != null)

				foreach (GameObject picked in disableInRange) 
			{
				picked.SetActive (false);
			}

		} 

		else 
		
		{


			if (enableOutOfRange != null)
				
				foreach (GameObject picked in enableOutOfRange) 
			{
				picked.SetActive (true);
			}

			foreach (MonoBehaviour picked in enableMonoOutOfRange)
			{
				picked.enabled = true;
			}


			
			if (disableOutOfRange != null)
				
				foreach (GameObject picked in disableOutOfRange) 
			{
				picked.SetActive (false);
			}



		}







	}


}
