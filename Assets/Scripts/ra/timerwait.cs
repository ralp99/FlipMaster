using UnityEngine;
using System.Collections;



public class timerwait : MonoBehaviour {

	
	public float duration;
	public bool disableOnFinish;
	public bool restart;
	public bool show;
	public GameObject[] usedObject;

	
	
	private IEnumerator TimedSelect ()
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

				if (disableOnFinish)
					gameObject.SetActive(false);
			if ((restart) && (!disableOnFinish))
				PlayTimer();


		}
		
	}
	
	public void PlayTimer ()
	{
		StartCoroutine(TimedSelect());
		
	}

	
	void OnEnable()
	{

		PlayTimer ();
		
	}

}