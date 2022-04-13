using UnityEngine;
using System.Collections;

public class transformer_mono3 : MonoBehaviour {
    // NOTES - To scale up consecutively, multiply by 1.01 or anything above 1. To scale down consecutively, 
    // multiply by 0.99 or thereabouts, otherwise it will go too fast to notice
    // NEW VERSION HERE--

    //public class transformer_WIP3 : MonoBehaviour {

    public enum Index { Empty, IndexA, IndexB, IndexC, IndexD, IndexE, IndexF, IndexG, IndexH, IndexI, IndexJ, IndexK }

    public MonoBehaviour[] killMono;


	public GameObject picked;
    public GameObject reference;
    public Index index;
    public bool flip;
    public bool world;

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
    public bool uniformScaleX;

    public bool posXToZero;
	public bool posYToZero;
	public bool posZToZero;

	public bool rotXToZero;
	public bool rotYToZero;
	public bool rotZToZero;

	public bool scaXToOne;
	public bool scaYToOne;
	public bool scaZToOne;

	public bool EnforceMinScales;
	public float minimumScaX;
	public float minimumScaY;
	public float minimumScaZ;

	public bool EnforceMaxScales;
	public float maximumScaX;
	public float maximumScaY;
	public float maximumScaZ;
    public bool ignoreZeroes;

  //  bool disableMonoAfterLimit, disableThisObjectAfterLimit;
    //  public bool disableMonoAfterLimit, disableThisObjectAfterLimit;
    public bool disableAfterLimit;

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

    // -----------------------

    float useRefFloat;
    bool flippedOnce;
    bool startedNegative;
    bool keepNegative;
    float useDirection = 1f;
    bool reachedLimit;

    // -----------------------


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

   public float limit;
   public bool useZeroLimit;
 //   public GameObject watchMultiplier;
   // float overrideValue;
  //  bool dontUseOverride;
    bool checkLimit;



	void Awake () {

    //    if (watchMultiplier != null) overrideValue = watchMultiplier.GetComponent<ValueStore>().controllerMultiply;
     //   else dontUseOverride = true;


        if ((limit != 0) || (useZeroLimit)) checkLimit = true;
       

  //      activepositionX = positionX;
		//activepositionY = positionY;
		//activepositionZ = positionZ;
		
		//activerotationX = rotationX;
		//activerotationY = rotationY;
		//activerotationZ = rotationZ;
		
		//activescaleX = scaleX;
		//activescaleY = scaleY;
		//activescaleZ = scaleZ;

        // --------


        if (reference != null)
        {
            switch (index)

            {

                case Index.IndexA:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatA;
                    break;

                case Index.IndexB:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatB;
                    break;

                case Index.IndexC:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatC;
                    break;

                case Index.IndexD:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatD;
                    break;

                case Index.IndexE:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatE;
                    break;

                case Index.IndexF:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatF;
                    break;

                case Index.IndexG:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatG;
                    break;

                case Index.IndexH:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatH;
                    break;

                case Index.IndexI:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatI;
                    break;

                case Index.IndexJ:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatJ;
                    break;

                case Index.IndexK:
                    useRefFloat = reference.GetComponent<referenceValuesObject>().storedFloatK;
                    break;

            }

        }


        activepositionX = positionX;
        activepositionY = positionY;
        activepositionZ = positionZ;

        activerotationX = rotationX;
        activerotationY = rotationY;
        activerotationZ = rotationZ;

        activescaleX = scaleX;
        activescaleY = scaleY;
        activescaleZ = scaleZ;

    } // END AWAKE


    //	 Vector3 Reorient(Vector3 startValue, Vector3 gotoValue)
    //	
    //	{
    //		return Vector3.MoveTowards(startValue,gotoValue,moveSpeed*Time.deltaTime);
    //	}


    //	void disableCurrentObject()
    //	{
    //		clearForQuit = false;
    //		gameObject.SetActive (false);
    //	}




    //	void Reposition(float positionToChange, float positionCurrent, float positionInput, bool consecutive)
    //	{
    //	}

    // ---------------------------------------------------------------------

    float CheckRef(float useFloat)
    {



        if (!flippedOnce)
        {
            if (flip)
                useDirection = -1f;
            flippedOnce = true;


            if (reference != null) useFloat = useRefFloat;
            useFloat = useFloat * useDirection;

            if (useFloat < 0) startedNegative = true;

            //	print ("startedNegative = "+startedNegative);
            return useFloat;

        }

        else
            useDirection = 1f;

        return (useFloat * useDirection);

    }



    float CheckNegative(float checkingIfNegative, float inputValue)
    {

        if (startedNegative)
        {
            if ((checkingIfNegative < 0) && (inputValue < 0))
                return checkingIfNegative * -1;
            else
                return checkingIfNegative;
        }

        else
            return checkingIfNegative;



    }




















