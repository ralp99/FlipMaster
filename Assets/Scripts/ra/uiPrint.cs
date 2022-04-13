using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class uiPrint : MonoBehaviour {


	public GameObject headWatch;
	public bool trackRotY;
	public bool trackTransformX;
	public bool trackColliderOn;
	float showthis;
	bool showbool;
	
	Text txt;
	 float currentscore=1f;
	
	void Start () {

		txt = gameObject.GetComponent<Text>(); 
		if (trackRotY) txt.text="Y Angle : " + showthis;
		if (trackColliderOn)
			txt.text = "Coll: " + showbool;
		if (trackTransformX) txt.text="X: "+ showthis;

			//


	}
	
	void Update () {

		if (trackRotY)
		{
			showthis = headWatch.transform.rotation.y;

			txt.text = "Y Angle : " + showthis;  
		}

			if (trackTransformX)

		{
			showthis = headWatch.transform.position.x;
			
			txt.text = "X : " + showthis;  
		}







		if (trackColliderOn)
		
		{
			if (headWatch.GetComponent<Collider>().enabled)
				showbool = true;
			else showbool = false;

			txt.text = "Coll: " + showbool;

		}



	}


}
