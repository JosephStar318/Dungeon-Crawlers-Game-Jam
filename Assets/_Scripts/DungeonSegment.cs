using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSegment : MonoBehaviour
{

    [SerializeField] private Transform pivotPoint;
    [SerializeField] private bool isAccessable;

    public bool IsAccessable { get => isAccessable; }

    public Vector3 GetPivotPosition()
    {
        return pivotPoint.position;
    }

}
