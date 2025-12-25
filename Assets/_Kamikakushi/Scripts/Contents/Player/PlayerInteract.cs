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
        PlayerEvents events;
        PlayerManager playerManager;
        PlayerAudio playerAudio;
        private IInteractable obj;
        //상호작용 시도가 가능한가? -> 레이캐스트가 iinteractable인가?
        private InteractResult result;
        private bool canInteractAttempt;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            playerAudio = GetComponent<PlayerAudio>();
        }
        private void OnEnable()
        {
            events.GetInteractContext += Attemptable;
            events.GetInteractable += GetIntaractable;
            events.RaycastOut += NotAttempable;
        }
        private void OnDisable()
        {
            events.GetInteractContext -= Attemptable;
            events.GetInteractable -= GetIntaractable;
            events.RaycastOut -= NotAttempable;
        }
        private void Update()
        {
            //E누르면 상호작용
            if (Input.GetKeyDown(KeyCode.E) && !playerManager.isHide)
            {
                if (canInteractAttempt)
                {
                    if (obj == null)
                    {
                        Debug.LogWarning("상호작용 대상 오브젝트 없음");
                        return;
                    }
                    //obj: 현재 관측중인 IInteractable 오브젝트
                    result = obj.Interact(playerManager);
                    //결과를 이벤트로 출력(UI에서 결과 텍스트로 출력하기 위함)
                    events.OnInteract(result);
                    //상호작용 실패 시 복귀
                    if (!result.success) return;
                    //실행해야 하는 액션이 없으면 execute 스킵
                    if (result.actions != null)
                    {
                        //모든 조건 실행
                        foreach (IInteractAction action in result.actions)
                        {
                            action.Execute(playerManager, obj);
                        }
                    }
                }
            }
            //숨은상태에서 E 입력시 빠져나오기
            else if (Input.GetKeyDown(KeyCode.E) && playerManager.isHide)
            {
                //나올때는 playerinteract에서 소리 발생시키기
                playerAudio.PlaySFX(Utills.Audio.SFXType.HideEnter);
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
