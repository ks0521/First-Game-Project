using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Structs
{
    [Serializable]
    public struct InteractResult
    {
        public bool success; //상호작용 성공 실패
        public string message; //결과에따른 플레이어에게 출력해줄 메시지
        //각 오브젝트에서 발생시켜야 할 액션 모음
        public List<IInteractAction> actions;
        public static InteractResult Success(string message)
        {
            return new InteractResult
            {
                success = true,
                message = message
            };
        }
        public static InteractResult Fail(string message)
        {
            return new InteractResult
            {
                success = false,
                message = message
            };
        }
    }
}