using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipManager : MonoBehaviour
{
    public Material[] ColorMaterials;

    public static FlipManager Instance;

    public GameObject CoinSource;
    public GameObject NextCoin;
    public GameObject PreviewCoin;

    public int ColumnQuantity = 6;
    public float HcoinPadding = 0.1f;
    public float VcoinPadding = 0.1f;

    float currentVlevel = 0.0f;

    public ColumnSo ColumnSource;

    public List<ColumnSo> ColumnsList;

   // private GameObject lastPlacedCoin;
    private List<GameObject> currentPlacedCoins = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        GameSessionStart();
    }


    void Update()
    {
        
    }


    void GameSessionStart()

    {
        for (int i = 0; i < ColumnQuantity; i++)
        {
            ColumnSo newColumnSo = Instantiate(ColumnSource) as ColumnSo;
            ColumnsList.Add(newColumnSo);
        }

        PopulateCoinRow();
    //    PopulateCoinRow();


    }


    void PopulateCoinRow()
    {
        currentVlevel = currentVlevel + VcoinPadding;

        for (int i = 0; i < ColumnsList.Count; i++)
        {
            GameObject newCoin = Instantiate(CoinSource) as GameObject;
            ColumnsList[i].CoinColumn.Add(newCoin);
            currentPlacedCoins.Add(newCoin);
            PlaceNewCoinInField();
        }
    }

    void PlaceNewCoinInField()
    {
        if (currentPlacedCoins.Count < 2)
        {
            return;
        }

        Transform currentCoin = currentPlacedCoins[currentPlacedCoins.Count-1].transform;
        Transform previousCoin = currentPlacedCoins[currentPlacedCoins.Count - 2].transform;


        /*
        if (currentPlacedCoins.Count > ColumnQuantity)
        {
            //  ColumnQuantity += VcoinPadding;
            currentVlevel = currentVlevel + VcoinPadding;
            currentPlacedCoins.Clear();
        }
        */

        float lastCoinHpos = previousCoin.transform.localPosition.x;
        // Vector3 newCoinPos = new Vector3(lastCoinHpos+HcoinPadding, 0, 0);
        Vector3 newCoinPos = new Vector3(lastCoinHpos + HcoinPadding, currentVlevel, 0);


        currentCoin.localPosition = newCoinPos;
    }

}
                         
