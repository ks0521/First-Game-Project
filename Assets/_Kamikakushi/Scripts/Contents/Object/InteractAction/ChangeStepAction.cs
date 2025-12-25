using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractAction
{
    public class ChangeStepAction : MonoBehaviour, IInteractAction
    {
        [SerializeField] private ProgressStep step;
        [SerializeField] private bool isOnce = true;
        private bool wasActivated;
        public void Execute(PlayerManager player, IInteractable source)
        {
            if (wasActivated && isOnce) return;
            wasActivated = true;

            GameManagers.instance?.SetStep(step);
        }

    }

}
