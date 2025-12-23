using UnityEngine;
using System.Collections.Generic;

public class GroundVillageBuilder : MonoBehaviour
{
    [Header("Ground")]
    public Collider groundCollider;

    [Header("House Prefabs")]
    public GameObject[] housePrefabs;

    [Header("Maze Settings")]
    [Tooltip("격자 크기: 집보다 커야 합니다. (60사이즈 집이면 70~80 추천)")]
    public float cellSize = 70f;

    [Range(0.1f, 1f)]
    public float density = 0.7f;

    [ContextMenu("Build Maze Village")]
    public void BuildVillage()
    {
        if (groundCollider == null || housePrefabs == null || housePrefabs.Length == 0) return;

        ClearChildren();

        Bounds g = groundCollider.bounds;

        // 1. 격자 수 계산 (지형 크기에 맞게)
        int cols = Mathf.FloorToInt(g.size.x / cellSize);
        int rows = Mathf.FloorToInt(g.size.z / cellSize);

        if (cols == 0 || rows == 0)
        {
            Debug.LogError("지형이 cellSize에 비해 너무 작습니다! cellSize를 줄이거나 지형을 키워주세요.");
            return;
        }

        // 지형의 정중앙에 격자를 맞추기 위한 시작 오프셋 계산
        float gridWidth = cols * cellSize;
        float gridHeight = rows * cellSize;
        Vector3 gridStartPos = g.center - new Vector3(gridWidth / 2, 0, gridHeight / 2);

        int spawnedCount = 0;

        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                if (Random.value > density) continue;

                GameObject prefab = housePrefabs[Random.Range(0, housePrefabs.Length)];

                // 해당 칸의 중심 좌표
                Vector3 currentCellCenter = gridStartPos + new Vector3(c * cellSize + cellSize / 2, 0, r * cellSize + cellSize / 2);

                // 2. 일단 생성 (측정을 위해)
                GameObject house = Instantiate(prefab);

                // 3. 회전 적용 (미로 느낌을 위해 90도 단위)
                float[] rotations = { 0, 90, 180, 270 };
                house.transform.rotation = Quaternion.Euler(0, rotations[Random.Range(0, rotations.Length)], 0);

                // 4. 실제 크기 측정
                Bounds b = GetTotalMeshBounds(house);
                float bottomOffset = -b.min.y;

                // 5. 위치 고정 (바닥 높이 맞춤)
                house.transform.position = new Vector3(currentCellCenter.x, g.max.y + bottomOffset, currentCellCenter.z);
                house.transform.SetParent(this.transform);

                // [중요] 지형 밖으로 나가는지 체크하는 로직을 완화하거나 로그를 남김
                // 만약 지형 밖으로 나가는 게 싫다면 여기 아래 주석을 해제하세요.
                /*
                if (!g.Intersects(b)) {
                    DestroyImmediate(house);
                    continue;
                }
                */

                spawnedCount++;
            }
        }
        Debug.Log($"미로 마을 생성 완료: {spawnedCount}개의 집 배치됨");
    }

    private Bounds GetTotalMeshBounds(GameObject obj)
    {
        Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
        if (renders.Length == 0) return new Bounds(obj.transform.position, Vector3.one);

        Bounds b = renders[0].bounds;
        foreach (Renderer r in renders) b.Encapsulate(r.bounds);
        return b;
    }

    void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
}