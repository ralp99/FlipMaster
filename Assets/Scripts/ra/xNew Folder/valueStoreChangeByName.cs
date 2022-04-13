using UnityEngine;
using System.Collections;

/*

sends data to ValueStore

*/

public class valueStoreChangeByName : MonoBehaviour {
	public enum SendType{Integer, Float, String, Object, Bool}

	public SendType sendType;
    public GameObject valueStoreObject;
    public string field = "";
	public float value;
	public string newString = "";
	public bool newBool;
	public GameObject[] newObject;

	void OnEnable ()

	{


		if(valueStoreObject != null)
		{
			var valueStore = valueStoreObject.GetComponent<ValueStore>();
			var theField = valueStore.GetType().GetField(field);
			
			if(theField != null)
			{

				switch (sendType) {
				
				case SendType.Float:
					float prevValueFloat = (float)theField.GetValue(valueStore);
					theField.SetValue(valueStore,prevValueFloat + value);
				break;


				case SendType.Integer:

					int newInt = (int) value;
					int prevValueInt = (int)theField.GetValue(valueStore);

					theField.SetValue(valueStore,prevValueInt + newInt);
				break;

				case SendType.String:
					theField.SetValue(valueStore,newString);
					break;

				case SendType.Bool:
					theField.SetValue(valueStore,newBool);
					break;

				case SendType.Object:
					theField.SetValue(valueStore,newObject);
					break;

				}

			}
		}

		gameObject.SetActive (false);

	}

}
