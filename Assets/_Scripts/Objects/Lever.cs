using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInterractable
{
    [SerializeField] private Animation elevatorAnim;
    [SerializeField] private Animation leverAnim;
    private AudioSource audioSoruce;

    private bool isTurnedOn = false;
    private void Start()
    {
        audioSoruce = GetComponent<AudioSource>();
    }
    public void Interract(Transform transform)
    {
        LeverActivate();
    }

    public void LeverActivate()
    {
        if(isTurnedOn == false)
        {
            leverAnim.Play();
            elevatorAnim.Play();
            audioSoruce.Play();
            isTurnedOn = true;
        }
        
    }

}
