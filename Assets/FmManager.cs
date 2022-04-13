using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CoinColors { Red, Green, Blue, Yellow, Purple };


public class FmManager : MonoBehaviour {


    public static FmManager instance;

    public CoinColors ShooterSideAColor;
    public CoinColors ShooterSideBColor;
    public CoinColors ShooterActiveColor;

    public CoinColors DeckSideAColor;
    public CoinColors DeckSideBColor;
    public CoinColors DeckSideActiveColor;



    public int CurrentScore = 0;
    public List<GameObject> currentPlayfieldPieces = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }


    void Start () {
		
	}
	
    /*
	void Update () {
		
	}
    */
}
