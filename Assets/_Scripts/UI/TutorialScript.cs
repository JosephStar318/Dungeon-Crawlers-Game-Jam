using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;

    private void OnEnable()
    {
        PlayerInputHelper.OnInterractPressed += PlayerInputHelper_OnInterractPressed;
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnInterractPressed -= PlayerInputHelper_OnInterractPressed;
    }

    private void PlayerInputHelper_OnInterractPressed()
    {
        StartCoroutine(cg.FadeOut(1f, () => gameObject.SetActive(false)));
    }
}
