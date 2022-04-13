using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class onCollision3D3 : MonoBehaviour {


	[System.Serializable]
	
	public class instanceList
		
	{
	public string note = "";
	public string collideWithName = "";
	public bool inheritX;
	public bool inheritY;
	public bool inheritZ;
	public string sendMessageToCollided = "";
		public string collidedMethod = "";

	public GameObject[] showObject;
	public GameObject[] hideObject;
	public bool checkAbove;
	public bool checkBelow;
	public GameObject originObject;
	[HideInInspector]public bool proceed;

	public GameObject[] showObjectOnLost;
	public GameObject[] hideObjectOnLost;


		[HideInInspector]public float previousPosX;
		[HideInInspector]public float previousPosY;
		[HideInInspector]public float previousPosZ;

		[HideInInspector]public float usePosX;
		[HideInInspector]public float usePosY;
		[HideInInspector]public float usePosZ;
	}

	public List<instanceList> InstanceList;


	void OnCollisionEnter(Collision col)
	{
		foreach (instanceList inList in InstanceList) {
			//if (col.gameObject.tag == collideWithName) {

			//foreach (instanceList inList in InstanceList)
			//foreach (string checkingString in collideWithName) {


			if (col.gameObject.tag == inList.collideWithName) {

				if (inList.checkAbove) {
					if (transform.position.y >= col.gameObject.transform.position.y)
						inList.proceed = true;
				}

				if (inList.checkBelow) {
					if (transform.position.y <= col.gameObject.transform.position.y)
						inList.proceed = true;
				}

				if ((!inList.checkAbove) && (!inList.checkBelow))
					inList.proceed = true;


				if (inList.proceed) {

					inList.previousPosX = col.gameObject.transform.position.x;
					inList.previousPosY = col.gameObject.transform.position.y;
					inList.previousPosZ = col.gameObject.transform.position.z;

					foreach (GameObject picked in inList.showObject) {
						picked.SetActive (true);
						MoveToObject (col.gameObject);
					}		

					foreach (GameObject picked in inList.hideObject) {
						picked.SetActive (false);
						MoveToObject (col.gameObject);
					}		


					col.gameObject.GetComponent<CoinGotHit>().parentCoin.SendMessage

						(inList.collidedMethod,inList.sendMessageToCollided, SendMessageOptions.DontRequireReceiver);  


					inList.proceed = false;
				}

			} // END IF
			//} // END CHECKING TAG

		}
	}







	void OnCollisionExit(Collision col)
	{

		foreach (instanceList inList in InstanceList) {



			foreach (GameObject picked in inList.hideObjectOnLost) {
				picked.SetActive (false);
			}

			foreach (GameObject picked in inList.showObjectOnLost) {
				picked.SetActive (true);
			}

		}
	}



	void MoveToObject (GameObject patrol)

	{
		foreach (instanceList inList in InstanceList) {

		 float moveSpeed = 111f;
		 float threshold = 0.1f;

			if (inList.originObject != null) {
				float newposx = inList.originObject.transform.position.x;
				float newposy = inList.originObject.transform.position.y;
				float newposz = inList.originObject.transform.position.z;


				if (!inList.inheritX)
					inList.usePosX = newposx;
			else
					inList.usePosX = inList.previousPosX;

				if (!inList.inheritY)
					inList.usePosY = newposy;
			else
					inList.usePosY = inList.previousPosY;

				if (!inList.inheritZ)
					inList.usePosZ = newposz;
			else
					inList.usePosZ = inList.previousPosZ;

				inList.originObject.transform.position = Vector3.MoveTowards (inList.originObject.transform.position, 
				                                                              new Vector3 (inList.usePosX, inList.usePosY, inList.usePosZ), moveSpeed * Time.deltaTime);
		}
	}
}
}
