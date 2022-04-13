using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class referenceValuesObject : MonoBehaviour {

    /*
     
        NOTES:
        FloatInput (OBJECT with Int Instance, Float FloatNumber) 
      
     
    */

    public enum IndexEntry {A, B, C, D, E, F, G, H, I, J, K}


	[HideInInspector]public GameObject storedObjectA, storedObjectB, storedObjectC, storedObjectD, storedObjectE,
	storedObjectF, storedObjectG, storedObjectH, storedObjectI, storedObjectJ, storedObjectK;
	[HideInInspector]public int storedIntA, storedIntB, storedIntC, storedIntD, storedIntE, storedIntF, storedIntG,
	storedIntH, storedIntI, storedIntJ, storedIntK;
	[HideInInspector]public float storedFloatA, storedFloatB, storedFloatC, storedFloatD, storedFloatE, storedFloatF,
	storedFloatG, storedFloatH, storedFloatI, storedFloatJ, storedFloatK;
	[HideInInspector]public string storedStringA, storedStringB, storedStringC, storedStringD, storedStringE,
	storedStringF, storedStringG, storedStringH, storedStringI, storedStringJ, storedStringK;



//	public GameObject storedObjectA, storedObjectB, storedObjectC, storedObjectD, storedObjectE,
//	storedObjectF, storedObjectG, storedObjectH, storedObjectI, storedObjectJ, storedObjectK;
//	public int storedIntA, storedIntB, storedIntC, storedIntD, storedIntE, storedIntF, storedIntG,
//	storedIntH, storedIntI, storedIntJ, storedIntK;
//	public float storedFloatA, storedFloatB, storedFloatC, storedFloatD, storedFloatE, storedFloatF,
//	storedFloatG, storedFloatH, storedFloatI, storedFloatJ, storedFloatK;
//	public string storedStringA, storedStringB, storedStringC, storedStringD, storedStringE,
//	storedStringF, storedStringG, storedStringH, storedStringI, storedStringJ, storedStringK;




	[System.Serializable]

	public class instanceList

	{
		public string note = "";
		//public ReferenceType referenceType;
		public IndexEntry indexEntry;
		public GameObject storedObject;
		public string storedString;
		public bool zeroInt, zeroFloat;
		public int storedInt;
		public float storedFloat;
		public bool mute;
	}

	public List<instanceList> InstanceList;





	float CheckIndexesFloat (float localValue, float storedValue, bool checkBool)
	{
		if (localValue != 0) {
			if (checkBool)
				return 0;
			else return localValue;
		} 

		else return storedValue;
	}





	int CheckIndexesInt (int localValue, int storedValue, bool checkBool)
	{
		if (localValue != 0) {
			if (checkBool)
				return 0;
			else return localValue;
		} 

		else return storedValue;
	}





	GameObject CheckIndexesObj (GameObject localValue, GameObject storedValue)
	{
		if (localValue != null) return localValue;
		else return storedValue;
	}



	string CheckIndexesString (string localValue, string storedValue)
	{
		if (localValue != null) return localValue;
		else return storedValue;
	}



    //	void DoSet (float storedFloat, int storedInt, GameObject storedObject, string storedString, bool currentZeroInt,
    //		float currentFloat, int currentInt, GameObject currentObject, string currentString, bool currentZeroFloat)
    //		{
    //		storedFloat = CheckIndexesFloat (currentFloat,storedFloat,currentZeroFloat);
    //		storedInt = CheckIndexesInt (currentInt,storedInt,currentZeroInt);
    //		storedObject = CheckIndexesObj (currentObject,storedObject);
    //		storedString = CheckIndexesString (currentString,storedString);
    //
    //		}


    //      -------------------------


    public void FloatInput(object[] parms)

    {
        float floatNumber = (float)parms[0];
        int instance = (int)parms[1];

        InstanceList[instance].storedFloat = floatNumber;
        
    }
    


    void Awake ()
	{

		foreach (instanceList inList in InstanceList) 
		{
			if (!inList.mute)
			switch (inList.indexEntry) 
			{

			case IndexEntry.A:
				storedFloatA = CheckIndexesFloat (inList.storedFloat,storedFloatA,inList.zeroFloat);
				storedIntA = CheckIndexesInt (inList.storedInt,storedIntA,inList.zeroInt);
				storedObjectA = CheckIndexesObj (inList.storedObject,storedObjectA);
				storedStringA = CheckIndexesString (inList.storedString,storedStringA);
				break;


			case IndexEntry.B:
				storedFloatB = CheckIndexesFloat (inList.storedFloat,storedFloatB,inList.zeroFloat);
				storedIntB = CheckIndexesInt (inList.storedInt,storedIntB,inList.zeroInt);
				storedObjectB = CheckIndexesObj (inList.storedObject,storedObjectB);
				storedStringB = CheckIndexesString (inList.storedString,storedStringB);
				break;

			case IndexEntry.C:
				storedFloatC = CheckIndexesFloat (inList.storedFloat,storedFloatC,inList.zeroFloat);
				storedIntC = CheckIndexesInt (inList.storedInt,storedIntC,inList.zeroInt);
				storedObjectC = CheckIndexesObj (inList.storedObject,storedObjectC);
				storedStringC = CheckIndexesString (inList.storedString,storedStringC);
				break;

			case IndexEntry.D:
				storedFloatD = CheckIndexesFloat (inList.storedFloat,storedFloatD,inList.zeroFloat);
				storedIntD = CheckIndexesInt (inList.storedInt,storedIntD,inList.zeroInt);
				storedObjectD = CheckIndexesObj (inList.storedObject,storedObjectD);
				storedStringD = CheckIndexesString (inList.storedString,storedStringD);
				break;

			case IndexEntry.E:
				storedFloatE = CheckIndexesFloat (inList.storedFloat,storedFloatE,inList.zeroFloat);
				storedIntE = CheckIndexesInt (inList.storedInt,storedIntE,inList.zeroInt);
				storedObjectE = CheckIndexesObj (inList.storedObject,storedObjectE);
				storedStringE = CheckIndexesString (inList.storedString,storedStringE);
				break;

			case IndexEntry.F:
				storedFloatF = CheckIndexesFloat (inList.storedFloat,storedFloatF,inList.zeroFloat);
				storedIntF = CheckIndexesInt (inList.storedInt,storedIntF,inList.zeroInt);
				storedObjectF = CheckIndexesObj (inList.storedObject,storedObjectF);
				storedStringF = CheckIndexesString (inList.storedString,storedStringF);
				break;

			case IndexEntry.G:
				storedFloatG = CheckIndexesFloat (inList.storedFloat,storedFloatG,inList.zeroFloat);
				storedIntG = CheckIndexesInt (inList.storedInt,storedIntG,inList.zeroInt);
				storedObjectG = CheckIndexesObj (inList.storedObject,storedObjectG);
				storedStringG = CheckIndexesString (inList.storedString,storedStringG);
				break;

			case IndexEntry.H:
				storedFloatH = CheckIndexesFloat (inList.storedFloat,storedFloatH,inList.zeroFloat);
				storedIntH = CheckIndexesInt (inList.storedInt,storedIntH,inList.zeroInt);
				storedObjectH = CheckIndexesObj (inList.storedObject,storedObjectH);
				storedStringH = CheckIndexesString (inList.storedString,storedStringH);
				break;

			case IndexEntry.I:
				storedFloatI = CheckIndexesFloat (inList.storedFloat,storedFloatI,inList.zeroFloat);
				storedIntI = CheckIndexesInt (inList.storedInt,storedIntI,inList.zeroInt);
				storedObjectI = CheckIndexesObj (inList.storedObject,storedObjectI);
				storedStringI = CheckIndexesString (inList.storedString,storedStringI);
				break;

			case IndexEntry.J:
				storedFloatJ = CheckIndexesFloat (inList.storedFloat,storedFloatJ,inList.zeroFloat);
				storedIntJ = CheckIndexesInt (inList.storedInt,storedIntJ,inList.zeroInt);
				storedObjectJ = CheckIndexesObj (inList.storedObject,storedObjectJ);
				storedStringJ = CheckIndexesString (inList.storedString,storedStringJ);
				break;

			case IndexEntry.K:
				storedFloatK = CheckIndexesFloat (inList.storedFloat,storedFloatK,inList.zeroFloat);
				storedIntK = CheckIndexesInt (inList.storedInt,storedIntK,inList.zeroInt);
				storedObjectK = CheckIndexesObj (inList.storedObject,storedObjectK);
				storedStringK = CheckIndexesString (inList.storedString,storedStringK);
				break;

			}


		}

	}

}
