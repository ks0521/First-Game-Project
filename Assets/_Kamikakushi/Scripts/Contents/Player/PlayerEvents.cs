using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerEvents : MonoBehaviour
    {
        /// <summary>
        /// 아이템을 주웠을 때 발생, 아이템 정보를 인자로 준다
        /// </summary>
        public event Action<ItemData> ItemPickUp;
        /// <summary>
        /// 플레이어 피격 이벤트- 공격자 위치, 받은 데미지와 피격 지속시간 인자로 전달
        /// </summary>
        public event Action<Vector3, float,float,HitType> PlayerHitEvent;
        /// <summary>
        /// 플레이어가 숨을 수 있는 오브젝트에 들어갈 때 발생
        /// 인자는 카메라 이동 위치
        /// </summary>
        public event Action<Transform> PlayerHideInEvent;
        /// <summary>
        /// 플레이어가 숨어있다가 밖으로 나올 때 발생
        /// 인자는 숨기 전 위치
        /// </summary>
        public event Action PlayerHideOutEvent;
        /// <summary>
        /// 플레이어가 회복 / 피격 등으로 체력,정신력에 변화가 생길 때 발생
        /// </summary>
        public event Action<playerStat> PlayerStatChange;
        /// <summary>
        /// InteractableObject를 레이캐스트 성공시 발생, 
        /// 탐지한 오브젝트의 정보를 전달
        /// </summary>
        public event Action<InteractContext> GetInteractContext;
        /// <summary>
        /// 플레이어가 상호작용 버튼을 눌렀을 때 발생, 상호작용 결과를 전달
        /// </summary>
        public event Action<InteractResult> GetInteractResult;
        public event Action<IInteractable> GetInteractable;
        //public event Action<interactAttemptinfo> info;
        /// <summary>
        /// InteractableObject에서 레이캐스트가 떨어졌을 시 발생
        /// </summary>
        public event Action RaycastOut;
        public event Action<float> CameraHold;

        public void OnHit(Vector3 pos, float damage, float time, HitType type)
        {
            PlayerHitEvent?.Invoke(pos, damage, time, type);
        }
        
        public void OnFindInteractable(IInteractable interactable)
        {
            GetInteractable?.Invoke(interactable);
        }
        public void OnRaycastEnter(InteractContext context)
        {
            Debug.Log("상호작용 탐지 성공");
            GetInteractContext?.Invoke(context);
        }

        public void OnRaycastOut()
        {
            Debug.Log("시선 떨어짐");
            RaycastOut?.Invoke();
        }
        public void OnInteract(InteractResult result)
        {
            GetInteractResult?.Invoke(result);
        }
        public void OnPickUp(ItemData data)
        {
            ItemPickUp?.Invoke(data);
        }
        public void OnPlayerStatChange(playerStat Stat)
        {
            PlayerStatChange?.Invoke(Stat);
        }
        public void OnHideEnter(Transform transform)
        {
            PlayerHideInEvent?.Invoke(transform);
        }
        public void OnHideOut()
        {
            PlayerHideOutEvent?.Invoke();
        }

        public void OnCameraHold(float time)
        {
            CameraHold?.Invoke(time);
        }
    }
}