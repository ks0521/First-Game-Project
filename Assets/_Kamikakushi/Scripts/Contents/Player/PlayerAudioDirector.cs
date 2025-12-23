using _Kamikakushi.Audio;
using _Kamikakushi.Contents.Player.Test;
using _Kamikakushi.Utills.Audio;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerAudioDirector : MonoBehaviour
    {
        [SerializeField] private PlayerAudio playerAudio; //피격, 발소리 등 플레이어 관련 sfx
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private HashSet<MonsterTest> chaser;

        private bool isHide;
        [SerializeField] private float interval = 0.4f;
        //[SerializeField] private float updateRate = 0.3f;
        [SerializeField] private float breathMinVolume = 0.28f;
        [SerializeField] private float breathMaxVolume = 0.38f;

        [Tooltip("심장소리가 들리는 최소 사거리")]
        [SerializeField] private float minDist = 2.5f;
        [Tooltip("심장소리가 들리는 최대 사거리")]
        [SerializeField] private float maxDist = 18f;
        [Tooltip("최대 심장박동소리")]
        [SerializeField] private float maxHeartbeatVol = 0.8f;
        [Tooltip("최소 심작박동소리 속도")]
        [SerializeField] private float minPitch = 0.85f;
        [Tooltip("최대 심장박동소리 속도")]
        [SerializeField] private float maxPitch = 1.00f;

        [SerializeField] private float nearest;
        private float ratio;
        private float distance;
        private float volume;
        private float pitch;
        private float mentalRatio;
        private Vector3 playerPos;
        private void Awake()
        {
            playerAudio = GetComponent<PlayerAudio>();
            playerManager = GetComponent<PlayerManager>();
            //현재 플레이어를 쫓고 있는 몬스터집합
            chaser = new HashSet<MonsterTest>();
            StartCoroutine(AudioUpdate(interval));
        }
        
        private void OnEnable()
        {
            foreach (var monster in FindObjectsByType<MonsterTest>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
                ))
            {
                Debug.Log(monster.name);
                monster.OnChaseStarted += ChaeserAdd;
                monster.OnChaseEnd += ChaserDelete;
            }
        }
        private void OnDisable()
        {
            foreach (var monster in FindObjectsByType<MonsterTest>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
                ))
            {
                monster.OnChaseStarted -= ChaeserAdd;
                monster.OnChaseEnd -= ChaserDelete;
            }
            chaser.Clear();
        }
        void ChaeserAdd(MonsterTest _monster) => chaser.Add(_monster);
        void ChaserDelete(MonsterTest _monster) => chaser.Remove(_monster);

        IEnumerator AudioUpdate(float interval)
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);
                UpdateBreath();
                UpdateHeartbeat();
                UpdateNoiseByStatus();
            }
        }
        private void UpdateBreath()
        {
            isHide = (playerManager != null) && (playerManager.isHide);
            playerAudio.SetBreath(
                isHide, 
                UnityEngine.Random.Range(breathMinVolume,breathMaxVolume));
        }
        private void UpdateHeartbeat()
        {
            //몬스터가 null이거나 현재 씬에서 비활성화되어있으면 chaser에서 제거
            chaser.RemoveWhere(monster => monster == null
            || !monster.gameObject.activeInHierarchy);
            if(chaser.Count == 0)
            {
                playerAudio.SetHeartbeat(_volume: 0f, _pitch: 1f);
                return;
            }
            nearest = float.MaxValue;
            playerPos = transform.position;
            foreach(var monster in chaser)
            {
                distance = Vector3.Distance(playerPos, monster.transform.position);
                nearest = nearest < distance ? nearest : distance;
            }
            Debug.Log("귀신 최단거리 계산완료");
            //nearest가 minDist와 maxDist사이 어느정도에 존재하는지 계산
            // 1 = nearest == maxDist / 0 = nearest == minDist
            // 따라서 1에 가까울수록 몬스터가 가까이, 0에 가까울수록 최대 거리에 존재
            ratio = 1f - Mathf.InverseLerp(minDist, maxDist, nearest);
            ratio = Mathf.Clamp01(ratio);
            //가까워질수록 증가율이 커짐
            ratio = Mathf.Pow(ratio, 2f);

            playerAudio.PlayHeartbeat();

            volume = Mathf.Lerp(0f, maxHeartbeatVol, ratio);
            pitch = Mathf.Lerp(minPitch, maxPitch, ratio);
            playerAudio.SetHeartbeat(volume, pitch);
        }
        private void UpdateNoiseByStatus()
        {
            mentalRatio = playerManager.stat.sanity / playerManager.stat.MaxSanity;
            //정신력이 60%이상일 시 이상효과 없음
            if (mentalRatio > 0.6)
            {
                AudioManager.Instance.setNoise(0);
                return;
            }
            float noise;
            //정신력 감소할때마다 노이즈 커짐
            if (mentalRatio < 1)
            {
                noise = (1f - mentalRatio) / 2;
            }
            else noise = 0.5f;
                AudioManager.Instance.setNoise(noise);
        }
    }
}
