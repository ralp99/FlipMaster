using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoinIdentity : MonoBehaviour
{
    FlipManager flipManager;

    public Animator Animator;
    public GameObject Frontside;
    public GameObject Backside;

    public bool BackActive;
    public bool Matching;
    public Material FrontColor;
    public Material BackColor;
    private Material newFrontColor;
    private Material newBackColor;

    public TextMesh TextMesh;

    public int MyColumn;
    public int MyRow;

    [HideInInspector]
    public bool isShotCoin;

    public void TouchEvent_DoFlipAnimation()
    {
        string performFlip = "flipA_B_ccw";

        if (BackActive)
        {
             performFlip = "flipB_A_cw";
        }

        BackActive = !BackActive;
        Animator.SetTrigger(performFlip);
        flipManager.CheckAllCoinsMatching();

    }

    public IEnumerator InitializeColors(bool forceInit = false)
    {

        int newFrontMatInt = Random.Range(0, flipManager.ColorMaterials.Length);
        int newBackMatInt = Random.Range(0, flipManager.ColorMaterials.Length);

        if (!FrontColor || forceInit)
        {
            while (newFrontMatInt == newBackMatInt)
            {
                newBackMatInt = Random.Range(0, flipManager.ColorMaterials.Length);
                yield return null;
            }

            FrontColor = flipManager.ColorMaterials[newFrontMatInt];
            BackColor = flipManager.ColorMaterials[newBackMatInt];
        }

        Frontside.GetComponent<Renderer>().material = FrontColor;
        Backside.GetComponent<Renderer>().material = BackColor;
    }

    void Start()
    {
       flipManager = FlipManager.Instance;
       StartCoroutine(InitializeColors());
    }

    private void OnDisable()
    {
        FrontColor = null;
        BackColor = null;
        Matching = false;
    }

    public void AssignMaterials(Material frontMatAssign, Material backMatAssign)
    {
        FrontColor = frontMatAssign;
        Frontside.GetComponent<Renderer>().material = frontMatAssign;

        BackColor = backMatAssign;
        Backside.GetComponent<Renderer>().material = backMatAssign;
    }



}
