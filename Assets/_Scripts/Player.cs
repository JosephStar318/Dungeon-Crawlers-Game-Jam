using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnPortalEntered;

    private DungeonSegment currentDungeonSegment;
    private Portal interractedPortal;

    [SerializeField] LayerMask segmentLayer;
    [SerializeField] LayerMask interractableLayer;

    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private AnimationCurve rotationCurve;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 10f;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float rotationCurveIndex;
    private float movementCurveIndex;
    
    private bool isRotating = false;
    private bool isMoving = false;
    private bool isUsingPortal = false;


    private void OnEnable()
    {
        PlayerInputHelper.OnMoveChanged += PlayerInputHelper_OnMoveChanged;
        PlayerInputHelper.OnInterractPressed += PlayerInputHelper_OnInterractPressed;
        Portal.OnPortalInterract += Portal_OnPortalInterract;
    }



    private void OnDisable()
    {
        PlayerInputHelper.OnMoveChanged -= PlayerInputHelper_OnMoveChanged;
        PlayerInputHelper.OnInterractPressed -= PlayerInputHelper_OnInterractPressed;
        Portal.OnPortalInterract -= Portal_OnPortalInterract;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationCurve.Evaluate(rotationCurveIndex));
            rotationCurveIndex += rotationSpeed * Time.deltaTime;
            if (rotationCurveIndex > 1)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    private void MovePlayer()
    {
        if (isMoving)
        {
            transform.position = Vector3.Slerp(startPosition, targetPosition, movementCurve.Evaluate(movementCurveIndex));
            movementCurveIndex += moveSpeed * Time.deltaTime;
            if(movementCurveIndex > 1)
            {
                transform.position = targetPosition;
                isMoving = false;
                OnTargetReached();
            }
        }
    }

    private void OnTargetReached()
    {
        if (isUsingPortal)
        {
            transform.position = interractedPortal.GetAttachedPortal().transform.position;

            DungeonSegment dungeonSegment;
            TryGetDungeonSegmentInMoveDirection(1, out dungeonSegment);
            currentDungeonSegment = dungeonSegment;
            currentDungeonSegment.ShowInMinimap();
            SetTargetPosition(dungeonSegment.GetPivotPosition());
            isUsingPortal = false;

            OnPortalEntered?.Invoke();
        }
    }

    private void PlayerInputHelper_OnMoveChanged(Vector2 dir)
    {
        if (dir == Vector2.zero) return;

        if (dir.x > 0.5f && isRotating == false)
        {
            startRotation = transform.rotation;
            targetRotation = Quaternion.LookRotation(transform.right, Vector3.up);
            rotationCurveIndex = 0;
            isRotating = true;
        }
        else if (dir.x < -0.5f && isRotating == false)
        {
            startRotation = transform.rotation;
            targetRotation = Quaternion.LookRotation(-transform.right, Vector3.up);
            rotationCurveIndex = 0;
            isRotating = true;
        }
        else if (dir.y != 0f && isMoving == false)
        {
            DungeonSegment dungeonSegment;
            if (TryGetDungeonSegmentInMoveDirection(dir.y, out dungeonSegment))
            {
                currentDungeonSegment = dungeonSegment;
                currentDungeonSegment.ShowInMinimap();
                SetTargetPosition(dungeonSegment.GetPivotPosition());
            }
        }
    }
    private void SetTargetPosition(Vector3 targetPos)
    {
        startPosition = transform.position;
        targetPosition = targetPos;
        movementCurveIndex = 0;
        isMoving = true;
    }

    private void PlayerInputHelper_OnInterractPressed()
    {
        IInterractable interractable;
        if(TryGetInterractableObject(out interractable))
        {
            interractable.Interract(transform);
        }
    }
    private void Portal_OnPortalInterract(Portal portal)
    {
        isUsingPortal = true;
        SetTargetPosition(portal.transform.position);
        interractedPortal = portal;
    }

    private bool TryGetInterractableObject(out IInterractable interractable)
    {
        Vector3 rayDir = transform.forward;
        Vector3 rayPos = transform.position;
        if (Physics.Raycast(rayPos, rayDir, out RaycastHit hit, 10f, interractableLayer))
        {
            interractable = hit.transform.GetComponent<IInterractable>();
            return true;
        }
        interractable = default;
        return false;
    }

    private bool TryGetDungeonSegmentInMoveDirection(float dir, out DungeonSegment dungeonSegment)
    {
        Vector3 rayDir = dir > 0 ? transform.forward : -transform.forward;
        Vector3 rayPos = transform.position;
        if(Physics.Raycast(rayPos, rayDir, out RaycastHit hit, 10f, segmentLayer))
        {
            dungeonSegment = hit.transform.GetComponent<DungeonSegment>();
            if (dungeonSegment.IsAccessable)
            {
                return true;
            }
        }
        dungeonSegment = default;
        return false;
    }
}
