using UnityEngine;
using System.Collections;

public class sendValueToObject2 : MonoBehaviour {

	public GameObject manager;
	public string method;
//	public int value;
	public string message;
	public string type;
    //public bool locateHere;
	public GameObject[] subject;
    public GameObject inheritLocation;
    public float offsetX, offsetY, offsetZ;
    Vector3 location;
    bool sendLocation;


    //string objectName;

    void OnEnable () {
        //		if (subject == null)
        //			manager.SendMessage (method, message);
        //		else 
        //		
        //		{

        if (inheritLocation != null)
        {
            float posX = inheritLocation.transform.position.x + offsetX;
            float posY = inheritLocation.transform.position.y + offsetY;
            float posZ = inheritLocation.transform.position.z + offsetZ;
            location = new Vector3(posX, posY, posZ);
            sendLocation = true;

        }
    //  if (sendLocation)  print("location = "+location);

			object[] parms = new object[5]{message, type, subject, sendLocation, location };
			//		manager.SendMessage (method, message, subject);
			manager.SendMessage (method, parms);
//		}
		
		gameObject.SetActive(false);
		
//	}

	// METHOD = ADVANCE
	// MESSAGE = NEXT
	}
}
