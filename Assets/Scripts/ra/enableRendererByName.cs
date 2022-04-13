using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enableRendererByName : MonoBehaviour {


	[System.Serializable]

	public class nameList
	{
		public GameObject [] objectHide;
		public GameObject [] objectShow;
		[HideInInspector] public GameObject objectFromStringHide;
		[HideInInspector] public GameObject objectFromStringShow;
		public string objectNameHide = "";
		public string objectNameShow = "";

	}

	public List<nameList> NameList;



	void OnEnable () {
	
		foreach (nameList inList in NameList) {

			if (inList.objectNameHide != "")
			{
			inList.objectFromStringHide = GameObject.Find(inList.objectNameHide);
		//	print (inList.objectFromStringHide);

			//inList.objectFromStringHide.SetActive(false);
				inList.objectFromStringHide.GetComponent<MeshRenderer>().enabled = false;
			}

			if (inList.objectNameShow != "")
			{
			inList.objectFromStringShow = GameObject.Find(inList.objectNameShow);
			//inList.objectFromStringShow.SetActive(true);
				inList.objectFromStringShow.GetComponent<MeshRenderer>().enabled = true;

			}


	}
	
		gameObject.SetActive(false);

}
}
