using UnityEngine;

namespace _Kamikakushi.Contents.Monster
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private float range = 3f;
        private Monster owner;

        public void Init(Monster owner)
        {
            this.owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Detector Trigger Enter: " + other.name);
            if (!other.CompareTag("Player")) return;

            owner?.OnPlayerDetected(other.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    
    }
}
