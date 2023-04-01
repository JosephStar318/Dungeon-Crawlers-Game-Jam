using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;

    private const string ATTACK = "Weapon_Attack";

    private int animIndex = 0;
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
        animIndex = UnityEngine.Random.Range(0, 3);
        anim.CrossFade(ATTACK + animIndex.ToString(), 0.1f);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

}
