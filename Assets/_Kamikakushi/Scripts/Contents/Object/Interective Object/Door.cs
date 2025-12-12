using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Utills;
using _Kamikakushi.Contents.Player;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class Door : MonoBehaviour, IInteractable
    {
        public IInteractionCondition[] conditions;

        public void Awake()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            conditions = GetComponents<IInteractionCondition>();
        }
        public bool CanInteract(PlayerManager target)
        {
            foreach (IInteractionCondition condition in conditions)
            {
                if (!condition.CanInteract(target)) return false;
            }
            return true;
        }

        public void Interact(PlayerManager target)
        {
            //상호작용(문이 열린다 / 길이 나온다 / 이벤트 발생 등등....)
            Debug.Log("상호작용 했습니다.");
        }

        // Start is called before the first frame update

    }

}