    // ---------------------------------------------------------------------

    void OnEnable()
	{
		DoTransform ();
	}


    float CheckLimitTransform(float positionCurrent, float positionUserInput)

    {
        if ((!useZeroLimit)&&(!checkLimit)) return positionCurrent + positionUserInput;
        if (useZeroLimit) limit = 0;
        if (checkLimit)
        {

            
            float newSum = positionCurrent + positionUserInput;
            bool outOfLimit = false;
            
            if ((limit < 0) && (newSum <= limit)) outOfLimit = true;
            if ((positionCurrent < limit ) && ((limit > 0) && (newSum >= limit))) outOfLimit = true;
            if ((positionCurrent > limit) && ((limit > 0) && (newSum <= limit))) outOfLimit = true;


            if ((outOfLimit) || ((limit == 0) && (positionCurrent < 0) && ((positionCurrent + positionUserInput) >= limit)) ||
               ((limit == 0) && (positionCurrent > 0) && ((positionCurrent + positionUserInput) <= limit)))
               
            {

         //       if ((limit > 0) && ((positionCurrent + positionUserInput) >= limit)) print("A");

          //     if ((limit < 0) && ((positionCurrent + positionUserInput) <= limit)) print("B");

           //     if ((limit > 0) && ((positionCurrent + positionUserInput) <= limit)) print("C");
           //     print(""+gameObject);



 //   print("CANNOT");


                // if (0.05 > 0) (YES!!) and (0.24 >= 0.05) (ALSO  YES)

           //     print("sum "+ (positionCurrent + positionUserInput));



                //print("CANNOT DO ANYTHING");
                //print("limit "+limit);
                //print("pos current "+positionCurrent);
                //print("pos user input " + positionUserInput);
                //print("sum "+ (positionCurrent + positionUserInput));
                return positionCurrent;
                // DO NOTHING
            }
            else return positionCurrent + positionUserInput;
        }
        else return positionUserInput;

    }



    void DoTransform()

