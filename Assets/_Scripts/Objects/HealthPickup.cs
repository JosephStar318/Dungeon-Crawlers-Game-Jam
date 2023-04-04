using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInterractable
{
    [SerializeField] private float healAmount = 20f;
    [SerializeField] private GameObject particles;

    public void Interract(Transform t)
    {
        t.GetComponent<Health>().Heal(healAmount);
        Instantiate(particles, transform.position, Quaternion.identity);
        StartCoroutine(GetSmall());
    }

    private IEnumerator GetSmall()
    {
        while(transform.lossyScale.x > 0.1f)
        {
            transform.localScale -= Vector3.one * 0.1f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
