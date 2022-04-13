using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pieceIdentity : MonoBehaviour {

    // piece above is diff of 6


    FmManager fmManagerRef;

    public CoinColors SideAColor;
    public CoinColors SideBColor;
    public CoinColors ActiveColor;
    
    public GameObject sideAObject, sideBObject, armedAlert;
	
   // /*
	public GameObject NBoundaryObject;
	public GameObject SBoundaryObject;
	public GameObject EBoundaryObject;
	public GameObject WBoundaryObject;
  //  */

    [HideInInspector] public CoinColors NBoundaryMat;
    [HideInInspector] public CoinColors SBoundaryMat;
    [HideInInspector] public CoinColors EBoundaryMat;
    [HideInInspector] public CoinColors WBoundaryMat;
    	
	public string searchTag = "";

	public List<GameObject> clearList;
	// [HideInInspector] public List<GameObject> clearListTemp; // NOT SURE IF NEED
	
	[HideInInspector] public List<GameObject> clearListHoriz;
	
	[HideInInspector] public List<GameObject> clearListVert;
	
	public int value;
	
	[HideInInspector] public float newXPos, newYPos;
	public bool spawnBeneath;
	public bool shooterPiece;
	public bool shootChooseCurrentPiece;
	public bool shootChooseNextPiece;
	
	public string spawnCommand = "";
	public GameObject spawnHolder;
	public GameObject spawnCoinMaker;
	public GameObject watchCoin;
	public GameObject shooterCoin;
	
	public bool shouldClearCheck;
	
	bool pieceHasFlipped;

    // public bool armedHoriz, armedVert, armedAny; // DUNNO IF NEED

    public int coinListPosition;
	
	
	void Update () 
		
		
		
	{


        if ((!shooterPiece) || (!shootChooseCurrentPiece) || (!shootChooseNextPiece)){
			if (transform.gameObject.tag == "previewCoin") {
				
				if (transform.position.y < 13) {
					transform.gameObject.tag = "coin";
					CheckNeighborsDo ();
				}
			}
		}  
		
		if (spawnBeneath)
			SpawnConvert ();
		
		if (shouldClearCheck)
		{
			//if (clearList.Count > 2) {
			
			if ((clearListHoriz.Count > 2) || (clearListVert.Count > 2)) {
				foreach (GameObject picked in clearList) {
					picked.SetActive (false);
        
                    if (fmManagerRef.currentPlayfieldPieces.Contains(picked))
                    {
                         fmManagerRef.currentPlayfieldPieces.Remove(picked);
                    }

                    if (picked != gameObject)
					{
						picked.GetComponent<pieceIdentity> ().clearList.Clear ();
						picked.GetComponent<pieceIdentity> ().clearListHoriz.Clear ();
						picked.GetComponent<pieceIdentity> ().clearListVert.Clear ();
						
					}
				}
				
				if (!gameObject.activeSelf)
				{
					clearList.Clear ();
					clearListHoriz.Clear ();
					clearListVert.Clear ();

                }
				shouldClearCheck = false;
			}  
			else StartCoroutine(ClearOffTimer());
			
		}  
		
        if (tempAssignBounds)
        {
            AssignBoundaryItems();
            tempAssignBounds = false;
        }


    }  // END UPDATE

    public bool tempAssignBounds;
    
	
	private IEnumerator ClearOffTimer ()
	{
		yield return new WaitForSeconds (0.25f);
		shouldClearCheck = false;
	}
	
	void AssignCoinListPosition()
    {
          coinListPosition = fmManagerRef.currentPlayfieldPieces.Count;
    }

    void SpawnConvert()
	{
		UseShotColors (); // ASSIGNS COLOR VALUES FROM STORED, TO COIN BEING PLACED WHERE COLLIDE HAPPENED
		StartCoroutine (TimedSelect());
		shouldClearCheck = true;
		spawnBeneath = false;
        fmManagerRef.currentPlayfieldPieces.Add(gameObject);
        AssignCoinListPosition();

    }
	
	void SpawnPiece (string tellMe)
		// RECEIVES MESSAGE FROM HITTER TO makeCoin AS A SPAWNED PIECE
		
	{
		// disable this object's spawnBneath boolean
		// make a new coin - send message to spawnHolder to activate it, and access it??
		// it should become spawnedPiece
		// assign it bneath current one Y position
		// get SHOT piece sideA and sideB
		// start clear if more than 3 match in its list
		// either way at this point, reassign spawnedPiece as NULL
		
		if (tellMe == spawnCommand)
			spawnCoinMaker.SetActive (true);
		
		
	}
	
	
	
	void Start () {

        if (fmManagerRef == null)
        {
            fmManagerRef = FmManager.instance;
        }


        //		if (shootChooseNextPiece)
        //		{
        //			RandomizeColors();
        //		}


        AssignChooseCoins ();
		
		if (shootChooseCurrentPiece) 
		{
			RandomizeColors();
			MaterialsCheck(gameObject);

            SetValuestoreColors(SideAColor, SideBColor, ActiveColor);
		
		}


        int spawnOrder = fmManagerRef.currentPlayfieldPieces.Count;
        


        if (spawnBeneath) 
		{
			
		}
		
		else if (spawnOrder > 0) 
			
			
		{
			
			newXPos =
            ((fmManagerRef.currentPlayfieldPieces[spawnOrder - 1].transform.localPosition.x));



            if (newXPos < -4) 
				
				
			{
				newXPos = 0;
                newYPos = ((fmManagerRef.currentPlayfieldPieces[spawnOrder - 6].transform.localPosition.y) + 0.82f);



                transform.localPosition =
                                    new Vector3(transform.localPosition.x, ((fmManagerRef.currentPlayfieldPieces[spawnOrder - 6].transform.localPosition.y) + 0.82f),

                 transform.localPosition.z);
				
			}  
			
			else 
				
			{
				
				transform.localPosition = new Vector3 ((newXPos - 0.862f),
                              (fmManagerRef.currentPlayfieldPieces[spawnOrder - 1].transform.localPosition.y),

                    transform.localPosition.z);
				
			}

        }  // END SPAWNORDER

        //if (transform.position.y < 12)


        //tell it where to appear

        List<string> selectPool = new List<string>();



        if ((!shooterPiece) && (!spawnBeneath) && (!shootChooseNextPiece) && (!shootChooseCurrentPiece))

        {
            RandomizeColors();
            fmManagerRef.currentPlayfieldPieces.Add(gameObject);
            AssignCoinListPosition();
        }


    }  // END START









    void UseShotColors()
		
	{

        SideAColor = fmManagerRef.ShooterSideAColor;
        SideBColor = fmManagerRef.ShooterSideBColor;
        ActiveColor = fmManagerRef.ShooterActiveColor;

        MaterialsCheck(gameObject);
	}
	
	
	
	
	
	
	
	
	
	void RandomizeColors()
		
	{

        
        // VERY TEMP SELECTION METHOD UNTIL I REMEMBER HOW TO USE ENUM INT SELECTION PROPERLY
        int sideAint = Random.Range(0, 4);
        int sideBint = Random.Range(0, 4);

        SideAColor = SelectRandomColor();
        SideBColor = SelectRandomColor();

        while (SideAColor == SideBColor)
        {
            SideBColor = SelectRandomColor();
        }


        if (pieceHasFlipped)
        {
            ActiveColor = SideBColor;
        }
        else
        {
            ActiveColor = SideAColor;
        }


		MaterialsCheck (gameObject);
	}

    CoinColors SelectRandomColor()

    {
        int randomInt = Random.Range(0, 5);
        CoinColors currentColor = CoinColors.Red;

        switch (randomInt)
        {
            case 0:
                currentColor = CoinColors.Red;
                break;

            case 1:
                currentColor = CoinColors.Green;
                break;

            case 2:
                currentColor = CoinColors.Blue;
                break;

            case 3:
                currentColor = CoinColors.Purple;
                break;

            case 4:
                currentColor = CoinColors.Yellow;
                break;

            default:
                break;
        }

        return currentColor;
    }



    void MaterialsCheck(GameObject currentObject)
    {

        if (currentObject.GetComponent<pieceIdentity>().SideAColor == CoinColors.Blue)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideAObject, "tempCoinBlue");

        if (currentObject.GetComponent<pieceIdentity>().SideAColor == CoinColors.Green)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideAObject, "tempCoinGreen");

        if (currentObject.GetComponent<pieceIdentity>().SideAColor == CoinColors.Purple)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideAObject, "tempCoinPurple");

        if (currentObject.GetComponent<pieceIdentity>().SideAColor == CoinColors.Red)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideAObject, "tempCoinRed");

        if (currentObject.GetComponent<pieceIdentity>().SideAColor == CoinColors.Yellow)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideAObject, "tempCoinYellow");

        if (currentObject.GetComponent<pieceIdentity>().SideBColor == CoinColors.Blue)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideBObject, "tempCoinBlue");

        if (currentObject.GetComponent<pieceIdentity>().SideBColor == CoinColors.Green)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideBObject, "tempCoinGreen");

        if (currentObject.GetComponent<pieceIdentity>().SideBColor == CoinColors.Purple)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideBObject, "tempCoinPurple");

        if (currentObject.GetComponent<pieceIdentity>().SideBColor == CoinColors.Red)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideBObject, "tempCoinRed");

        if (currentObject.GetComponent<pieceIdentity>().SideBColor == CoinColors.Yellow)
            AssignTempMaterials(currentObject.GetComponent<pieceIdentity>().sideBObject, "tempCoinYellow");

    }



    void AssignTempMaterials(GameObject assignSide, string useColorName)
		
	{
		
		Material newTempMat = Resources.Load(useColorName, typeof(Material)) as Material;
		assignSide.GetComponent<Renderer> ().material = newTempMat;
		
	}
	
	
	
	
	
	
	
	void AssignChooseCoins()
		
	{

		if (shootChooseNextPiece)
			RandomizeColors ();
		
		
		if (shootChooseCurrentPiece)
		{
			if (!pieceHasFlipped)


				{
					if (watchCoin.GetComponent<pieceIdentity>().pieceHasFlipped)
					{

						SideAColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;
						SideBColor = watchCoin.GetComponent<pieceIdentity> ().SideAColor;
						ActiveColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;
					}
					
					else
						
					{

                    SideAColor = watchCoin.GetComponent<pieceIdentity> ().SideAColor;
					SideBColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;
                    ActiveColor = watchCoin.GetComponent<pieceIdentity> ().ActiveColor;

					}

				shooterCoin.GetComponent<pieceIdentity> ().SideAColor = SideAColor;
				shooterCoin.GetComponent<pieceIdentity> ().SideBColor = SideBColor;

				SetValuestoreColors(SideAColor, SideBColor, ActiveColor);


				}

			else


			{
				if (watchCoin.GetComponent<pieceIdentity>().pieceHasFlipped)
				{
					
					SideAColor = watchCoin.GetComponent<pieceIdentity> ().SideAColor;
                    SideBColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;
					ActiveColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;

				}
				
				else
					
				{
					
					SideAColor = watchCoin.GetComponent<pieceIdentity> ().SideBColor;
					SideBColor = watchCoin.GetComponent<pieceIdentity> ().SideAColor;
					ActiveColor = watchCoin.GetComponent<pieceIdentity> ().SideAColor;

					
				}
				
				shooterCoin.GetComponent<pieceIdentity> ().SideAColor = SideBColor;
				shooterCoin.GetComponent<pieceIdentity> ().SideBColor = SideAColor;

				SetValuestoreColors(SideBColor, SideAColor, SideBColor);

			}
          
            MaterialsCheck(gameObject);
			MaterialsCheck (shooterCoin);

		}
	}





    //	void OnEnable ()
    //
    //	{
    //			if (shooterPiece)
    //
    //				{
    //			print ("reassigning");
    //
    //					sideAcolor = valueObject.GetComponent<ValueStore> ().shooterSideAcolor;
    //					sideBcolor = valueObject.GetComponent<ValueStore> ().shooterSideBcolor;
    //					facingColor = valueObject.GetComponent<ValueStore> ().shooterFacingColor;
    //					MaterialsCheck ();
    //				}
    //
    //
    //
    //		//spawn info
    //
    //
    //		//make list of all previous coins
    //		// stick this one to the far right, if offscreen, then appear on consecutive line below)
    //		// don't appear below a certain line?
    //
    //	}

        
        /*
        void OnEnable()
    {

        if (fmManagerRef == null)
        {
            fmManagerRef = FmManager.instance;
        }
    

    }
    */

    void SetValuestoreColors(CoinColors inColorA, CoinColors inColorB, CoinColors inColorFacing)


    {
       fmManagerRef.ShooterSideAColor = inColorA;
       fmManagerRef.ShooterSideBColor = inColorB;
       fmManagerRef.ShooterActiveColor = inColorFacing;
    }
	

	
	
	private IEnumerator TimedSelect ()
	{
		yield return new WaitForSeconds (0.2f);
		CheckNeighborsDo ();
	}
	
	
	
	void Flip ()
	{
		
		if ((!shootChooseCurrentPiece) && (!shootChooseNextPiece) && (!shooterPiece)) 
			
		{
			foreach (GameObject picked in GameObject.FindGameObjectsWithTag(searchTag)) 
			{
				picked.GetComponent<pieceIdentity> ().clearList.Clear ();
				picked.GetComponent<pieceIdentity> ().clearListHoriz.Clear ();
				picked.GetComponent<pieceIdentity> ().clearListVert.Clear ();
				
			}
			
		}
		
		
		if (shootChooseNextPiece)
		{
			if (!pieceHasFlipped) pieceHasFlipped = true;
			else pieceHasFlipped = false;
		}


		if (shootChooseCurrentPiece)
		{
			if (!pieceHasFlipped) 
			{
				pieceHasFlipped = true;
               
                shooterCoin.GetComponent<pieceIdentity>().SideAColor = SideBColor;
                shooterCoin.GetComponent<pieceIdentity>().SideBColor = SideAColor;
                SetValuestoreColors(SideBColor, SideAColor, SideBColor);
         
            }
			
			else 
				
			{
				pieceHasFlipped = false;
               
                shooterCoin.GetComponent<pieceIdentity>().SideAColor = SideAColor;
                shooterCoin.GetComponent<pieceIdentity>().SideBColor = SideBColor;
                SetValuestoreColors(SideAColor, SideBColor, SideAColor);

            }

			MaterialsCheck (shooterCoin);

		}



		if (ActiveColor == SideAColor)
			ActiveColor = SideBColor;
		else 
			ActiveColor = SideAColor;

	

		
		if ((!shootChooseCurrentPiece) && (!shootChooseNextPiece) && (!shooterPiece)) CheckNeighborsDo ();
		
	}
	






	// ----------------------------------------------------------------------------------

	// ----------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------

	// ----------------------------------------------------------------------------------






	
	
	void CheckNeighborsDo ()
	{
		
		
		foreach (GameObject picked in GameObject.FindGameObjectsWithTag(searchTag)) 
		{
			CheckNeighbors (picked); 
		}
		
	}
	
	// /*
	void AllBoundariesCheck (GameObject boundary)
	{
		
		foreach (GameObject picked in GameObject.FindGameObjectsWithTag(searchTag)) 
		{
			
			if ((picked.GetComponent<pieceIdentity> ().clearList.Contains (picked)) == false)
				picked.GetComponent<pieceIdentity> ().clearList.Add (picked);
			
			
			if ((picked.GetComponent<pieceIdentity> ().clearListHoriz.Contains (picked)) == false)
				picked.GetComponent<pieceIdentity> ().clearListHoriz.Add (picked);
			
			if ((picked.GetComponent<pieceIdentity> ().clearListVert.Contains (picked)) == false)
				picked.GetComponent<pieceIdentity> ().clearListVert.Add (picked);
			
			
			
		}
		
		
		Transform distanceA = boundary.transform;
		
		foreach (GameObject picked in GameObject.FindGameObjectsWithTag(searchTag)) {
			
			Transform distanceB = picked.transform;
			
			if (Vector3.Distance (distanceA.position, distanceB.position) < 0.2) {
				
				
				if (boundary.tag == "boundaryW")
				{
					picked.GetComponent<pieceIdentity> ().EBoundaryMat = boundary.GetComponentInParent<pieceIdentity> ().ActiveColor;
					if (boundary.GetComponentInParent<pieceIdentity> ().ActiveColor == picked.GetComponent<pieceIdentity> ().ActiveColor)
					{
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearListHoriz, picked.GetComponent<pieceIdentity> ().clearListHoriz);
						
					}
					
					else 
					{
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearListHoriz, picked.GetComponent<pieceIdentity> ().clearListHoriz);
						
					}
					
				}
				
				
				if (boundary.tag == "boundaryS")
				{
					picked.GetComponent<pieceIdentity> ().NBoundaryMat = boundary.GetComponentInParent<pieceIdentity> ().ActiveColor;
					
					
					if (boundary.GetComponentInParent<pieceIdentity> ().ActiveColor == picked.GetComponent<pieceIdentity> ().ActiveColor)
					{
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearListVert, picked.GetComponent<pieceIdentity> ().clearListVert);
					}
					
					else 
						
					{
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearListVert, picked.GetComponent<pieceIdentity> ().clearListVert);
					}
				}
				
				
				
				
				if (boundary.tag == "boundaryE")
				{
					picked.GetComponent<pieceIdentity> ().WBoundaryMat = boundary.GetComponentInParent<pieceIdentity> ().ActiveColor;
					
					if (boundary.GetComponentInParent<pieceIdentity> ().ActiveColor == picked.GetComponent<pieceIdentity> ().ActiveColor)
					{
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearListHoriz, picked.GetComponent<pieceIdentity> ().clearListHoriz);
						
					}
					
					else 
						
					{
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearListHoriz, picked.GetComponent<pieceIdentity> ().clearListHoriz);
					}
				}
				
				
				if (boundary.tag == "boundaryN")
				{
					picked.GetComponent<pieceIdentity> ().SBoundaryMat = boundary.GetComponentInParent<pieceIdentity> ().ActiveColor;
					
					if (boundary.GetComponentInParent<pieceIdentity> ().ActiveColor == picked.GetComponent<pieceIdentity> ().ActiveColor)
						
					{
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistAdding (boundary.GetComponentInParent<pieceIdentity> ().clearListVert, picked.GetComponent<pieceIdentity> ().clearListVert);
						
					}
					
					else 
						
					{
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearList, picked.GetComponent<pieceIdentity> ().clearList);
						ClearlistRemoving (boundary.GetComponentInParent<pieceIdentity> ().clearListVert, picked.GetComponent<pieceIdentity> ().clearListVert);
						
					}
				}
			}
		}
	}
