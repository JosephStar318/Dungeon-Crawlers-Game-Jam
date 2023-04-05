using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInterractable
{
    [SerializeField] private DungeonSegment[] dungeonSegments;
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
            
            foreach (DungeonSegment item in dungeonSegments)
            {
                item.GetComponent<Animation>().Play();
            }

            audioSoruce.Play();
            isTurnedOn = true;
        }
        
    }

}
