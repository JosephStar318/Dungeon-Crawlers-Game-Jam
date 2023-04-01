using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInterractable
{
    public static event Action<Portal> OnPortalInterract;
    [SerializeField] private Portal attachedPortal;

    private Transform interractedTransform;

    public void Interract(Transform targetTransform)
    {
        OnPortalInterract?.Invoke(this);
    }
    public Portal GetAttachedPortal()
    {
        return attachedPortal;
    }
}
