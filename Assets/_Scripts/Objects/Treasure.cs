using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInterractable
{
    [SerializeField] private AudioSource audioSource;

    public void Interract(Transform transform)
    {
        GameManager.LevelComplete();
        audioSource.Play();
    }
}
