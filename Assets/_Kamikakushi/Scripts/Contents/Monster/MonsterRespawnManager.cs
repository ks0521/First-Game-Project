using System.Collections;
using UnityEngine;
using _Kamikakushi.Contents.Monster;

public class MonsterRespawnManager : MonoBehaviour
{
    public static MonsterRespawnManager Instance;
    [Header("Disappear / Respawn Timing")]
    [SerializeField] private float disappearDelay = 0.5f;   // 👈 충돌 후 사라지기까지
    [SerializeField] private float respawnDelay = 3f;        // 👈 사라진 뒤 리스폰까지
    [SerializeField] private float detectorDelay = 1.5f;     // 👈 리스폰 후 감지 재개
    private void Awake()
    {
        Instance = this;
    }

    public void StartDisappear(Monster monster)
    {
        StartCoroutine(DisappearRoutine(monster));
    }

    private IEnumerator DisappearRoutine(Monster monster)
    {
        Detector detector = monster.GetComponentInChildren<Detector>();

        // 1️⃣ 몬스터 정지
        monster.OnTouchedPlayer();

        yield return new WaitForSeconds(disappearDelay);

        // 2️⃣ 사라짐
        monster.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        // 3️⃣ 리스폰
        monster.gameObject.SetActive(true);
        monster.OnRespawned();

        // 4️⃣ Detector 딜레이
        if (detector != null)
        {
            detector.SetEnable(false);
            yield return new WaitForSeconds(detectorDelay);
            detector.SetEnable(true);
        }
    }
}
