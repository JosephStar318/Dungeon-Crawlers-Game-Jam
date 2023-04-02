using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private void OnEnable()
    {
        Player.OnPlayerMoved += Player_OnPlayerMoved;
        MeleAttack.OnMeleAttackSwing += MeleAttack_OnMeleAttackSwing;
        MeleAttack.OnMeleAttackHit += MeleAttack_OnMeleAttackHit;
    }
    private void OnDisable()
    {
        Player.OnPlayerMoved -= Player_OnPlayerMoved;
        MeleAttack.OnMeleAttackSwing -= MeleAttack_OnMeleAttackSwing;
        MeleAttack.OnMeleAttackHit -= MeleAttack_OnMeleAttackHit;
    }
    private void MeleAttack_OnMeleAttackHit()
    {
        AudioClip randomClip = audioClipRefsSO.monsterGetHitClips[UnityEngine.Random.Range(0, audioClipRefsSO.monsterGetHitClips.Length)];
        AudioUtility.CreateSFX(randomClip, transform.position, AudioUtility.AudioGroups.SFX, 0f);
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
