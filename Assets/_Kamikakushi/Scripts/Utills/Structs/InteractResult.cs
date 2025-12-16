using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Utills.Structs
{
    public struct InteractResult
    {
        public bool success; //상호작용 성공 실패
        public string message; //결과에따른 플레이어에게 출력해줄 메시지
        public Transform transform; //hide오브젝트 전용 카메라 이동 위치

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