using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class distanceCompareTagged : MonoBehaviour {

	public List<GameObject> taggedObjects = new List<GameObject>();

	public GameObject objectA;
	public GameObject objectB;
	public string tagged = "";
	GameObject[] taggedObjectsInScene;
	//public GameObject[] exclude;
	public List<GameObject> exclude = new List<GameObject>();

	public GameObject[] enableInRange;
	public MonoBehaviour[] enableMonoInRange;
	public GameObject[] disableInRange;
	public GameObject[] enableOutOfRange;
	public MonoBehaviour[] enableMonoOutOfRange;
	public GameObject[] disableOutOfRange;
	public bool disableThisAfterOnce;
	public float range;
	public bool checkX;
	public bool checkY;
	public bool checkZ;
	//bool movedOnce;
	bool counting;


	float distanceDifferenceObjects;
	float distanceDifferenceTagged;

	float distanceDifferenceObjectsX;
	float distanceDifferenceObjectsY;
	float distanceDifferenceObjectsZ;





	bool withinRange;
	GameObject[] taggedSet;
	//col.gameObject.tag == collideWithTag


	void Start()

	{

		{
			taggedObjectsInScene = GameObject.FindGameObjectsWithTag (tagged);
			
			foreach (GameObject picked in taggedObjectsInScene) 
			{
				if (!exclude.Contains(picked))
				taggedObjects.Add(picked);
			}
			
		}

	}


	bool RangeCheck (float distanceCheck, bool checkedAlready)
	{

		if (distanceCheck > range)
			return false;
		
		if (distanceCheck <= range) 
		{
			if (!checkedAlready)
				return true;
		}

		return true;
	}

	void CloseThis()
	{
		GetComponent<distanceCompareTagged> ().enabled = false;
	}



	private IEnumerator TimedSelect ()
	{
		counting = true;
		float threshold = .2f;
		yield return new WaitForSeconds(threshold);
		counting = false;
		CloseThis();

	}




	
	void Update () {

		if (!counting) StartCoroutine(TimedSelect());

		int countSession = 0;
		Transform distanceA = objectA.transform;

		float distanceAX = objectA.transform.position.x;
		float distanceAY = objectA.transform.position.y;
		float distanceAZ = objectA.transform.position.z;

		foreach (GameObject pickedA in taggedObjects) {


			countSession++;
			float distanceBX = pickedA.transform.position.x;
			float distanceBY = pickedA.transform.position.y;
			float distanceBZ = pickedA.transform.position.z;
			bool alreadyFalse = false;

			distanceDifferenceObjectsX = Mathf.Abs(distanceBX - distanceAX);
			distanceDifferenceObjectsY = Mathf.Abs(distanceBY - distanceAY);
			distanceDifferenceObjectsZ = Mathf.Abs(distanceBZ - distanceAZ);


			if (checkX) withinRange = RangeCheck(distanceDifferenceObjectsX,alreadyFalse);
			if (checkY) withinRange = RangeCheck(distanceDifferenceObjectsY,alreadyFalse);
			if (checkZ) withinRange = RangeCheck(distanceDifferenceObjectsZ,alreadyFalse);


		if (withinRange) {
			if (enableInRange != null)

				foreach (GameObject picked in enableInRange) {
					picked.SetActive (true);
				}

			foreach (MonoBehaviour picked in enableMonoInRange) {
				picked.enabled = true;
			}

				 
			if (disableInRange != null)

				foreach (GameObject picked in disableInRange) {
					picked.SetActive (false);
				}
				//movedOnce = true;

		} else {

				// list is taggedObjects

			//	if taggedObjects.Count 
			//	if (events.Count == 0) emptyCheck=true;
				//if (taggedObjects.Count == taggedObjects.Count - 1) print ("last item in list");
			//	print ("gameObject "+name+" countsession "+countSession);
			//	print ("gameObject "+name+" tagged items count "+taggedObjects.Count);
					


//				if (countSession == taggedObjects.Count)
//				{
//
//				if (movedOnce) 
//				{
//					movedOnce = false;
//					if (disableThisAfterOnce)
//					{
//						print ("GAMEOBJECT "+name+" CLOSING mono");
//
//					//	CloseThis();
//				
//					}
//				}
//				}





				if (enableOutOfRange != null)
				
				foreach (GameObject picked in enableOutOfRange) {
					picked.SetActive (true);
				}

			foreach (MonoBehaviour picked in enableMonoOutOfRange) {
				picked.enabled = true;
			}

			if (disableOutOfRange != null)
				
				foreach (GameObject picked in disableOutOfRange) {
					picked.SetActive (false);
				}

		}
		////// END FOREACH
 	}
}


}
