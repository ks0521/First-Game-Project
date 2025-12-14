using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] PlayerEvents events;
        [SerializeField] PlayerManager playerManager;
        private IInteractable obj;
        //상호작용 시도가 가능한가? -> 레이캐스트가 iinteractable인가?
        private bool canInteractAttempt;
        private bool canInteract;

        private void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            events.RaycastEnter += Attemptable;
            events.RaycastOut += NotAttempable;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canInteractAttempt)
                {
                    Debug.Log(obj.Interact(playerManager));
                }
            }
        }
        void Attemptable(RaycastHit hit)
        {
            if(hit.collider.gameObject.TryGetComponent<IInteractable>(out obj))
            {
                canInteractAttempt = true;
            }
        }
        void NotAttempable()
        {
            canInteractAttempt = false;
        }

    }
}
