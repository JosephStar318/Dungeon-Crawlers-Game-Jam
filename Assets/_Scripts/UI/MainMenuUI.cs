using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button levelSelectionBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private LevelSelectionUI levelSelectionUI;

    private CanvasGroup cg;
    private bool isRotating;

    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float rotationSpeed;
    
    private float rotationCurveIndex;
    private Quaternion targetRotation;
    private Quaternion startRotation;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        levelSelectionBtn.onClick.AddListener(LevelSelectBtnEvent);
        quitBtn.onClick.AddListener(() => GameManager.QuitGame());
        Cursor.visible = true;
    }
    private void OnDestroy()
    {
        levelSelectionBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
    }
    
    private void LevelSelectBtnEvent()
    {
        StartRotate(Camera.main.transform.right);
        StartCoroutine(cg.FadeOut(0.1f));
    }

    public void StartRotate(Vector3 dir)
    {
        isRotating = true;
        startRotation = Camera.main.transform.rotation;
        targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        rotationCurveIndex = 0;
        PlayerInputHelper.Instance.DisableActions();
    }
    private void RotationFinished()
    {
        if(cg.alpha == 0)
            Hide();
        PlayerInputHelper.Instance.EnableUIActions();
    }
    private void Hide()
    {
        levelSelectionUI.Show(Show);
        gameObject.SetActive(false);
    }
    private void Show()
    {
        StartRotate(-Camera.main.transform.right);
        gameObject.SetActive(true);
        StartCoroutine(cg.FadeIn(1f));
    }
    private void Update()
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        if (isRotating)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationCurve.Evaluate(rotationCurveIndex));
            rotationCurveIndex += rotationSpeed * Time.deltaTime;
            if (rotationCurveIndex > 1)
            {
                Camera.main.transform.rotation = targetRotation;
                isRotating = false;
                RotationFinished();
            }
        }
    }

}
