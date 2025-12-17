using UnityEngine;
using _Kamikakushi.Utills.Interfaces;

namespace _Kamikakushi.Contents.Monster
{
    public class Detector : MonoBehaviour
    {
        private Monster owner;

        public void Init(Monster owner)
        {
            this.owner = owner;
        }

        private bool CanDetect(Collider other)
        {
            if (!other.CompareTag("Player"))
                return false;

            Detectorble detectorble = other.GetComponent<Detectorble>();
            if (detectorble != null && detectorble.CanDetected == false)
                return false;

            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!CanDetect(other)) return;

            owner.OnPlayerDetected(other.transform.position);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!CanDetect(other)) return;

            owner.OnPlayerDetected(other.transform.position);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            owner.OnPlayerLost();
        }
    }
}
