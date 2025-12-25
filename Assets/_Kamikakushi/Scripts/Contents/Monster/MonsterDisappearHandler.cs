using System.Collections;
using UnityEngine;
using _Kamikakushi.Contents.Monster;

public class MonsterDisappearHandler : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 1f;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private float detectorDelayAfterRespawn = 1.5f;
    [SerializeField] private bool canRespawn;
    
    private Monster monster;
    private Detector detector;
    private Vector3 spawnPos;
    private Quaternion spawnRot;

    // 🔹 외부에서 몬스터 지정
    public void Init(Monster target)
    {
        monster = target;
        detector = monster.GetComponentInChildren<Detector>();

        spawnPos = monster.transform.position;
        spawnRot = monster.transform.rotation;
    }

    // 🔹 몬스터가 닿았을 때 호출
    public void StartDisappear()
    {
        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        monster.OnTouchedPlayer();

        yield return new WaitForSeconds(disappearDelay);

        monster.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        Respawn();
    }

    private void Respawn()
    {
        monster.transform.position = spawnPos;
        monster.transform.rotation = spawnRot;

        monster.gameObject.SetActive(true);

        monster.OnRespawned();

        if (detector != null)
        {
            detector.SetEnable(false);
            Invoke(nameof(EnableDetector), detectorDelayAfterRespawn);
        }
    }

    private void EnableDetector()
    {
        detector.SetEnable(true);
    }
}
