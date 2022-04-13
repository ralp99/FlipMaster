using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnHolder2 : MonoBehaviour {

    GameObject picked;


    [System.Serializable]

    public class instanceList

    {
        public bool hideAllAtStart;
		public bool typeAsAnim;
        public string activateMessage = "";
        public List<GameObject> enemySet = new List<GameObject>();


//        public List<GameObject> activeEnemies = new List<GameObject>();
//
//        public List<GameObject> inactiveEnemies = new List<GameObject>();

        [HideInInspector]  public List<GameObject> activeEnemies = new List<GameObject>();
        [HideInInspector]  public List<GameObject> inactiveEnemies = new List<GameObject>();

    }

    public List<instanceList> InstanceList;


    public void Activate(object[] parms)


    {
        string received = (string)parms[0];
        string type = (string)parms[1];
        bool sendLocation = (bool)parms[3];
        Vector3 location = (Vector3)parms[4];

        foreach (instanceList inList in InstanceList)
        {

            if (received == inList.activateMessage)
            {
				//print ("try make piece");
				// WILL DEF ALWAYS TRY TO MAKE A COIN FROM HERE!!!!!



				// PUTS ALL PIECES INTO ACTIVE OR INACTIVE LISTS

                SpawnedChecker();





                if (inList.inactiveEnemies.Count > 0)
                {
					inList.inactiveEnemies[0].SetActive(true);
                    if (sendLocation) inList.inactiveEnemies[0].transform.position = location;
					if (inList.typeAsAnim)

					{
                   if (type !="") inList.inactiveEnemies[0].GetComponent<playAnimator4>().triggera = type;
					}


					else
						if (type != "") // doesn't have special instruction - like inherit -

					{
						// MEANS SPAWNHOLDER HAS AVAILABLE SPOTS 
						inList.inactiveEnemies[0].GetComponent<pieceIdentity>().spawnBeneath = true;
					}

					//else print ("does have inherit");


					// UPDATES BOTH LISTS AFTER CHANGES MADE

                 SpawnedChecker();



                }

            }
        }

    }


        void Start()
    {

        foreach (instanceList inList in InstanceList)
        {
            if (inList.hideAllAtStart)
                for (int i = 0; i < inList.enemySet.Count; i++)
                {
                    picked = inList.enemySet[i];
                    picked.SetActive(false);
                }
        }

    }

    

    void SpawnedChecker() { 

		// adds ACTIVE piece to ACTIVE list if it isn't there
		// removes it from INACTIVE list

		// if it is INACTIVE
		// removes from ACTIVE list
		// puts into INACTIVE list


        foreach (instanceList inList in InstanceList)
        {

            for (int i = 0; i < inList.enemySet.Count; i++)
            {
                picked = inList.enemySet[i];
                if (picked.active == true)
                {
                    if (!inList.activeEnemies.Contains(picked)) inList.activeEnemies.Add(picked);
                    if (inList.inactiveEnemies.Contains(picked)) inList.inactiveEnemies.Remove(picked);
                }


                // IF OBJECT IS INACTIVE

                else
                {
                    if (!inList.inactiveEnemies.Contains(picked)) inList.inactiveEnemies.Add(picked);
                    if (inList.activeEnemies.Contains(picked)) inList.activeEnemies.Remove(picked);

                }



            }
        }





    }
}
