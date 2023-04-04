using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask segmentLayer;
    [SerializeField] private List<GameObject> monsterPrefabs = new List<GameObject>();

    public static List<DungeonSegment> dungeonSegments = new List<DungeonSegment>(49);
    public void SpawnMonsters(DungeonSegment centerSegment, float segmentLength, int radius)
    {
        Vector3 center = centerSegment.GetPivotPosition();
        dungeonSegments.Clear();
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (Mathf.Abs(i) != radius && Mathf.Abs(j) != radius) continue;
                Vector3 rayOrigin = new Vector3(center.x + i * segmentLength, segmentLength, center.z + j * segmentLength);
                if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, segmentLength, segmentLayer))
                {
                    if(hit.transform.GetComponent<DungeonSegment>().IsSpawnable(out DungeonSegment segment))
                    {
                        dungeonSegments.Add(segment);
                    }
                }
            }
        }
        int monsterIndex = UnityEngine.Random.Range(0, monsterPrefabs.Count);
        int spawnIndex = UnityEngine.Random.Range(0, dungeonSegments.Count);

        Instantiate(monsterPrefabs[monsterIndex], dungeonSegments[spawnIndex].GetPivotPosition(), Quaternion.identity);
        
    }
}
