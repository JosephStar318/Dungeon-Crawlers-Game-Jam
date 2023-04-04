using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public static event Action OnBowDraw;
    public static event Action OnArrowShoot;

    private const string DRAWING = "isDrawing";

    [SerializeField] private Transform bow;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private Transform attachedArrow;
    [SerializeField] private float arrowForce;

    private Animator anim;
    private bool isDrawing;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerInputHelper.OnDrawing += PlayerInputHelper_OnDrawing;
        PlayerInputHelper.OnAttack += PlayerInputHelper_OnAttack;
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnDrawing -= PlayerInputHelper_OnDrawing;
        PlayerInputHelper.OnAttack -= PlayerInputHelper_OnAttack;
    }

    private void PlayerInputHelper_OnDrawing(bool isDrawing)
    {
        anim.SetBool(DRAWING, isDrawing);
        this.isDrawing = isDrawing;
        if (isDrawing)
        {
            OnBowDraw?.Invoke();
        }
    }

    private void LateUpdate()
    {
        ChangePosition();
        ChangeRotation();
    }

    private void ChangeRotation()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(CustomCursor.PositionPerspective);

        transform.LookAt(pos);
    }

    private void ChangePosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(CustomCursor.PositionPerspective);
        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.InverseTransformPoint(pos), Time.deltaTime * 5f);
      
    }
    private void FireArrow()
    {
        if(attachedArrow != null)
        {
            attachedArrow.GetComponent<Rigidbody>().isKinematic = false;
            attachedArrow.GetComponentInChildren<Collider>().enabled = true;
            attachedArrow.GetComponent<Rigidbody>().AddForce(attachedArrow.forward * arrowForce, ForceMode.Impulse);
            Destroy(attachedArrow.gameObject, 4f);
            Invoke(nameof(CreateNewArrow), 0.5f);
            OnArrowShoot?.Invoke();
        }
    }

    private void CreateNewArrow()
    {
        attachedArrow = Instantiate(arrowPrefab, arrowHolder).transform;
    }

    private void PlayerInputHelper_OnAttack()
    {
        if (isDrawing)
        {
            FireArrow();
            isDrawing = false;
            anim.SetBool(DRAWING, isDrawing);
        }
    }
}
