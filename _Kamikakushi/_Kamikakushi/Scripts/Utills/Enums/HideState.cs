using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Enums
{
    /// <summary>
    /// 현재 플레이어의 은신상태 여부에 대한 FSM
    /// </summary>
    public enum HideState
    {
        Idle, //숨지 않은 상태
        Entering, //숨으러 들어가는 상태 (카메라 전환, 플레이어 이동)
        Hidden, //숨어있는 상태(입력 제한 및 플레이어 isHide 변경)
        Exiting //숨어있는 곳에서 나오기(카메라 전환, 플레이어 입력상태 및 isHide 변경)
    }
}