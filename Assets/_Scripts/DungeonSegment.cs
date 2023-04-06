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

    [SerializeField] private Collider segmentCollider;
    [SerializeField] private LayerMask enemyLayer;
    public bool IsAccessable 
    { 
        get
        {
            if (isAccessable == false)
            {
                return false;
            }
            else
            {
                Collider[] colls = Physics.OverlapBox(segmentCollider.bounds.center, segmentCollider.bounds.size*0.5f, Quaternion.identity, enemyLayer);
                if (colls.Length > 1)
                {
                    return false;
                }
            }
            return true;
        } 
    }
    
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
