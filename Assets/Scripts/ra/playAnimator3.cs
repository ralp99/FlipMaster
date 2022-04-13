using UnityEngine;
using System.Collections;

public class playAnimator3 : MonoBehaviour {

	public GameObject animObject;
	public string triggera = "";
	public bool spam;
	public float threshold = .5f;
	bool playNext = false;

		void Update () {

			animObject.GetComponent<Animator>().SetTrigger(triggera);
		if (spam)
			gameObject.SetActive (false);
		else

		{
			StartCoroutine(TimedSelect());
			if (playNext == true) {
								animObject.GetComponent<Animator> ().ResetTrigger (triggera);
								playNext = false;
								gameObject.SetActive (false);
							} else
								playNext = false;
		}

						}


			private IEnumerator TimedSelect ()
						{
							yield return new WaitForSeconds(threshold);
				
							playNext = true;
						}



		// TO DO - ADD SPAMMING, SPAMMING DELAY TIME

//			StartCoroutine(TimedSelect());
//			if (playNext == true) {
//				animObject.GetComponent<Animator> ().ResetTrigger (triggera);
//				playNext = false;
//				gameObject.SetActive (false);
//			} else
//				playNext = false;
//	
//		}
//
//	
//		private IEnumerator TimedSelect ()
//		{
//			yield return new WaitForSeconds(.5f);
//
//			playNext = true;
//	
//		}


}


