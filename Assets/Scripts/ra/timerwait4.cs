using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class timerwait4 : MonoBehaviour 
{

    public enum Index{Empty,IndexA,IndexB,IndexC,IndexD,IndexE,IndexF,IndexG,IndexH,IndexI,IndexJ,IndexK}
    public bool disableOnFinish;
    public bool restart;
    bool readyToClose;
    float highestTime;

    [System.Serializable]
    
    public class instanceList

    {
    public string note = "";
    public bool mute;
    public bool show;
    public float duration;
    public GameObject[] usedObject;
    public GameObject reference;
    public Index index;


    }

    public List<instanceList> InstanceList;

    [HideInInspector]public List<float> Timeslist = new List<float>();

    



    private IEnumerator TimedSelect (float duration, bool show, GameObject[] usedObject, GameObject referenceObject)

    {
        
        {
            
            yield return new WaitForSeconds(duration);
            
            if (show == true) 
                
            {
                if (usedObject.Length > 0)

                foreach (GameObject picked in usedObject) 
                {
                    picked.SetActive (true);
                }

                if (referenceObject != null) {
                    referenceObject.SetActive (true);
                }
            }
            
            else // HIDE OBJECTS
                
            {
                if (usedObject.Length > 0)
                    
                foreach (GameObject picked in usedObject) 
                {
                    picked.SetActive (false);
                }

                if (referenceObject != null)
                    referenceObject.SetActive (false);
            }
        }

        if (duration >= highestTime) 
        {
            readyToClose = true;

        }
    }
    

//  void Start()
    void Awake()

    {
        
        foreach (instanceList inList in InstanceList) 
        {
            if (!inList.mute) Timeslist.Add (inList.duration);

            if (inList.reference != null)


            {
                switch (inList.index) 

                {

                case Index.IndexA:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectA;
                    break;

                case Index.IndexB:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectB;
                    break;

                case Index.IndexC:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectC;
                    break;

                case Index.IndexD:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectD;
                    break;

                case Index.IndexE:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectE;
                    break;

                case Index.IndexF:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectF;
                    break;

                case Index.IndexG:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectG;
                    break;

                case Index.IndexH:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectH;
                    break;

                case Index.IndexI:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectI;
                    break;

                case Index.IndexJ:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectJ;
                    break;

                case Index.IndexK:
                    inList.reference = inList.reference.GetComponent<referenceValuesObject> ().storedObjectK;
                    break;

                }

            }

        }

        highestTime = Mathf.Max (Timeslist.ToArray());
    } 


        


    public void PlayTimer (float duration, bool show, GameObject[] usedObject, GameObject referenceObject)

    {
        
                StartCoroutine (TimedSelect (duration, show, usedObject, referenceObject));

    } 








        
        void OnEnable()
        {
        readyToClose = false;
        foreach (instanceList inList in InstanceList) 
            {
            if (!inList.mute) PlayTimer (inList.duration, inList.show, inList.usedObject, inList.reference);
            }
        }






    void Update()
    {

        if (InstanceList.Count == 0)
        {
            if (disableOnFinish) readyToClose = true;
        }

                    if ((readyToClose) && (disableOnFinish)) {
                gameObject.SetActive (false);
        }

                    if ((readyToClose) && (restart) && (!disableOnFinish))
                    {
                    readyToClose = false;
                    foreach (instanceList inList in InstanceList) 
                        {
                //if (!inList.mute) PlayTimer(inList.duration, inList.show, inList.usedObject, inList.reference); // DOESN'T MATTER?
                 PlayTimer(inList.duration, inList.show, inList.usedObject, inList.reference); 

                        }
            
                    }

    } // END UPDATE

}