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
    public class UseItem : InteractItems
    {
        [SerializeField] private string itemName;
        [SerializeField] private string resultMessage;
        [SerializeField] private float increseHp;
        [SerializeField] private float increseSanity;

        protected override void Init()
        {
            //이름은 os에서 따옴
            context.displayName = itemName ?? "사용아이템";
            context.promptKey = PromptKey.UseItem;
            //result.actions.Add(new PlayerHpRecovery(increseHp));
            //result.actions.Add(new PlayerSanityRecovery(increseSanity));
            //result.actions.Add(new PlaySFXAction(SFXType.UseItem));
        }
        public override InteractResult Interact(PlayerManager target)
        {

            Debug.Log("플레이어 회복");
            result.success = true;
            result.message = resultMessage;
            Destroy(gameObject);
            return result;
        }


    }

}

