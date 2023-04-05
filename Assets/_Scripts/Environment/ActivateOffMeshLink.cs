using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOffMeshLink : MonoBehaviour
{
    [SerializeField] private DungeonSegment subscribedSegment;
    private void Start()
    {
        subscribedSegment.OnEnabledAccess += SubscribedSegment_OnEnabledAccess;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        subscribedSegment.OnEnabledAccess -= SubscribedSegment_OnEnabledAccess;
    }
    private void SubscribedSegment_OnEnabledAccess()
    {
        gameObject.SetActive(true);
    }
}
