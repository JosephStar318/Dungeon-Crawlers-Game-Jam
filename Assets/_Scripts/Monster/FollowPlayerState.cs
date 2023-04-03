using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerState : IState
{
    public event Action OnFollowState;

    private NavMeshAgent _agent;
    private Transform _followedTransform;

    public FollowPlayerState(NavMeshAgent agent, Transform followedTransform)
    {
        _agent = agent;
        _followedTransform = followedTransform;
    }

    public void OnEnter()
    {
        _agent.isStopped = false;
        OnFollowState?.Invoke();
    }

    public void OnExit()
    {
        _agent.isStopped = true;
    }

    public void Update()
    {
        _agent.SetDestination(_followedTransform.position);
    }
}
