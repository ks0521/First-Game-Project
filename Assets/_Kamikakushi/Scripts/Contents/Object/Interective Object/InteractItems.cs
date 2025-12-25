using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    /* 작성 규약
     * InteractResult는 Interact() 안에서만 작성
     * Interact() 시작 시 ResetResult() 호출
     * PlayerInteract에서는 result를 수정하지 않는다
     * 게임 상태 변화는 전부 IInteractAction으로 처리
     */
    abstract public class InteractItems : MonoBehaviour, IInteractable
    {
        [Header("InteractInfo")]
        //상호작용 타입
        [SerializeField] public InteractType interactType;
        //UI나 크로스헤어 정보전달용
        [SerializeField] protected InteractContext context;
        //상호작용 결과 반환
        [SerializeField] protected InteractResult result;
        //해당 오브젝트의 상호작용 조건들

        [Header("Result Message")]
        [TextArea(2, 4)]
        [SerializeField] protected string overrideMessage;
        [SerializeField] protected string defaultMessage = "상호작용했다...";

        [SerializeField] protected string displayName;

        [SerializeField] protected IInteractionCondition[] conditions;
        [SerializeField] protected IInteractAction[] actions;
        abstract protected void Init();
        private void Awake()
        {
            //인스펙터에 부착된 조건들을 모음
            conditions = GetComponents<IInteractionCondition>();
            actions = GetComponents<IInteractAction>();
            context.displayName = string.IsNullOrEmpty(displayName) ? "아이템" : displayName;
            if (result.actions == null) result.actions = new List<IInteractAction>();
            else result.actions.Clear();

            foreach(var action in actions)
            {
                result.actions.Add(action);
            }
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

        //상호작용 아이템의 상호작용 구현
        public virtual InteractResult Interact(PlayerManager target)
        {
            ResetResult();
            result.success = true;

            result.message = string.IsNullOrEmpty(overrideMessage) ? defaultMessage : overrideMessage;

            return result;
        }
        //InteractContext 구조체 반환하여 UI부에서 크로스헤어 및 텍스트 출력
        public InteractContext GetContext() { return context; }

        public virtual void ResetResult(bool keepAction = true)
        {
            result.success = false;
            result.message = null;

        }
    }

}
