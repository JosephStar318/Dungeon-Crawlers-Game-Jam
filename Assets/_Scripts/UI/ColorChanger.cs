using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile standard;
    [SerializeField] private VolumeProfile bright;

    private bool isBright = false;

    private void OnEnable()
    {
        Player.OnPortalEntered += Player_OnPortalEntered;
    }
    private void OnDisable()
    {
        Player.OnPortalEntered -= Player_OnPortalEntered;
    }
    private void Player_OnPortalEntered()
    {
        isBright = !isBright;
        if(isBright == true)
        {
            volume.profile = bright;
        }
        else
        {
            volume.profile = standard;
        }
    }

}
