using _Kamikakushi.Contents.Monster;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.Player.Test;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using UnityEngine;

public class MonsterDisappearHandlerTest : MonoBehaviour
{
    //원래 자리에 돌아오는 시간 
    [SerializeField] private float returnDelay = 4f;
    //돌아오고 디텍터가 켜지는 시간
    [SerializeField] private float detectorDelayAfterRespawn = 2.5f;

    [SerializeField]private PlayerEvents events;
    private MonsterTest monster;
    private DetectorTest detector;
    private Vector3 spawnPos;
    private Quaternion spawnRot;

    private void Awake()
    {
        monster = GetComponentInParent<MonsterTest>();
        detector = monster.GetComponentInChildren<DetectorTest>();

        spawnPos = monster.transform.position;
        spawnRot = monster.transform.rotation;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        StartCoroutine(DisappearRoutine());
    }*/
    public void Disappear()
    {
        StartCoroutine(DisappearRoutine());
    }
    private IEnumerator DisappearRoutine()
    {
        detector.SetEnable(false);
        monster.ForceStopChase();
        yield return new WaitForSeconds(returnDelay);
        Respawn();
        yield return new WaitForSeconds(detectorDelayAfterRespawn);
        detector.SetEnable(true);
    }
    //원래자리로 돌아간 후 탐색상태 변경
    private void Respawn()
    {
        monster.transform.position = spawnPos;
        monster.transform.rotation = spawnRot;

        // 리스폰 초기화
        monster.chasing = ChasingState.Idle;
    }
}
