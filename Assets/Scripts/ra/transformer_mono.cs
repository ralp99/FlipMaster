using UnityEngine;
using System.Collections;

public class transformer_mono : MonoBehaviour {
	//public class transformer_WIP3 : MonoBehaviour {

	public MonoBehaviour[] killMono;


	public GameObject picked;

	public float positionX;
	public bool consecutivePX;
	public float positionY;
	public bool consecutivePY;
	public float positionZ;
	public bool consecutivePZ;

	public float rotationX;
	public bool consecutiveRX;
	public float rotationY;
	public bool consecutiveRY;
	public float rotationZ;
	public bool consecutiveRZ;

	public float scaleX;
	public bool consecutiveSX;
	public float scaleY;
	public bool consecutiveSY;
	public float scaleZ;
	public bool consecutiveSZ;

	public bool posXToZero;
	public bool posYToZero;
	public bool posZToZero;

	public bool rotXToZero;
	public bool rotYToZero;
	public bool rotZToZero;

	public bool scaXToOne;
	public bool scaYToOne;
	public bool scaZToOne;
	public bool disableAfterOnce;
	public bool keepDoing;
	public float moveSpeed;


	float positionCurrentX;
	float positionCurrentY;
	float positionCurrentZ;

	float rotationCurrentX;
	float rotationCurrentY;
	float rotationCurrentZ;

	float scaleCurrentX;
	float scaleCurrentY;
	float scaleCurrentZ;

	Vector3 newPosition;
	Vector3 newRotation;
	Vector3 newScale;

	float activepositionX;
	float activepositionY;
	float activepositionZ;
	float activerotationX;
	float activerotationY;
	float activerotationZ;
	float activescaleX;
	float activescaleY;
	float activescaleZ;
	bool clearForQuit;






	void Awake () {

			
		activepositionX = positionX;
		activepositionY = positionY;
		activepositionZ = positionZ;
		
		activerotationX = rotationX;
		activerotationY = rotationY;
		activerotationZ = rotationZ;
		
		activescaleX = scaleX;
		activescaleY = scaleY;
		activescaleZ = scaleZ;

	
	}


	 Vector3 Reorient(Vector3 startValue, Vector3 gotoValue)
	
	{
		return Vector3.MoveTowards(startValue,gotoValue,moveSpeed*Time.deltaTime);
	}


//	void disableCurrentObject()
//	{
//		clearForQuit = false;
//		gameObject.SetActive (false);
//	}




//	void Reposition(float positionToChange, float positionCurrent, float positionInput, bool consecutive)
//	{
//	}


	void OnEnable()
	{
		DoTransform ();
	}

	void DoTransform()

	{
		//SET X, Y, Z current positions
		positionCurrentX = picked.transform.position.x;
		positionCurrentY = picked.transform.position.y;
		positionCurrentZ = picked.transform.position.z;


		if (positionX != 0) {

			if (consecutivePX) positionCurrentX = positionCurrentX + positionX;
			else positionCurrentX = positionX;

		}

		if (positionY != 0) {
			
			if (consecutivePY) positionCurrentY = positionCurrentY + positionY;
			else positionCurrentY = positionY;
			
		}

		if (positionZ != 0) {
			
			if (consecutivePZ) positionCurrentZ = positionCurrentZ + positionZ;
			else positionCurrentZ = positionZ;
			
		}


		if (posXToZero) positionCurrentX = 0;
		if (posYToZero) positionCurrentY = 0;
		if (posZToZero) positionCurrentZ = 0;
			
		picked.transform.position = new Vector3(positionCurrentX,positionCurrentY,positionCurrentZ);

//		if (killMono != null) foreach (MonoBehaviour pickedm in killMono) {
//
//				pickedm.enabled = false;
//		}
//		clearForQuit = true;

		// ----------------------------------------------------------------------------------------------------------------


		scaleCurrentX = picked.transform.localScale.x;
		scaleCurrentY = picked.transform.localScale.y;
		scaleCurrentZ = picked.transform.localScale.z;


		if (scaleX != 0) {
			
			if (consecutiveSX) scaleCurrentX = scaleCurrentX + scaleX;
			else scaleCurrentX = scaleX;
			
		}
		
		if (scaleY != 0) {
			
			if (consecutiveSY) scaleCurrentY = scaleCurrentY + scaleY;
			else scaleCurrentY = scaleY;
			
		}
		
		if (scaleZ != 0) {
			
			if (consecutiveSZ) scaleCurrentZ = scaleCurrentZ + scaleZ;
			else scaleCurrentZ = scaleZ;
			
		}
		
		
		if (scaXToOne) scaleCurrentX = 1;
		if (scaYToOne) scaleCurrentY = 1;
		if (scaZToOne) scaleCurrentZ = 1;

		picked.transform.localScale = new Vector3(scaleCurrentX,scaleCurrentY,scaleCurrentZ);



















		// ----------------------------------------------------------------------------------------------------------------

		if (activerotationX != 0) {

			{
				if (!consecutiveRX) {
					// REPLACE ROTATION
					// switched from position, script is working in opposite booleans sort of
				//	picked.transform.rotation = Quaternion.Euler(rotationX,picked.transform.rotation.y,picked.transform.rotation.z);
					picked.transform.rotation = Quaternion.Euler(activerotationX,0,0);

				} 
				
				else 
					
				{
					// CONSECUTIVE ROTATION

					//picked.transform.Rotate (rotationX, picked.transform.rotation.y,picked.transform.rotation.z);
					//THIS SHOULD BE ADDING ZERO TO OTHER OBJECTS
					picked.transform.Rotate (activerotationX, 0,0);

				}
				
			}
		}
		
		if (rotXToZero) {

		//	picked.transform.rotation = Quaternion.Euler(0,picked.transform.rotation.y,picked.transform.rotation.z);
				picked.transform.rotation = Quaternion.Euler(0,picked.transform.rotation.y,picked.transform.rotation.z);


		}
		
		// ----------------------


		
		if (activerotationY != 0) {
			
			{
				if (!consecutiveRY) {
					// REPLACE ROTATION
					// switched from position, script is working in opposite booleans sort of
					picked.transform.rotation = Quaternion.Euler(picked.transform.rotation.x,activerotationY,picked.transform.rotation.z);
				} 
				
				else 
					
				{
					// CONSECUTIVE ROTATION
					//THIS SHOULD BE ADDING ZERO TO OTHER OBJECTS
					//picked.transform.Rotate (rotationX, picked.transform.rotation.y,picked.transform.rotation.z);
					picked.transform.Rotate (0,activerotationY,0);
					
				}
				
			}
		}
		
		if (rotYToZero) {
			
			picked.transform.rotation = Quaternion.Euler(picked.transform.rotation.x,0,picked.transform.rotation.z);
			
			
		}
		
		// ----------------------



		if (killMono != null) foreach (MonoBehaviour pickedm in killMono) {
			
			pickedm.enabled = false;
		}

		clearForQuit = true;







	//	//gameObject.SetActive (false);
//		if (clearForQuit) {
//		//	//disableCurrentObject ();
//			
//			if (disableAfterOnce) gameObject.SetActive (false);
//			if (keepDoing)
//			{
//				//make it continue
//			}

		//	//GetComponent<transformer_mono> ().enabled = false; //NOT USRE IF THIS SHOULD BE DISABLED


		}

	void Update ()
	{

	if (clearForQuit) {
			//	//disableCurrentObject ();
		
			if (disableAfterOnce)
				gameObject.SetActive (false);
			if (keepDoing) {
				//make it continue
				DoTransform();
			}
		}



	}


}
