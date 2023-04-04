using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSegment : MonoBehaviour
{
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
    }

    public void EnableAccess()
    {
        isAccessable = true;
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
