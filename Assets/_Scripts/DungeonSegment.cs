using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSegment : MonoBehaviour
{
    public event Action OnVisibleOnMinimap;
    public event Action OnEnabledAccess;

    [SerializeField] private GameObject curtain;
    [SerializeField] private DungeonSegment neigbourSegment;

    [SerializeField] private Transform pivotPoint;
    [SerializeField] private bool isAccessable;
    [SerializeField] private bool isSpawnable;

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
        OnVisibleOnMinimap?.Invoke();
    }

    public void EnableAccess()
    {
        isAccessable = true;
        OnEnabledAccess?.Invoke();
    }
    public void EnableAccessToNeigbour()
    {
        if(neigbourSegment != null)
        {
            neigbourSegment.EnableAccess();
        }
    }

    public bool IsSpawnable(out DungeonSegment dungeonSegment)
    {
        dungeonSegment = this;
        return isSpawnable;
    }

}
