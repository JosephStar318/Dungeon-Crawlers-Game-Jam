using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IHittable
{
    public event Action OnDieState;
    public static event Action OnAnyMonsterKilled;

    private StateMachine stateMachine;
    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Health health;
    private Transform player;
    private PlayerDetector playerDetector;
    private Transform detectedTransform;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float idlePeriod;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private ParticleRefsSO particleRefsSO;
    [SerializeField] private MonsterAnimControl animControl;
    [SerializeField] private AudioSource audioSource;

    private FollowPlayerState followPlayerState;
    private AttackPlayerState attackPlayerState;
    private IdleState idleState;
    private DieState dieState;

    private void Awake()
    {
        stateMachine = new StateMachine();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        playerDetector = GetComponent<PlayerDetector>();

        player = GameObject.FindObjectOfType<Player>().transform;

        followPlayerState = new FollowPlayerState(agent, player);
        attackPlayerState = new AttackPlayerState();
        idleState = new IdleState(this, player, idlePeriod);
        dieState = new DieState();

        stateMachine.AddTransition(followPlayerState, attackPlayerState, DetectedPlayer());
        stateMachine.AddTransition(attackPlayerState, idleState, AttackFinished());
        stateMachine.AddTransition(idleState, followPlayerState, IsIdleFinished());

        stateMachine.AddAnyTransition(dieState, IsDead());

        stateMachine.SetState(followPlayerState);

        Func<bool> DetectedPlayer() => () => playerDetector.DetectPlayer(out detectedTransform);
        Func<bool> IsIdleFinished() => () => idleState.IdleFinished;
        Func<bool> AttackFinished() => () => true;
        Func<bool> IsDead() => () => health.IsDead;

    }
    private void Start()
    {
        health.OnDead += Health_OnDead;
        health.OnDamage += Health_OnDamage;
        animControl.OnAttack += AnimControl_OnAttack;

    }
    private void OnDestroy()
    {
        health.OnDead -= Health_OnDead;
        health.OnDamage += Health_OnDamage;
        animControl.OnAttack -= AnimControl_OnAttack;
    }

    private void Update()
    {
        stateMachine.Update();
    }
    private void AnimControl_OnAttack()
    {
        audioSource.Play();
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, attackRange, playerLayer))
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.TakeDamage(attackDamage, transform.forward);
            }
        }
    }

    private void Health_OnDead(Vector3 dir)
    {
        Instantiate(particleRefsSO.bloodSplashParticle.gameObject, transform.position, Quaternion.identity);
        OnDieState?.Invoke();
        OnAnyMonsterKilled?.Invoke();
        Destroy(gameObject, 4f);
    }
    private void Health_OnDamage(float health, Vector3 damageDir)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(damageDir, ForceMode.Impulse);

        Invoke(nameof(ResetSpeed), 1f);
    }
    private void ResetSpeed()
    {
        rb.velocity = Vector3.zero;
    }

    public FollowPlayerState GetFollowPlayerState()
    {
        return followPlayerState;
    }
    public AttackPlayerState GetAttackPlayerState()
    {
        return attackPlayerState;
    }
    public IdleState GetIdleState()
    {
        return idleState;
    }

    public void OnHit(Vector3 hitPoint)
    {
        Instantiate(particleRefsSO.bloodParticle.gameObject, hitPoint, Quaternion.LookRotation(-transform.forward));
    }
}
