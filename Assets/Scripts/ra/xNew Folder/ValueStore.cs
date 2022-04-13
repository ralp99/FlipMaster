using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValueStore : MonoBehaviour {

//	[HideInInspector] public int incomingInt;
//	[HideInInspector] public float incomingFloat;


    public bool highScore;
    public int currentScore;
    public int beginHealth = 1;
    public int currentHealth;
    public int beginLives;
    public int currentLives;
    public float scrollSpeed;
	public int spawnNumber;

    //  public float controllerMultiply;
    public GameObject[] lifeOverObject;

    public GameObject[] gameOverObject;

	public List<GameObject> spawnedList;

    /*
     // obsolete!
	public string shooterSideAcolor = "";
	public string shooterSideBcolor = "";
	public string shooterFacingColor = "";
    */


    public CoinColors shooterSideAColor;
    public CoinColors shooterSideBColor;
    public CoinColors shooterActiveColor;



    void Start()

    {
        currentLives = beginLives;
        currentHealth = beginHealth;
    }

    void Update()
    {

        if (currentHealth <= 0)
        {
            currentLives = currentLives - 1;
            currentHealth = beginHealth;
            foreach (GameObject picked in lifeOverObject)
            {
                picked.SetActive(true);
            }

        }

        if (currentLives <= 0)
            if (gameOverObject != null)
                foreach (GameObject picked in gameOverObject)
                {
                    picked.SetActive(true);
                }
    }




    }
