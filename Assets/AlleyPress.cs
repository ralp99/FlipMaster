using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyPress : MonoBehaviour
{

    FlipManager flipManager;
    Renderer renderer;
    public Transform LaunchPoint;

    void Start()
    {
        flipManager = FlipManager.Instance;
        renderer = GetComponent<Renderer>();
        AlleyVisibility(flipManager.ShowAlleysAtStart);
    }

    public void ClickEvents_TouchAlley()
    {
        if (!flipManager.lastAlleyPress)
        {
            AlleyVisibility(true);
        }
    }

    public void ClickEvents_ReleaseTouch()
    {
        if (!flipManager.lastAlleyPress)
        {
            flipManager.ClickEvents_ShootPiece(this);
            AlleyVisibility(false);
        }
    }

    void AlleyVisibility(bool active)
    {
        renderer.enabled = active;
    }

}
