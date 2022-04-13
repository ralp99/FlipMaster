using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class timerwait2 : MonoBehaviour {

	public bool disableOnFinish;
	public bool restart;
    bool readyToClose;
	float highestTime;
	
	[System.Serializable]
	
	public class instanceList

	{
	public bool mute;
	public bool show;
	public float duration;
	public GameObject[] usedObject;
	}

	public List<instanceList> InstanceList;

	[HideInInspector]public List<float> Timeslist = new List<float>();

	



	private IEnumerator TimedSelect (float duration, bool show, GameObject[] usedObject)

	{
		
		{
			
			yield return new WaitForSeconds(duration);
			
			if (show == true) 
				
			{
				foreach (GameObject picked in usedObject) 
				{
					picked.SetActive (true);
				}
				
			}
			
			else
				
			{
				foreach (GameObject picked in usedObject) 
				{
					picked.SetActive (false);
				}
			}
		}

		if (duration >= highestTime) 
		{
			readyToClose = true;

		}
	}
	

	void Start()
	{
		foreach (instanceList inList in InstanceList) 
		{
			if (!inList.mute) Timeslist.Add (inList.duration);

		}

		highestTime = Mathf.Max (Timeslist.ToArray());
	}


public void PlayTimer (float duration, bool show, GameObject[] usedObject)

{
		foreach (instanceList inList in InstanceList) 
		{
		StartCoroutine(TimedSelect(duration, show, usedObject));

	}
	
		
	}
		
		
		void OnEnable()
		{
		readyToClose = false;
		foreach (instanceList inList in InstanceList) 
			{
				if (!inList.mute) PlayTimer (inList.duration, inList.show, inList.usedObject);
			}
		}


	void Update()
	{
					if ((readyToClose) && (disableOnFinish)) {
				gameObject.SetActive (false);
		}

					if ((readyToClose) && (restart) && (!disableOnFinish))
					{
					readyToClose = false;
					foreach (instanceList inList in InstanceList) 
						{
							PlayTimer(inList.duration, inList.show, inList.usedObject);
						}
			
					}

	}

}