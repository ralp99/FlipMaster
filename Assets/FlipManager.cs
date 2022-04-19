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

    public Dictionary<AlleyPress, ColumnSo> Dict_Alley_Columns = new Dictionary<AlleyPress, ColumnSo>();



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
            Dict_Alley_Columns.Add(Alleys[i].GetComponent<AlleyPress>(), newColumnSo);
        }


        nextCoinIdentity = NextCoin.GetComponent<MyCoinIdentity>();
        onDeckCoinIdentity = PreviewCoin.GetComponent<MyCoinIdentity>();
        shootingCoinIdentity = shootingCoinTransform.GetComponent<MyCoinIdentity>();
        shootingCoinTransform.gameObject.SetActive(false);


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


    void InstantiateShotCoinAtColumn(AlleyPress alley)
    {
        GameObject newCoin = Instantiate(CoinSource) as GameObject;
        TransferColorValues(shootingCoinIdentity, newCoin.GetComponent<MyCoinIdentity>());
        ColumnSo thisColObject = Dict_Alley_Columns[alley];
         Transform borderCoin = thisColObject.CoinColumn[thisColObject.CoinColumn.Count - 1].transform;
        // Transform borderCoin = thisColObject.CoinColumn[0].transform;


        thisColObject.CoinColumn.Add(newCoin);

        Vector3 newCoinPosition = new Vector3(borderCoin.position.x, borderCoin.position.y - VcoinPadding,
            borderCoin.position.z);

        newCoin.transform.position = newCoinPosition;

        // shows up in correct list, but wrong phys location
        // should offset below last piece in that list


    }



    public void ClickEvents_ShootPiece(AlleyPress alleyPress)
    {
        CoinSideUpdates();

        /*
        //  shootingCoinTransform.position = activeAlley.transform.
        Mesh mesh = AlleyObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        shootingCoinTransform.position = vertices[0];
        */
        shootingCoinTransform.gameObject.SetActive(true);
        shootingCoinTransform.position = alleyPress.LaunchPoint.position;

        InstantiateShotCoinAtColumn(alleyPress);


    }

    private void CoinSideUpdates()
    {
        TransferColorValues(nextCoinIdentity, shootingCoinIdentity);
        TransferColorValues(onDeckCoinIdentity, nextCoinIdentity);
        StartCoroutine(onDeckCoinIdentity.InitializeColors(true));
    }


    private void TransferColorValues(MyCoinIdentity sourceCoin, MyCoinIdentity goalCoin)
    {

        Material frontMatSend = null;
        Material backMatSend = null;

        if (sourceCoin.BackActive)
        {
            frontMatSend = sourceCoin.BackColor;
            backMatSend = sourceCoin.FrontColor;
        }
        else
        {
            frontMatSend = sourceCoin.FrontColor;
            backMatSend = sourceCoin.BackColor;
        }


        if (goalCoin.BackActive)
        {
            goalCoin.AssignMaterials(backMatSend, frontMatSend);

        }
        else
        {
            goalCoin.AssignMaterials(frontMatSend, backMatSend);
        }

    }



}
                         
