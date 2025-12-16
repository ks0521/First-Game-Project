using UnityEngine;
using _Kamikakushi.Contents.Player;


namespace _Kamikakushi.Contents.Player
{
    public class Detectortest : MonoBehaviour
    {
        private Monstertest owner;

        public void Init(Monstertest owner)
        {
            this.owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // 플레이어 발견
            owner.OnPlayerDetected(other.transform.position);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // 플레이어가 범위 안에 계속 있음 → 계속 추적
            owner.OnPlayerDetected(other.transform.position);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // 플레이어가 감지 범위에서 나갔음 → 추적 중단
            owner.OnPlayerLost();
        }
    }
}
