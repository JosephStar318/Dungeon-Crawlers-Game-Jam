using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject axe;

    private void OnEnable()
    {
        PlayerInputHelper.OnSwap += PlayerInputHelper_OnSwap;
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnSwap -= PlayerInputHelper_OnSwap;
    }

    private void PlayerInputHelper_OnSwap()
    {
        bow.SetActive(!bow.activeSelf);
        axe.SetActive(!axe.activeSelf);
    }
}
