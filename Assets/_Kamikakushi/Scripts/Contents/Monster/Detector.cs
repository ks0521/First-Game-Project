using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets._Kamikakushi.Contents.Monster
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private float range;
        private Monster owner;

        public void Init(Monster owner)
        {
            //몬스터 컴포넌트를 인자로 받아 저장
            this.owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            //owner가 null이 아니라면 owner의 OnPlayerDetected 실행, 탐지된 대상의 위치를 인자로 제공
            owner?.OnPlayerDetected(other.transform.position);
        }
    }
}