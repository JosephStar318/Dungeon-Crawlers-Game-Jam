using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        Player.OnPlayerMoved += Player_OnPlayerMoved;
        Portal.OnPortalInterract += Portal_OnPortalInterract;
        MeleAttack.OnMeleAttackSwing += MeleAttack_OnMeleAttackSwing;
        MeleAttack.OnMeleAttackHit += MeleAttack_OnMeleAttackHit;
        Arrow.OnArrowHit += MeleAttack_OnMeleAttackHit;
        Bow.OnBowDraw += Bow_OnBowDraw;
        Bow.OnArrowShoot += Bow_OnArrowShoot;
        health.OnDamage += Health_OnDamage;
    }
    private void OnDisable()
    {
        Player.OnPlayerMoved -= Player_OnPlayerMoved;
        Portal.OnPortalInterract -= Portal_OnPortalInterract;
        MeleAttack.OnMeleAttackSwing -= MeleAttack_OnMeleAttackSwing;
        MeleAttack.OnMeleAttackHit -= MeleAttack_OnMeleAttackHit;
        Arrow.OnArrowHit -= MeleAttack_OnMeleAttackHit;
        Bow.OnBowDraw -= Bow_OnBowDraw;
        Bow.OnArrowShoot -= Bow_OnArrowShoot;
        health.OnDamage -= Health_OnDamage;
    }
    private void Portal_OnPortalInterract(Portal obj)
    {
        AudioUtility.CreateSFX(audioClipRefsSO.portalEnterClip, transform.position, AudioUtility.AudioGroups.SFX, 0f);
    }
    private void Health_OnDamage(float arg1, Vector3 arg2)
    {
        AudioClip randomClip = audioClipRefsSO.getHitClips[UnityEngine.Random.Range(0, audioClipRefsSO.getHitClips.Length)];
        AudioUtility.CreateSFX(randomClip, transform.position, AudioUtility.AudioGroups.SFX, 1f);
    }
    private void Bow_OnArrowShoot()
    {
        AudioUtility.CreateSFX(audioClipRefsSO.arrowShoot, transform.position, AudioUtility.AudioGroups.SFX, 1f);
    }

    private void Bow_OnBowDraw()
    {
        AudioUtility.CreateSFX(audioClipRefsSO.bowStreching, transform.position, AudioUtility.AudioGroups.SFX, 1f);
    }
    private void MeleAttack_OnMeleAttackHit()
    {
        AudioClip randomClip = audioClipRefsSO.monsterGetHitClips[UnityEngine.Random.Range(0, audioClipRefsSO.monsterGetHitClips.Length)];
        AudioUtility.CreateSFX(randomClip, transform.position, AudioUtility.AudioGroups.SFX, 1f);
    }

    private void MeleAttack_OnMeleAttackSwing()
    {
        AudioClip randomClip = audioClipRefsSO.swings[UnityEngine.Random.Range(0, audioClipRefsSO.swings.Length)];
        AudioUtility.CreateSFX(randomClip, transform.position, AudioUtility.AudioGroups.SFX, 0f);
    }
    
    private void Player_OnPlayerMoved()
    {
        AudioClip randomClip = audioClipRefsSO.footSteps[UnityEngine.Random.Range(0, audioClipRefsSO.footSteps.Length)];
        AudioUtility.CreateSFX(randomClip, transform.position, AudioUtility.AudioGroups.SFX, 0f);
    }
}
