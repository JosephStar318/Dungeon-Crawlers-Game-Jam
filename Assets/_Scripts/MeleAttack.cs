using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public static event Action OnMeleAttackSwing;
    public static event Action OnMeleAttackHit;

    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;

    private void OnEnable()
    {
        PlayerInputHelper.OnAttack += PlayerInputHelper_OnAttack;
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnAttack -= PlayerInputHelper_OnAttack;
    }
    private void PlayerInputHelper_OnAttack()
    {
        Vector3 rayPos = Camera.main.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(CustomCursor.Position);
        OnMeleAttackSwing?.Invoke();
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, attackableLayers))
        {
            if(hit.transform.TryGetComponent(out Health healt))
            {
                healt.TakeDamage(attackDamage, ray.direction.normalized);
                OnMeleAttackHit?.Invoke();
            }
        }
    }
}
