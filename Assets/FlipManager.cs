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

    [HideInInspector]
    public AlleyPress lastAlleyPress;

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
            ColumnsList[i].CoinColumn.Insert(0, newCoin);
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


        // TODO - properly relocate SHOOTING COIN to beginning of ALLEY
        /*
        //  shootingCoinTransform.position = activeAlley.transform.
        Mesh mesh = AlleyObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        shootingCoinTransform.position = vertices[0];
        */
        lastAlleyPress = alleyPress;
        shootingCoinTransform.gameObject.SetActive(true);
        shootingCoinTransform.position = alleyPress.LaunchPoint.position;

        //   InstantiateShotCoinAtColumn(alleyPress);

    }


    // happens on coinHit
    public void InstantiateShotCoinAtColumn()
    {


        GameObject newCoin = InstantiatedCoin();
        MyCoinIdentity newCoinIdentity = newCoin.GetComponent<MyCoinIdentity>();
        newCoinIdentity.isShotCoin = true;

        TransferColorValues(shootingCoinIdentity, newCoinIdentity);
        ColumnSo thisColObject = Dict_Alley_Columns[lastAlleyPress];
        newCoinIdentity.MyColumn = System.Array.IndexOf(Alleys, lastAlleyPress.gameObject);

        lastAlleyPress = null;

        Transform borderCoin = thisColObject.CoinColumn[thisColObject.CoinColumn.Count - 1].transform;

        thisColObject.CoinColumn.Add(newCoin);

        Vector3 newCoinPosition = new Vector3(borderCoin.position.x, borderCoin.position.y - VcoinPadding,
            borderCoin.position.z);

        newCoin.transform.position = newCoinPosition;

        CheckAllCoinsMatching();

    }


    void CheckColorMatches(MyCoinIdentity coinA, MyCoinIdentity coinB)
    {
        Material checkColorA = null;
        Material checkColorB = null;

        if (coinA.BackActive)
        {
            checkColorA = coinA.BackColor;
        }
        else
        {
            checkColorA = coinA.FrontColor;
        }

        if (coinB.BackActive)
        {
            checkColorB = coinB.BackColor;
        }
        else
        {
            checkColorB = coinB.FrontColor;
        }

        if (checkColorA == checkColorB)
        {
            SetMatchingStatus(coinA, true);
            SetMatchingStatus(coinB, true);
            AddToMatchingGroup(coinA, coinB, true);
        }
    }

    void AddToMatchingGroup(MyCoinIdentity groupOwner, MyCoinIdentity groupMember, bool bothSides)
    {
      
        MatchingGroupCheck(groupOwner, groupMember);

        /*
        for (int i = 0; i < groupMember.MatchingGroup.Count; i++)
        {
            MyCoinIdentity subMember = groupMember.MatchingGroup[i].GetComponent<MyCoinIdentity>();
            // MatchingGroupCheck(groupOwner, subMember);
            AddToMatchingGroup(groupOwner, subMember, true);
        }
        */

        if (bothSides)
        {
            AddToMatchingGroup(groupMember, groupOwner, false);
        }

    }

    void MatchingGroupCheck(MyCoinIdentity groupOwner, MyCoinIdentity groupMember)
    {
        if (!groupOwner.MatchingGroup.Contains(groupMember.gameObject))
        {
            groupOwner.MatchingGroup.Add(groupMember.gameObject);
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



    public void CheckAllCoinsMatching()
    {

        // clear all associations
        for (int i = 0; i < currentPlacedCoins.Count; i++)
        {
            MyCoinIdentity currentCoinIdentity = currentPlacedCoins[i].GetComponent<MyCoinIdentity>();

            SetMatchingStatus(currentCoinIdentity, false);
            currentCoinIdentity.MatchingGroup.Clear();
        }


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

                // checking vertically

                if (j > 0)
                {
                    previousVertCoinIdentity = currentColumnSo.CoinColumn[j-1].GetComponent<MyCoinIdentity>();
                    CheckColorMatches(previousVertCoinIdentity, currentCoinIdentity);
                }

                if (j < coinCount-1)
                {
                    nextVertCoinIdentity = currentColumnSo.CoinColumn[j + 1].GetComponent<MyCoinIdentity>();
                    CheckColorMatches(nextVertCoinIdentity, currentCoinIdentity);
                }

                // checking horizontally
                int currentColumnsCount = ColumnsList[i].CoinColumn.Count;

                if (currentCoinIdentity.MyColumn < ColumnsList.Count-1)
                {
                    //   nextHorizCoinIdentity
                    ColumnSo nextColumn = ColumnsList[currentCoinIdentity.MyColumn + 1];


                      if (nextColumn.CoinColumn.Count >= currentColumnsCount)
                        {
                            if (nextColumn.CoinColumn[j] != null)
                            {
                                nextHorizCoinIdentity = nextColumn.CoinColumn[j].GetComponent<MyCoinIdentity>();
                                CheckColorMatches(nextHorizCoinIdentity, currentCoinIdentity);
                            }
                        }
            
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
                            CheckColorMatches(previousHorizCoinIdentity, currentCoinIdentity);
                        }
                    }
                }



            }  // end J


        }

        MergeAllMatchingGroups();

       

    }

    void MergeAllMatchingGroups()
    {
        // merge all matching groups to each member

        for (int i = 0; i < currentPlacedCoins.Count; i++)  // start per-coin
        {
            List<GameObject> myGroupMembers = new List<GameObject>();

            MyCoinIdentity currentCoin = currentPlacedCoins[i].GetComponent<MyCoinIdentity>();

            for (int j = 0; j < currentCoin.MatchingGroup.Count; j++)
            {
                MyCoinIdentity subMember = currentCoin.MatchingGroup[j].GetComponent<MyCoinIdentity>();

                if (!myGroupMembers.Contains(subMember.gameObject))
                {
                    myGroupMembers.Add(subMember.gameObject);
                }
            }

            for (int k = 0; k < myGroupMembers.Count; k++)
            {
                MyCoinIdentity groupMember = myGroupMembers[k].GetComponent<MyCoinIdentity>();
                for (int l = 0; l < myGroupMembers.Count; l++)
                {
                    GameObject currentAddingCoin = myGroupMembers[l];

                    if (!groupMember.MatchingGroup.Contains(currentAddingCoin))
                    {
                        groupMember.MatchingGroup.Add(currentAddingCoin);
                    }
                }
            }
        }  // end each individual coin
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
                         
