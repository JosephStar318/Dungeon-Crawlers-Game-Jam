using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimControl : MonoBehaviour
{
    public event Action OnAttack;
    private const string SPEED_FLOAT = "Speed";
    private const string ATTACK = "Monster_Attack";
    private const string DIE = "Monster_Die";

    private Animator anim;
    private Monster monster;

    private float speed = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        monster = GetComponentInParent<Monster>();

        monster.GetAttackPlayerState().OnAttackState += AttackPlayerState_OnAttackState;
        monster.GetIdleState().OnIdleState += IdleState_OnIdleState;
        monster.GetFollowPlayerState().OnFollowState += MonsterAnimControl_OnFollowState;
        monster.OnDieState += Monster_OnDieState;
    }
    private void OnDestroy()
    {
        monster.GetAttackPlayerState().OnAttackState -= AttackPlayerState_OnAttackState;
        monster.GetIdleState().OnIdleState -= IdleState_OnIdleState;
        monster.GetFollowPlayerState().OnFollowState -= MonsterAnimControl_OnFollowState;
        monster.OnDieState -= Monster_OnDieState;
    }

    public void Attack()
    {
        OnAttack?.Invoke();
    }
    private void Monster_OnDieState()
    {
        PlayMonsterDieAnimation();
    }

    private void MonsterAnimControl_OnFollowState()
    {
        ChangeMonsterSpeed(1f);
    }

    private void IdleState_OnIdleState()
    {
        ChangeMonsterSpeed(0f);
    }

    private void AttackPlayerState_OnAttackState()
    {
        PlayMonsterAttackAnim();
    }

    private void ChangeMonsterSpeed(float val)
    {
        speed = val;
        anim.SetFloat(SPEED_FLOAT, speed);
    }

    private void PlayMonsterAttackAnim()
    {
        int randomIndex = UnityEngine.Random.Range(0, 3);
        anim.CrossFade($"{ATTACK}{randomIndex}", 0.1f);
    }

    private void PlayMonsterDieAnimation()
    {
        anim.CrossFade(DIE, 0.1f);
    }
}
