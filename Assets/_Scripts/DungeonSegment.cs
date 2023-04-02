using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSegment : MonoBehaviour
{
    [SerializeField] private GameObject curtain;

    [SerializeField] private Transform pivotPoint;
    [SerializeField] private bool isAccessable;

    public bool IsAccessable { get => isAccessable; }
    
    public Vector3 GetPivotPosition()
    {
        return pivotPoint.position;
    }
    
    public void HideInMinimap()
    {
        curtain.SetActive(true);
    }
    public void ShowInMinimap()
    {
        curtain.SetActive(false);
    }


}
