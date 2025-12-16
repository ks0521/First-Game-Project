using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InteratctContext
{
    public PromptKey promptKey; //ui에서 프롬포트 가공시 사용할 키

    public string displayName; //아이템 이름 (선택사항)

    public bool isAvailable; //현재 상호작용 가능여부
}
