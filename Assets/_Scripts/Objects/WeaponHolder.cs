using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    private Vector2 startPos;
    private Vector3 targetPos;

    [SerializeField] private float movespeed;
    private bool isAttacking = false;
    private float delta;

    private void OnEnable()
    {
        PlayerInputHelper.OnAttack += PlayerInputHelper_OnAttack;
    }

    private void Start()
    {
        startPos = transform.localPosition;
    }
    private void Update()
    {
        if (isAttacking)
        {
            transform.localPosition = Vector3.Slerp(startPos, targetPos, delta);
            if(delta > 1)
            {
                isAttacking = false;
                delta = 0;
            }
        }
        else
        {
            transform.localPosition = Vector3.Slerp(targetPos, startPos, delta);
        }
        delta += Time.deltaTime * movespeed;
    }

    private void PlayerInputHelper_OnAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        targetPos = transform.InverseTransformPoint(ray.GetPoint(0.01f));
        delta = 0;
        isAttacking = true;
    }
}
