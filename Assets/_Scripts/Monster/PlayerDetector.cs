using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private Vector3 detectionBoxCenter;
    [SerializeField] private Vector3 detectionBoxSize;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.TransformPoint(detectionBoxCenter), detectionBoxSize);
    }

    public bool DetectPlayer(out Transform detectedTransform)
    {
        Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(detectionBoxCenter), detectionBoxSize, transform.rotation, detectionLayer);

        if (colliders.Length > 0)
        {
            detectedTransform = colliders[0].transform;
            return true;
        }

        detectedTransform = default;
        return false;
    }
}
