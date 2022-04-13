using UnityEngine;
using System.Collections;

public class onCollision3D : MonoBehaviour {


	public string collideWithName = "";
	public bool inheritX;
	public bool inheritY;
	public bool inheritZ;
	public GameObject[] showObject;
	public GameObject[] hideObject;
	public bool checkAbove;
	public bool checkBelow;
	public GameObject originObject;
	bool proceed;

	public GameObject[] showObjectOnLost;
	public GameObject[] hideObjectOnLost;


	float previousPosX;
	float previousPosY;
	float previousPosZ;

	float usePosX;
	float usePosY;
	float usePosZ;



	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == collideWithName) {





			if (checkAbove)
			{
				if (transform.position.y >= col.gameObject.transform.position.y) proceed = true;
			}

			if (checkBelow)

			{
				if (transform.position.y <= col.gameObject.transform.position.y) proceed = true;
			}

			if ((!checkAbove) && (!checkBelow)) proceed = true;


			if (proceed) 

			{

				previousPosX = col.gameObject.transform.position.x;
				previousPosY = col.gameObject.transform.position.y;
				previousPosZ = col.gameObject.transform.position.z;

				foreach (GameObject picked in showObject) 
				{
					picked.SetActive (true);
					MoveToObject(col.gameObject);
				}		

				foreach (GameObject picked in hideObject) 
				{
					picked.SetActive (false);
					MoveToObject(col.gameObject);
				}		

				proceed = false;
			}
		}

	}

	void OnCollisionExit(Collision col)
	{
			foreach (GameObject picked in hideObjectOnLost) 
		{
			picked.SetActive (false);
		}

		foreach (GameObject picked in showObjectOnLost) 
		{
			picked.SetActive (true);
		}

	}

	void MoveToObject (GameObject patrol)

	{

		 float moveSpeed = 111f;
		 float threshold = 0.1f;

		if (originObject != null) {
			float newposx = originObject.transform.position.x;
			float newposy = originObject.transform.position.y;
			float newposz = originObject.transform.position.z;


			if (!inheritX)
				usePosX = newposx;
			else
				usePosX = previousPosX;

			if (!inheritY)
				usePosY = newposy;
			else
				usePosY = previousPosY;

			if (!inheritZ)
				usePosZ = newposz;
			else
				usePosZ = previousPosZ;

			originObject.transform.position = Vector3.MoveTowards (originObject.transform.position, new Vector3 (usePosX, usePosY, usePosZ), moveSpeed * Time.deltaTime);
		}
	}

}
