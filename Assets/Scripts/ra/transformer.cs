using UnityEngine;
using System.Collections;

public class transformer : MonoBehaviour {
	//public class transformer_WIP3 : MonoBehaviour {

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

	void Start () {

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


	void disableCurrentObject()
	{
		clearForQuit = false;
		gameObject.SetActive (false);
	}



	void Update()

	{

		newPosition = picked.transform.position;
		newScale = picked.transform.localScale;

		// ----------------------

		if (activepositionX != 0) {

			positionCurrentX = picked.transform.position.x;

			{

				newPosition.x = activepositionX;

				picked.transform.position = Reorient (picked.transform.position,newPosition);

				if (picked.transform.position == newPosition)
				{
					if (consecutivePX) activepositionX = activepositionX + positionX;
					//disableCurrentObject();
					clearForQuit=true;

				}

			}
		}

		if (posXToZero) {
			newPosition.x = 0f;
			picked.transform.position = newPosition;
		}

		// ----------------------



		if (activepositionY != 0) {
			
			positionCurrentY = picked.transform.position.y;
			
			{
				
				newPosition.y = activepositionY;
				
				picked.transform.position = Reorient (picked.transform.position,newPosition);

				if (picked.transform.position == newPosition)
				{
					if (consecutivePY) activepositionY = activepositionY + positionY;
					clearForQuit=true;

				}
				
			}
		}
		
		if (posYToZero) {
			newPosition.y = 0f;
			picked.transform.position = newPosition;
		}
		
		// ----------------------


		if (activepositionZ != 0) {
			
			positionCurrentZ = picked.transform.position.z;
			
			{
				
				newPosition.z = activepositionZ;
				
				picked.transform.position = Reorient (picked.transform.position,newPosition);

				if (picked.transform.position == newPosition)
				{
					if (consecutivePZ) activepositionZ = activepositionZ + positionZ;
					clearForQuit=true;

				}
				
			}
		}
		
		if (posZToZero) {
			newPosition.z = 0f;
			picked.transform.position = newPosition;
		}
		
		// ----------------------





//
//		if (activepositionY != 0) {
//			
//			positionCurrentY = picked.transform.position.y;
//			
//			{
//				if (consecutivePY) {
//					newPosition.y = positionCurrentY + activepositionY;
//					// ADD CONSECUTIVE POSITION
//					
//					positionCurrentY = (positionCurrentY + activepositionY);
//				} 
//				
//				else 
//					
//				{
//					// REPLACE POSITION
//					newPosition.y = activepositionY;
//				}
//				
//				picked.transform.position = newPosition;
//			}
//		}
//		
//		if (posYToZero) {
//			newPosition.y = 0f;
//			picked.transform.position = newPosition;
//		}
//
//
//		// ----------------------
//		
//		if (activepositionZ != 0) {
//			
//			positionCurrentZ = picked.transform.position.z;
//			
//			{
//				if (consecutivePZ) {
//					newPosition.z = positionCurrentZ + activepositionZ;
//					// ADD CONSECUTIVE POSITION
//					
//					positionCurrentZ = (positionCurrentZ + activepositionZ);
//				} 
//				
//				else 
//					
//				{
//					// REPLACE POSITION
//					newPosition.z = positionZ;
//				}
//				
//				picked.transform.position = newPosition;
//			}
//		}
//		
//		if (posZToZero) {
//			newPosition.z = 0f;
//			picked.transform.position = newPosition;
//		}
//







		// ----------------------


		// ----------------------


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











		//gameObject.SetActive (false);
		if (clearForQuit)
			disableCurrentObject ();
	}


}
