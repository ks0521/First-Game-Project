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

        protected override void Init()
        {
            //이름은 os에서 따옴
            context.promptKey = PromptKey.UseItem;
            //result.actions.Add(new PlayerHpRecovery(increseHp));
            //result.actions.Add(new PlayerSanityRecovery(increseSanity));
            //result.actions.Add(new PlaySFXAction(SFXType.UseItem));
        }



    }

}

