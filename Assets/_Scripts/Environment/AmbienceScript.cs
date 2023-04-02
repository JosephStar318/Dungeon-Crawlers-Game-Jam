using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceScript : MonoBehaviour
{
    [SerializeField] private AudioClip ambienceClip;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioUtility.ChangeAmbience(ambienceClip);
        }
    }
}
