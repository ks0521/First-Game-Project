using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Contents.Item
{
    public class Key : PickUpItems
    {
        protected override void Init()
        {
            context.displayName = "열쇠";
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
            result.message = context.displayName+"아이템 획득";
            return result;
        }
    }
}
