using UnityEngine;
using System.Collections.Generic;

public class RandomGroundVillageBuilder : MonoBehaviour
{
    [Header("Ground")]
    public GameObject ground; // 바닥 오브젝트 (MeshRenderer 필수)

    [Header("House Prefabs")]
    public GameObject[] housePrefabs;

    [Header("Village Settings")]
    public int houseCount = 40;
    public float minDistance = 2f; // 집끼리 최소 거리

    private Bounds groundBounds;

    [ContextMenu("Generate Village")] // 인스펙터에서 버튼 클릭 가능
    public void GenerateVillage()
    {
        if (ground == null)
        {
            Debug.LogError("Ground Mesh 필요!");
            return;
        }

        Renderer groundRenderer = ground.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogError("Ground에 Renderer가 필요합니다!");
            return;
        }

        groundBounds = groundRenderer.bounds;

        ClearChildren();

        if (housePrefabs == null || housePrefabs.Length == 0)
        {
            Debug.LogError("House Prefabs가 비어있습니다!");
            return;
        }

        List<Vector3> placedPositions = new List<Vector3>();

        int attempts = 0;
        int maxAttempts = houseCount * 10;

        for (int i = 0; i < houseCount; i++)
        {
            bool placed = false;

            while (!placed && attempts < maxAttempts)
            {
                float x = Random.Range(groundBounds.min.x + 1f, groundBounds.max.x - 1f);
                float z = Random.Range(groundBounds.min.z + 1f, groundBounds.max.z - 1f);

                Vector3 pos = new Vector3(x, groundBounds.max.y, z);

                bool tooClose = false;
                foreach (var p in placedPositions)
                {
                    if (Vector3.Distance(p, pos) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    GameObject prefab = housePrefabs[Random.Range(0, housePrefabs.Length)];
                    Collider col = prefab.GetComponent<Collider>();
                    float yOffset = col.bounds.extents.y; // 바닥 기준 맞춤

                    GameObject house = Instantiate(prefab, pos + Vector3.up * yOffset, Quaternion.identity, transform);
                    house.name = $"House_{i}";
                    placedPositions.Add(pos);
                    placed = true;
                }

                attempts++;
            }

            if (!placed)
            {
                Debug.LogWarning($"모든 집을 배치하지 못했습니다. minDistance를 줄이거나 houseCount를 줄이세요.");
                break;
            }
        }

        Debug.Log($"배치된 집 수: {placedPositions.Count}/{houseCount}");
    }

    void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
