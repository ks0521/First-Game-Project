using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using _Kamikakushi.Utills.Structs;

public class WaterScript : InteractItems
{
    public override InteractResult Interact(PlayerManager target)
    {
        //마시면 정신력을 회복시켜주는 오브젝트
        InteractResult result;
        result.success = true;
        result.message = "정신이 맑아진 기분이 든다...";
        result.transform = null;
        target.stat.sanity += 20;
        return result;
    }
    protected override void Init()
    {
        //인터페이스의 배열이기때문에 GetComponents 사용
        conditions = GetComponents<IInteractionCondition>();
        interactType = InteractType.Event;
        context.promptKey = PromptKey.UseItem;
        context.displayName = "물";
    }

}
