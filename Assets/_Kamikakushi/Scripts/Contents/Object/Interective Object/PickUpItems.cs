using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;
using Project.Inventory;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using System;
using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Utills.Audio;
using _Kamikakushi.Contents.Item;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class PickUpItems : InteractItems
    {
        //실제 인벤토리에 저장될 아이템 정보
        [SerializeField] protected ItemData data;

        protected override void Init()
        {
            context.displayName = data.itemName ?? "사용아이템";
            context.promptKey = PromptKey.PickupItem;
        }

        public override InteractResult Interact(PlayerManager target)
        {
            if (!target.inven.Add(data))
            {
                result.success = false;
                result.message = "가방이 꽉 찼다...";
                Debug.Log("아이템 획득 실패");
                return result;
            }
            Debug.Log("인벤토리 추가");
            Destroy(gameObject);
            result.success = true;
            result.message = context.displayName + " 획득";
            //result.actions.Add(new PlaySFXAction(SFXType.PickupItem)); 컴포넌트에 붙이기
            return result;
        }
    }

}

