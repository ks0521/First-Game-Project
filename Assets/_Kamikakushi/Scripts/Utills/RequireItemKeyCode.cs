using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

public class RequireItemKeyCode : MonoBehaviour,IInteractionCondition
{
    [SerializeField] private string requireKeycode;
    /// <summary>
    /// 플레이어가 장착중인 아이템이 사용가능한지 여부 확인 
    /// </summary>
    /// <param name="target">플레이어의 정보</param>
    /// <returns>플레이어가 장착한 장비와 상호작용 필요한 키코드 일치여부</returns>
    public bool CanInteract(_Kamikakushi.Contents.Player.PlayerManager target)
    {
        if (target.handeditems == null)
        {
            Debug.Log("조건 확인 : 플레이어가 장착중인 아이템 없음");
            return false;
        }
        if (target.handeditems.keyCode == requireKeycode )
        {
            Debug.Log("조건 확인 : 플레이어가 알맞은 아이템 가지고 있음, 상호작용");
            return true;
        }
        Debug.Log("조건 확인 : 플레이어가 알맞은 아이템 장착하지 않음");
        return false;
    }
}
