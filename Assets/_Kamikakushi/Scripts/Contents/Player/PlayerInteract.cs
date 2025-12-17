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
        [SerializeField] PlayerHide hide;
        [SerializeField] private IInteractable obj;
        private Transform prevTransform;
        //상호작용 시도가 가능한가? -> 레이캐스트가 iinteractable인가?
        private InteractResult result;
        private bool canInteractAttempt;
        private bool canInteract;

        private void Start()
        {
            hide = GetComponent<PlayerHide>();
            playerManager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            events.GetInteractContext += Attemptable;
            events.GetInteractable += GetIntaractable;
            events.RaycastOut += NotAttempable;
        }
        private void Update()
        {
            //E누르면 상호작용
            if (Input.GetKeyDown(KeyCode.E) && !playerManager.isHide)
            {
                if (canInteractAttempt)
                {
                    result = obj.Interact(playerManager);
                    events.OnInteract(result);
                    //숨을 수 있는장소면 숨기
                    if (result.transform != null)
                    {
                        prevTransform = transform;
                        events.OnHideEnter(result.transform);
                        Debug.Log("hideEnter");
                    }
                    Debug.Log(obj.GetContext().displayName);
                }
            }
            //숨은상태에서 E 입력시 빠져나오기
            else if(Input.GetKeyDown(KeyCode.E) && playerManager.isHide)
            {
                events.OnHideOut();
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
