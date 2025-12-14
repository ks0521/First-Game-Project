using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class WaterScript : InteractItems, IInteractable
{
    public bool Interact(PlayerManager target)
    {
        //마시면 정신력을 회복시켜주는 오브젝트
        target.sanity += 20;
        return true;
    }
    protected override void Init()
    {
        //인터페이스의 배열이기때문에 GetComponents 사용
        conditions = GetComponents<IInteractionCondition>();
        interactType = InteractType.Event;
    }

}
