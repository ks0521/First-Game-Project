using UnityEngine;

namespace _Kamikakushi.Utills.Interfaces
{
    public class Detectorble : MonoBehaviour
    {
        public bool CanDetected { get; private set; } = true;

        public void Hide()
        {
            CanDetected = false;
        }

        public void UnHide()
        {
            CanDetected = true;
        }
    }
}