//	*/
	// ----------------- END BOUNDARY CHECKS
	

















	
	void ClearlistAdding (List <GameObject> objectListA, List <GameObject> objectListB)
	{
		
		
		foreach (GameObject addingList in objectListA)
			if (objectListB.Contains(addingList) == false)
				objectListB.Add(addingList);
		
		foreach (GameObject addingList in objectListB)
			if (objectListA.Contains(addingList) == false)
				objectListA.Add(addingList);
		
	}
	
	
	
	
	
	
	void ClearlistRemoving (List <GameObject> objectListA, List <GameObject> objectListB)
	{
		
		
		foreach (GameObject addingList in objectListA)
			if (objectListB.Contains(addingList) == false)
				objectListB.Remove(addingList);
		
		foreach (GameObject addingList in objectListB)
			if (objectListA.Contains(addingList) == false)
				objectListA.Remove(addingList);
		
	}
	
    void AssignBoundaryItems()
    {
        /*
        int coinListVerticalSearchRange = 6;

        if (coinListPosition > 0)
        {
            WBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition - 1];
        }

        else
        {
            WBoundaryObject = null;
        }

        if (coinListPosition < fmManagerRef.currentPlayfieldPieces.Count)
        {
            EBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition+1];
        }

        else
        {
            EBoundaryObject = null;
        }

        if (coinListPosition - coinListVerticalSearchRange > 0)
        {
            SBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition + coinListVerticalSearchRange];
        }

        else
        {
            SBoundaryObject = null;
        }


        if (coinListPosition + coinListVerticalSearchRange < fmManagerRef.currentPlayfieldPieces.Count)
        {
            NBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition - coinListVerticalSearchRange];
        }

        else
        {
            NBoundaryObject = null;
        }
        */


        int coinListVerticalSearchRange = 6;

        if (coinListPosition > 0)
        {
            WBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition - 2];
        }

        else
        {
            WBoundaryObject = null;
        }





        if (coinListPosition < fmManagerRef.currentPlayfieldPieces.Count)
        {
            EBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition];
        }

        else
        {
            EBoundaryObject = null;
        }

        if (coinListPosition - coinListVerticalSearchRange > 0)
        {
            SBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition + coinListVerticalSearchRange];
        }

        else
        {
            SBoundaryObject = null;
        }


        if (coinListPosition + coinListVerticalSearchRange < fmManagerRef.currentPlayfieldPieces.Count)
        {
            NBoundaryObject = fmManagerRef.currentPlayfieldPieces[coinListPosition - coinListVerticalSearchRange];
        }

        else
        {
            NBoundaryObject = null;
        }



    }
	
	
	void CheckNeighbors(GameObject currentCoin)
	{
		
		//	currentCoin.GetComponent<pieceIdentity>().clearList.Clear();
		
		foreach (GameObject picked in GameObject.FindGameObjectsWithTag(searchTag))
		{
			AllBoundariesCheck (picked.GetComponent<pieceIdentity> ().NBoundaryObject);
			AllBoundariesCheck (picked.GetComponent<pieceIdentity> ().SBoundaryObject);
			AllBoundariesCheck (picked.GetComponent<pieceIdentity> ().EBoundaryObject);
			AllBoundariesCheck (picked.GetComponent<pieceIdentity> ().WBoundaryObject);
			
			//rearrange lists here??
			
			foreach (GameObject sublistItem in picked.GetComponent<pieceIdentity>().clearList)
			{
				//clearListTemp
				//				Vector3 coinPos = new Vector3 (sublistItem.transform.position.x, sublistItem.transform.position.y, sublistItem.transform.position.z);
				//
				//				hits = hits.OrderBy(
				//					x => Vector2.Distance(this.transform.position,x.transform.position)
				//					).ToList();
				
			}
			
			
			
		}
		
		
	}     
	
	
	
	
}




