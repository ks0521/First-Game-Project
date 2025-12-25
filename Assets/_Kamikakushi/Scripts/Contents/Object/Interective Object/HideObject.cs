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


        protected override void Init()
        {
            interactType = InteractType.Event;
            context.promptKey = PromptKey.Hide;
        }

    }
}