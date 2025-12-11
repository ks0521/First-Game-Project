using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Utills;
using _Kamikakushi.Contents.Player;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeReference] private IInteractionCondition condition;
    public bool CanInteract(PlayerManager target)
    {
        if (condition == null) return true;
        //이렇게 하는 이유는 IInteractionCondition 변수의 확장성을 위함
        //현재는 keycode 필요 / 불필요만 있지만 나중에 체력이나 진행도 등의 조건 판정문을 만들고
        //필요한 조건만 넣으면 되니까 (전략 패턴 구현)
        return condition.CanInteract(target);
    }

    public void Interact(PlayerManager target)
    {
        //상호작용(문이 열린다 / 길이 나온다 / 이벤트 발생 등등....)
        Debug.Log("상호작용 했습니다.");
    }

    // Start is called before the first frame update

}
