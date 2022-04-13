using UnityEngine;
using System.Collections;

public class enableComponent : MonoBehaviour {

    public Behaviour[] enableComponents;
    public Behaviour[] disableComponents;


    void OnEnable () {

        foreach (Behaviour picked in enableComponents) picked.enabled = true;
        foreach (Behaviour picked in disableComponents) picked.enabled = false;


        gameObject.SetActive(false);
    }
	
}
