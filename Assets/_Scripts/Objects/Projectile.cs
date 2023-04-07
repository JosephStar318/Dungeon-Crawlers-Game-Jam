using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AudioClip explodeSfx;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Health health))
        {
            Vector3 damageDir = collision.transform.position - transform.position;
            health.TakeDamage(attackDamage, damageDir);
        }

        AudioUtility.CreateSFX(explodeSfx, transform.position, AudioUtility.AudioGroups.SFX, 1f);
        Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject,0.1f);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

}
