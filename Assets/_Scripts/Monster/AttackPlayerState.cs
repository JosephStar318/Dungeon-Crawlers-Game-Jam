using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerState : IState
{
    public event Action OnAttackState;

    public void OnEnter()
    {
        OnAttackState?.Invoke();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}
