using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private float delay;
    [SerializeField] private float shootPeriod;
    [SerializeField] private float projectileSpeed = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(ShootProjectile), delay, shootPeriod);
    }

    private void ShootProjectile()
    {
        AudioUtility.CreateSFX(shootSfx, transform.position, AudioUtility.AudioGroups.SFX, 1f);
        GameObject obj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        obj.GetComponent<Projectile>().SetSpeed(projectileSpeed);
    }
}
