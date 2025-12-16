using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;
using _Kamikakushi.Utills.Enums;

namespace _Kamikakushi.Utills.Structs
{
    //UI 크로스헤어 변경 및 하이라이트를 위한 구조체
    public struct InteractContext
    {
        public PromptKey promptKey; //ui에서 프롬포트 가공시 사용할 키
        public string displayName; //아이템 이름 (선택사항)
    }
}
