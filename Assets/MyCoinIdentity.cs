using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoinIdentity : MonoBehaviour
{
    FlipManager flipManager;

    public Animator Animator;
    bool isBackfacing;

    public void DoFlipAnimation()
    {
        string performFlip = "flipA_B_ccw";

        if (isBackfacing)
        {
             performFlip = "flipB_A_cw";
        }

        isBackfacing = !isBackfacing;
        Animator.SetTrigger(performFlip);
    }

    void Start()
    {
        flipManager = FlipManager.Instance;
    }

    void Update()
    {
        
    }
}
