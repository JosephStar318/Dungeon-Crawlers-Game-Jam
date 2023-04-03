using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public event Action OnIdleState;

    private float _idlePeriod;
    private float _startTime;
    public bool IdleFinished { get; private set; }

    public IdleState(float idlePeriod)
    {
        _idlePeriod = idlePeriod;
    }

    public void OnEnter()
    {
        _startTime = Time.time;
        IdleFinished = false;
        OnIdleState?.Invoke();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if(Time.time - _startTime > _idlePeriod && IdleFinished == false)
        {
            IdleFinished = true;
        }
    }
}
