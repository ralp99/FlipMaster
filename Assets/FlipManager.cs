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
    private MyCoinIdentity shootingCoinIdentity;
    private MyCoinIdentity nextCoinIdentity;
    private MyCoinIdentity onDeckCoinIdentity;
    public Transform shootingCoinTransform;



    public GameObject[] Alleys;

    public int ColumnQuantity = 6;
    public float HcoinPadding = 0.1f;
    public float VcoinPadding = 0.1f;

    float currentVlevel = 0.0f;

    public ColumnSo ColumnSource;

    public List<ColumnSo> ColumnsList;

    private List<GameObject> currentPlacedCoins = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        GameSessionStart();
    }

    /*
    void Update()
    {
        
    }
    */


    void GameSessionStart()

    {
        for (int i = 0; i < ColumnQuantity; i++)
        {
            ColumnSo newColumnSo = Instantiate(ColumnSource) as ColumnSo;
            ColumnsList.Add(newColumnSo);
        }

        nextCoinIdentity = NextCoin.GetComponent<MyCoinIdentity>();
        onDeckCoinIdentity = PreviewCoin.GetComponent<MyCoinIdentity>();
        shootingCoinIdentity = shootingCoinTransform.GetComponent<MyCoinIdentity>();


        PopulateCoinRow();
        PopulateCoinRow();
    }


    void PopulateCoinRow()
    {
        currentVlevel = currentVlevel + VcoinPadding;

        for (int i = 0; i < ColumnsList.Count; i++)
        {
            GameObject newCoin = Instantiate(CoinSource) as GameObject;
            ColumnsList[i].CoinColumn.Add(newCoin);
            currentPlacedCoins.Add(newCoin);
            PlaceNewCoinInField(i == 0);
        }
    }

    void PlaceNewCoinInField(bool resetH)
    {

        Transform currentCoin = currentPlacedCoins[currentPlacedCoins.Count - 1].transform;
        float nextCoinHpos = 0.0f;

        if (currentPlacedCoins.Count > 1 && !resetH)
        {
            Transform previousCoin = currentPlacedCoins[currentPlacedCoins.Count - 2].transform;
            nextCoinHpos = previousCoin.transform.localPosition.x + HcoinPadding;
        }

        Vector3 newCoinPos = new Vector3(nextCoinHpos, currentVlevel, 0);
        currentCoin.localPosition = newCoinPos;
    }

    public void ClickEvents_AddRow()
    {
        PopulateCoinRow();
    }




    public void ClickEvents_ShootPiece(int alleyNumber)
    {
        GameObject activeAlley = Alleys[alleyNumber];


        TransferColorValues(nextCoinIdentity, shootingCoinIdentity);
        TransferColorValues(onDeckCoinIdentity, nextCoinIdentity);
        StartCoroutine(onDeckCoinIdentity.InitializeColors(true));





    }


    private void TransferColorValues(MyCoinIdentity sourceCoin, MyCoinIdentity goalCoin)
    {

        goalCoin.AssignMaterials(sourceCoin.FrontColor, sourceCoin.BackColor);



    }



}
                         
