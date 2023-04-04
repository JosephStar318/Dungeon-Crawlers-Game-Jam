using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static event Action OnArrowHit;

    [SerializeField] private float arrowDamage;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player") == false)
        {
            if(collision.transform.TryGetComponent(out Health health))
            {
                health.TakeDamage(arrowDamage, transform.forward);
                OnArrowHit?.Invoke();
                collision.transform.GetComponent<IHittable>().OnHit(collision.collider.ClosestPoint(transform.position));
            }
            Destroy(gameObject);
        }
    }
}
