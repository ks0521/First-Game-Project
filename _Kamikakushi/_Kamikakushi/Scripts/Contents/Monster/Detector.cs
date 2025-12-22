using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Detector : MonoBehaviour
    {
        private Monster owner;
        private bool isEnabled = true;

        public void Init(Monster monster)
        {
            owner = monster;
        }

        public void SetEnable(bool enable)
        {
            isEnabled = enable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isEnabled) return;
            if (!other.CompareTag("Player")) return;
            if (owner.IsTouchingPlayer) return;

            owner.OnPlayerDetected(other.transform.position);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!isEnabled) return;
            if (!other.CompareTag("Player")) return;
            if (owner.IsTouchingPlayer) return;

            owner.OnPlayerDetected(other.transform.position);
        }
    }
}
