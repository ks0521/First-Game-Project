using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class InspectItem : InteractItems
    {
        protected override void Init()
        {
            interactType = InteractType.Event;
            context.promptKey = PromptKey.Inspect;
        }
    }
}