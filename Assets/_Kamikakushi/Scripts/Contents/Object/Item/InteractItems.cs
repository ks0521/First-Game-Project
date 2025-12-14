using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    abstract public class InteractItems : MonoBehaviour
    {
        [SerializeField]protected string itemName;
        [SerializeField]protected string itemDescription;
        [SerializeField]public InteractType interactType;
        protected IInteractionCondition[] conditions;

        abstract protected void Init();
        private void Awake()
        {
            conditions = GetComponents<IInteractionCondition>();
            Init();
        }
        /// <summary>
        /// conditions를 모아서 상호작용 가능여부 반환
        /// 만일 오브젝트가 더 자세한 조건판단이 필요할 시
        /// (문이 열려있으면 닫히고 닫혀있으면 열리는 등...) override
        /// </summary>
        /// <param name="target">player</param>
        /// <returns>상호작용 가능하면 true, 제한되면 false</returns>
        public virtual bool CanInteract(PlayerManager target)
        {
            foreach (IInteractionCondition condition in conditions)
            {
                if (!condition.CanInteract(target)) return false;
            }
            return true;
        }

    }

}
