using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    abstract public class InteractItems : MonoBehaviour, IInteractable
    {
        //상호작용 타입
        [SerializeField] public InteractType interactType;
        //UI나 크로스헤어 정보전달용
        [SerializeField] protected InteractContext context;
        //상호작용 결과 반환
        [SerializeField] protected InteractResult result;
        //해당 오브젝트의 상호작용 조건들
        [SerializeField] protected IInteractionCondition[] conditions;

        abstract protected void Init();
        private void Awake()
        {
            //인스펙터에 부착된 조건들을 모음
            conditions = GetComponents<IInteractionCondition>();
            Init();
        }

        /// <summary>
        /// 상호작용 조건들을 모두 만족하는지 확인
        /// </summary>
        /// <param name="target">플레이어의 정보</param>
        /// <returns>모든 조건을 만족 시 True, 아니면 False 반환</returns>
        public virtual bool CanInteract(PlayerManager target)
        {
            foreach (IInteractionCondition condition in conditions)
            {
                if (!condition.CanInteract(target)) return false;
            }
            //모든 조건들에서 상호작용 가능하면 true 반환
            return true;
        }

        //상호작용 아이템의 상호작용 구현부(실제 자식에서 구현)
        abstract public InteractResult Interact(PlayerManager target);

        //InteractContext 구조체 반환하여 UI부에서 크로스헤어 및 텍스트 출력
        public InteractContext GetContext() { return context; }
    }

}
