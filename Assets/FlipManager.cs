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

    [Header("Debug")]
    public bool ShowAlleysAtStart = false;
    public Color MatchingTextColor = new Color();
    public Color LonelyTextColor = new Color();



    int namedCoinCounter;
    int namedColumnListCounter;

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
            newColumnSo.name = newColumnSo.name + namedColumnListCounter;
            namedColumnListCounter++;
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

    GameObject InstantiatedCoin()
    {
        // check in pool 
        GameObject newCoin = Instantiate(CoinSource) as GameObject;
        newCoin.name = newCoin.name + namedCoinCounter;
        newCoin.GetComponent<MyCoinIdentity>().TextMesh.text = namedCoinCounter.ToString();
        namedCoinCounter++;
        newCoin.transform.SetParent(CoinSlider);
        currentPlacedCoins.Add(newCoin);
        return newCoin;
    }



    void PopulateCoinRow()
    {
        currentVlevel = currentVlevel + VcoinPadding;

        for (int i = 0; i < ColumnsList.Count; i++)
        {
            GameObject newCoin = InstantiatedCoin();
            ColumnsList[i].CoinColumn.Add(newCoin);
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
        GameObject newCoin = InstantiatedCoin();
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

        CheckIfMatching();

    }

    public void CheckIfMatching()
    {

        int currentTestCounter = 0;

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

                SetMatchingStatus(currentCoinIdentity, false);

                // checking vertically

                if (j > 0)
                {
                    previousVertCoinIdentity = currentColumnSo.CoinColumn[j-1].GetComponent<MyCoinIdentity>();
                    if (previousVertCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                    {
                        SetMatchingStatus(previousVertCoinIdentity, true);
                        SetMatchingStatus(currentCoinIdentity, true);
                    }
                }

                if (j < coinCount-1)
                {
                    nextVertCoinIdentity = currentColumnSo.CoinColumn[j + 1].GetComponent<MyCoinIdentity>();
                    if (nextVertCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                    {
                        SetMatchingStatus(nextVertCoinIdentity, true);
                        SetMatchingStatus(currentCoinIdentity, true);
                    }
                }

                // checking horizontally
                currentTestCounter++;  //crash at 5..7?
                int currentColumnsCount = ColumnsList[i].CoinColumn.Count;

                if (currentCoinIdentity.MyColumn < ColumnsList.Count-1)
                {
                    //   nextHorizCoinIdentity
                    ColumnSo nextColumn = ColumnsList[currentCoinIdentity.MyColumn + 1];


                    // find current coin index in existing column
                    //      int currentCoinIndex = System.Array.IndexOf(ColumnsList[currentCoinIdentity.MyColumn], currentCoinIdentity.gameObject);


                    // j is position

                    //  if (nextColumn.CoinColumn[j] != null)

                    //    if (nextColumn.CoinColumn.Count < j)
                    //  if (nextColumn.CoinColumn.Count >= j)
                      if (nextColumn.CoinColumn.Count >= currentColumnsCount)
                        {
                            if (nextColumn.CoinColumn[j] != null)
                            {
                                nextHorizCoinIdentity = nextColumn.CoinColumn[j].GetComponent<MyCoinIdentity>();
                                if (nextHorizCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                                {
                                    SetMatchingStatus(nextHorizCoinIdentity, true);
                                    SetMatchingStatus(currentCoinIdentity, true);
                                }
                            }
                        }
                    /*

                    int currentCoinIndex = 0;

                    for (int k = 0; k < ColumnsList[currentCoinIdentity.MyColumn].CoinColumn.Count; k++)
                    {
                     //   if ()
                    }

                    */

                    // check if a coin is there, or if it is null

                    //  newCoinIdentity.MyColumn = System.Array.IndexOf(Alleys, alley.gameObject);
                }   // end check next

                // check previous

                if (currentCoinIdentity.MyColumn > 0)
                {
                    ColumnSo prevColumn = ColumnsList[currentCoinIdentity.MyColumn - 1];
                    if (prevColumn.CoinColumn.Count >= currentColumnsCount)
                    {
                        if (prevColumn.CoinColumn[j] != null)
                        {
                            previousHorizCoinIdentity = prevColumn.CoinColumn[j].GetComponent<MyCoinIdentity>();
                            if (previousHorizCoinIdentity.FrontColor == currentCoinIdentity.FrontColor)
                            {
                                SetMatchingStatus(previousHorizCoinIdentity, true);
                                SetMatchingStatus(currentCoinIdentity, true);
                            }
                        }

                    }





                }










            }  // end J


        }



    }

    void SetMatchingStatus(MyCoinIdentity coinID, bool isMatching)
    {
        Color useColor = LonelyTextColor;
        if (isMatching)
        {
            useColor = MatchingTextColor;
        }
        coinID.TextMesh.color = useColor;
        coinID.Matching = isMatching;
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
                         
