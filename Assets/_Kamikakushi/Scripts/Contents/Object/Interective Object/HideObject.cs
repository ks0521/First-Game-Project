using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class HideObject : InteractItems
    {
        [SerializeField]
        [Tooltip("숨을 때 플레이어의 시점")]
        Transform hidePoint;
        [SerializeField]
        [Tooltip("해당 오브젝트의 이름 입력. \n 미입력시 숨는공간으로 이름정해짐")]
        string overrideObjectName;

        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            conditions = GetComponents<IInteractionCondition>();
            interactType = InteractType.Event;
            hidePoint = GetComponentInChildren<HideEnterViewPoint>()?.transform;
            //ClosetViewPoint 위치탐색용 마커 스크립트
            if (hidePoint == null)
            {
                Debug.LogWarning($"{gameObject.name} 숨기장소 지정 오류!");
            }
            context.promptKey = PromptKey.Hide;
            context.displayName = string.IsNullOrEmpty(overrideObjectName)
                                  ? "바구니"
                                  : overrideObjectName;

            result.actions.Add(new HideEnterAction(hidePoint));
            result.actions.Add(new PlaySFXAction(Utills.Audio.SFXType.HideEnter));
        }
        public override InteractResult Interact(PlayerManager target)
        {
            Debug.Log($"{context.displayName}오브젝트 {hidePoint.position}위치에 숨기");
            result.success = true;
            result.message = "플레이어 숨음";
            Debug.Log(result.actions.Count);
            return result;
        }
    }
}