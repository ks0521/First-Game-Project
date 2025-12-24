using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class MonsterTouchTrigger : MonoBehaviour
    {
        private Monster owner;

        private void Awake()
        {
            owner = GetComponentInParent<Monster>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Debug.Log("몬스터 충돌 → 사라짐 시작");

            MonsterRespawnManager.Instance
                .StartDisappear(owner);
        }
    }
}
