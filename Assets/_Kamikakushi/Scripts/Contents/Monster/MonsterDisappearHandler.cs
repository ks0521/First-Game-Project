using System.Collections;
using UnityEngine;
using _Kamikakushi.Contents.Monster;

public class MonsterDisappearHandler : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 1f;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private float detectorDelayAfterRespawn = 1.5f;

    private Monster monster;
    private Detector detector;
    private Vector3 spawnPos;
    private Quaternion spawnRot;

    private void Awake()
    {
        monster = GetComponentInParent<Monster>();
        detector = monster.GetComponentInChildren<Detector>();

        spawnPos = monster.transform.position;
        spawnRot = monster.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        monster.ForceStopChase();

        yield return new WaitForSeconds(disappearDelay);

        Invoke(nameof(Respawn), respawnDelay);
        monster.gameObject.SetActive(false);
    }

    private void Respawn()
    {
        monster.transform.position = spawnPos;
        monster.transform.rotation = spawnRot;

        monster.gameObject.SetActive(true);

        // 🔥 리스폰 초기화
        monster.OnRespawned();

        // 🔥 Detector 잠시 OFF
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
