using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDungeon : MonoBehaviour
{
    [SerializeField] private DungeonSegment refDungeonSegment;
    [SerializeField] private GameObject visualBlockPrefab;
    [SerializeField] private GameObject visualBlock;

    private DungeonSegment dungeonSegment;
    
    private void Start()
    {
        dungeonSegment = GetComponent<DungeonSegment>();
        visualBlock = Instantiate(visualBlockPrefab, transform);
    }
    private void OnEnable()
    {
        refDungeonSegment.OnVisibleOnMinimap += RefDungeonSegment_OnEnableAccess;
    }
    private void OnDisable()
    {
        refDungeonSegment.OnVisibleOnMinimap -= RefDungeonSegment_OnEnableAccess;
    }
    private void RefDungeonSegment_OnEnableAccess()
    {
        dungeonSegment.EnableAccess();
        visualBlock.SetActive(false);
    }
}
