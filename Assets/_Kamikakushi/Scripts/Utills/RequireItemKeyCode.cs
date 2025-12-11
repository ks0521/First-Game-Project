using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Utills;
using UnityEngine;

public class RequireItemKeyCode : IInteractionCondition
{
    [SerializeField] private int requireKeycode;
    /// <summary>
    /// 플레이어가 장착중인 아이템이 사용가능한지 여부 확인
    /// </summary>
    /// <param name="target">플레이어의 정보</param>
    /// <returns>플레이어가 장착한 장비와 상호작용 필요한 키코드 일치여부</returns>
    public bool CanInteract(PlayerManager target)
    {
        if(target.handeditems.KeyCode == requireKeycode) return true;
        return false;
    }
}
