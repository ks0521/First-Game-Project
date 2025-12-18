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
    public class Closet : InteractItems
    {
        [SerializeField] Transform hidePoint;
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
            context.displayName = "옷장";

            result.actions.Add(new HideEnterAction(hidePoint));
        }
        public override InteractResult Interact(PlayerManager target)
        {
            Debug.Log($"{hidePoint.position}위치에 숨기");
            result.success = true;
            result.message = "플레이어 숨음";
            Debug.Log(result.actions.Count);
            return result;
        }
    }
}