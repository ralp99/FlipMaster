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
    public Transform CoinSlider;



    public GameObject[] Alleys;

    public int ColumnQuantity = 6;
    public float HcoinPadding = 0.1f;
    public float VcoinPadding = 0.1f;

    float currentVlevel = 0.0f;

    public ColumnSo ColumnSource;

    public List<ColumnSo> ColumnsList;

    public List<GameObject> currentPlacedCoins = new List<GameObject>();

    public Dictionary<AlleyPress, ColumnSo> Dict_Alley_Columns = new Dictionary<AlleyPress, ColumnSo>();



    private void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        GameSessionStart();
    }
   

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
            newCoin.transform.SetParent(CoinSlider);
            ColumnsList[i].CoinColumn.Add(newCoin);
            currentPlacedCoins.Add(newCoin);
            newCoin.GetComponent<MyCoinIdentity>().MyColumn = i;
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

    void InstantiateShotCoinAtColumn(AlleyPress alley)
    {
        GameObject newCoin = Instantiate(CoinSource) as GameObject;
        newCoin.transform.SetParent(CoinSlider);
        currentPlacedCoins.Add(newCoin);
        MyCoinIdentity newCoinIdentity = newCoin.GetComponent<MyCoinIdentity>();
        newCoinIdentity.isShotCoin = true;

        TransferColorValues(shootingCoinIdentity, newCoinIdentity);
        ColumnSo thisColObject = Dict_Alley_Columns[alley];
        newCoinIdentity.MyColumn = System.Array.IndexOf(Alleys, alley.gameObject);

        bool lastShotCoin = false;
        Transform borderCoin = null;

        if (thisColObject.CoinColumn[thisColObject.CoinColumn.Count - 1].GetComponent<MyCoinIdentity>().isShotCoin)
        {
            lastShotCoin = true;
        }

        if (lastShotCoin)
        {
            borderCoin = thisColObject.CoinColumn[thisColObject.CoinColumn.Count - 1].transform;
        }
        else
        {
            borderCoin = thisColObject.CoinColumn[0].transform;
        }

        thisColObject.CoinColumn.Add(newCoin);

        Vector3 newCoinPosition = new Vector3(borderCoin.position.x, borderCoin.position.y - VcoinPadding,
            borderCoin.position.z);

        newCoin.transform.position = newCoinPosition;

        CheckIfArmed();

    }

    private void CheckIfArmed()
    {
    


        for (int i = 0; i < ColumnsList.Count; i++)
        {
            ColumnSo currentColumnSo = ColumnsList[i];

            int coinCount = currentColumnSo.CoinColumn.Count;

            for (int j = 0; j < coinCount; j++)
            {
                MyCoinIdentity currentCoinIdentity =
                    currentColumnSo.CoinColumn[j].GetComponent<MyCoinIdentity>();

                MyCoinIdentity previousVertCoinIdentity = null;
                MyCoinIdentity nextVertCoinIdentity = null;

                MyCoinIdentity previousHorizCoinIdentity = null;
                MyCoinIdentity nextHorizCoinIdentity = null;


                currentCoinIdentity.Armed = false;

                // checking vertically

                if (j > 0)
                {
                    previousVertCoinIdentity = currentColumnSo.CoinColumn[j-1].GetComponent<MyCoinIdentity>();
                    if (previousVertCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                    {
                        previousVertCoinIdentity.Armed = true;
                        currentCoinIdentity.Armed = true;
                    }
                }

                if (j < coinCount-1)
                {
                    nextCoinIdentity = currentColumnSo.CoinColumn[j + 1].GetComponent<MyCoinIdentity>();
                    if (nextVertCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                    {
                        nextVertCoinIdentity.Armed = true;
                        currentCoinIdentity.Armed = true;
                    }
                }

                // checking horizontally

                if (currentCoinIdentity.MyColumn < ColumnsList.Count)
                {
               //     nextHorizCoinIdentity
                }



            }


        }



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
                         
