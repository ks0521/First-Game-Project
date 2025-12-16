using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
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
        [SerializeField] private IInteractable obj;
        //상호작용 시도가 가능한가? -> 레이캐스트가 iinteractable인가?
        private InteractResult result;
        private bool canInteractAttempt;
        private bool canInteract;

        private void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            events.GetInteractContext += Attemptable;
            events.GetInteractable += GetIntaractable;
            events.RaycastOut += NotAttempable;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canInteractAttempt)
                {
                    result = obj.Interact(playerManager);
                    events.OnInteract(result);
                    Debug.Log(obj.GetContext().displayName);
                }
            }
        }
        void GetIntaractable(IInteractable interactable)
        {
            obj = interactable;
        }
        void Attemptable(InteractContext context)
        {
            canInteractAttempt = true;
        }
        void NotAttempable()
        {
            canInteractAttempt = false;
        }

    }
}