	{
        //SET X, Y, Z current positions


        if (world)
        {
            positionCurrentX = picked.transform.position.x;
            positionCurrentY = picked.transform.position.y;
            positionCurrentZ = picked.transform.position.z;
        }

        else

        {
            positionCurrentX = picked.transform.localPosition.x;
            positionCurrentY = picked.transform.localPosition.y;
            positionCurrentZ = picked.transform.localPosition.z;
        }




        // IF USER DEFINED IS ACTUALLY A NUMBER
        if (positionX != 0)
        {
            positionX = CheckRef(positionX);
            if (consecutivePX)
                positionCurrentX = CheckLimitTransform(positionCurrentX, positionX);
            else positionCurrentX = positionX;

        }

		if (positionY != 0)

        {
            positionY = CheckRef(positionY);
            if (consecutivePY) positionCurrentY = CheckLimitTransform(positionCurrentY, positionY);
            else positionCurrentY = positionY;
        }

		if (positionZ != 0) {


            positionZ = CheckRef(positionZ);

            // FROM REF VERSION
            //   if (consecutivePZ) positionCurrentZ = positionCurrentZ + positionZ;
            //   else positionCurrentZ = positionZ;



            // FROM STARCRAB VERSION
            //    if (consecutivePZ) positionCurrentZ = CheckLimitTransform(positionCurrentZ, positionZ);
            //    else positionCurrentZ = positionZ;

             if (consecutivePZ) positionCurrentZ = CheckLimitTransform(positionCurrentZ, positionZ);
            else positionCurrentZ = positionZ;





        }


        if (posXToZero) positionCurrentX = 0;
		if (posYToZero) positionCurrentY = 0;
		if (posZToZero) positionCurrentZ = 0;


        //	if (!((positionX == 0) && (positionY == 0) && (positionZ == 0) && (posXToZero == false) && (posYToZero == false) && (posZToZero == false))) 

        if ((limit != 0) || (useZeroLimit))
            { }

        //	picked.transform.localPosition = new Vector3(positionCurrentX,positionCurrentY,positionCurrentZ);
        if (world) picked.transform.position = new Vector3(positionCurrentX, positionCurrentY, positionCurrentZ);

        else picked.transform.localPosition = new Vector3(positionCurrentX, positionCurrentY, positionCurrentZ);





        //		if (killMono != null) foreach (MonoBehaviour pickedm in killMono) {
        //
        //				pickedm.enabled = false;
        //		}
        //		clearForQuit = true;

        // ----------------------------------------------------------------------------------------------------------------


        if (world)
        {
            scaleCurrentX = picked.transform.lossyScale.x;
            scaleCurrentY = picked.transform.lossyScale.y;
            scaleCurrentZ = picked.transform.lossyScale.z;
        }

        else

        {
            scaleCurrentX = picked.transform.localScale.x;
            scaleCurrentY = picked.transform.localScale.y;
            scaleCurrentZ = picked.transform.localScale.z;
        }



        if (scaleX != 0) {

            //if (consecutiveSX) scaleCurrentX = scaleCurrentX + scaleX;
            //else scaleCurrentX = scaleX;

            scaleX = CheckRef(scaleX);

            if (consecutiveSX) scaleCurrentX = CheckNegative(scaleCurrentX, scaleX) * scaleX;

            else scaleCurrentX = scaleX;





        }
		
		if (scaleY != 0) {

            scaleY = CheckRef(scaleY);

            if (consecutiveSY) scaleCurrentY = CheckNegative(scaleCurrentY, scaleY) * scaleY;

            else scaleCurrentY = scaleY;

        }
		
		if (scaleZ != 0) {

            scaleZ = CheckRef(scaleZ);

            if (consecutiveSZ) scaleCurrentZ = CheckNegative(scaleCurrentZ, scaleZ) * scaleZ;

            else scaleCurrentZ = scaleZ;


        }
		
		
		if (scaXToOne) scaleCurrentX = 1;
		if (scaYToOne) scaleCurrentY = 1;
		if (scaZToOne) scaleCurrentZ = 1;

        

		if (EnforceMinScales) 
		
		{
            //if (scaleCurrentX <= minimumScaX) scaleCurrentX = minimumScaX;
            //if (scaleCurrentY <= minimumScaY) scaleCurrentY = minimumScaY;
            //if (scaleCurrentZ <= minimumScaZ) scaleCurrentZ = minimumScaZ;

            if (scaleCurrentX <= minimumScaX)
            {
                scaleCurrentX = minimumScaX;
                reachedLimit = true;

            }



            if (!uniformScaleX)

            {

                if (scaleCurrentY <= minimumScaY)
                {
                    scaleCurrentY = minimumScaY;
                    reachedLimit = true;

                }

                if (scaleCurrentZ <= minimumScaZ)
                {
                    scaleCurrentZ = minimumScaZ;
                    reachedLimit = true;

                }
            }



        }


		if (EnforceMaxScales) 
			
		{
            //if (scaleCurrentX >= maximumScaX) scaleCurrentX = maximumScaX;
            //if (scaleCurrentX >= maximumScaY) scaleCurrentY = maximumScaY;
            //if (scaleCurrentX >= maximumScaZ) scaleCurrentZ = maximumScaZ;

            if (scaleCurrentX >= maximumScaX)
            {
                scaleCurrentX = maximumScaX;
                reachedLimit = true;

            }

            

            if (!uniformScaleX)
            {
                if (scaleCurrentY >= maximumScaY)
                {
                    if (ignoreZeroes)
                        {

                        if (maximumScaY != 0)
                            scaleCurrentY = maximumScaY;
                            reachedLimit = true;
                        }
                    else
                    
                        if (maximumScaY == 0)
                        {
                            scaleCurrentY = maximumScaY;
                            reachedLimit = true;
                        } 
                 }

                

                if (scaleCurrentZ >= maximumScaZ)
                {
                    if (ignoreZeroes)
                    {

                        if (maximumScaZ != 0)
                            scaleCurrentZ = maximumScaZ;
                        reachedLimit = true;
                    }
                    else

                        if (maximumScaZ == 0)
                    {
                        scaleCurrentZ = maximumScaZ;
                        reachedLimit = true;
                    }
                }
                
            }
            

        } // END MAX SCALE LIMITS


        if (reachedLimit)
        {
            if (disableAfterLimit)
            { 

            if (killMono.Length == 0)
                gameObject.SetActive(false);
            else foreach (MonoBehaviour pickedn in killMono)
                {
                   pickedn.enabled = false;
                }
        }






        }






        if (uniformScaleX) scaleCurrentY = scaleCurrentZ = scaleCurrentX;

        picked.transform.localScale = new Vector3(scaleCurrentX,scaleCurrentY,scaleCurrentZ);
		//picked.transform.lossyScale = new Vector3(scaleCurrentX,scaleCurrentY,scaleCurrentZ); 

	//	picked.transform.localScale.






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



		//if (killMono != null) foreach (MonoBehaviour pickedm in killMono) {
			
		////	pickedm.enabled = false; // PROBABLY THIS ONE
		//}

     //   MonoKiller();
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


		} // END DoTransform



    void MonoKiller()
    {

        if (killMono != null) foreach (MonoBehaviour pickedm in killMono)
            {

            pickedm.enabled = false;
            }



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
