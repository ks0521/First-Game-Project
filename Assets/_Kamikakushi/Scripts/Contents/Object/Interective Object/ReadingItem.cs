using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class ReadingItem : InteractItems
    {
        protected override void Init()
        {
            interactType = InteractType.Event;
            //아이템 타입 설정(읽는 아이템은 inspect 고정)
            context.promptKey = PromptKey.Read;
        }
    }
}
