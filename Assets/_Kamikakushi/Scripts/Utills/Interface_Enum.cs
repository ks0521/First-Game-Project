using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills
{
    public interface IHittable
    {
        //플레이어에게만 달리는 피격 인터페이스
        public void Hit(Vector3 position);
    }
    public interface IInteractable
    {
        //상호작용 가능한 아이템에 달리는 인터페이스
        public bool CanInteract(PlayerManager target);
        public void Interact(PlayerManager target);
    }
    public interface IInteractionCondition
    {
        //상호작용 조건부분 분리
        //해당 인터페이스를 상속받는 클래스는 다양한 조건설정 가능
        //player가 장착한 아이템 keycode뿐만이 아니라 체력, 진척도 등...
        bool CanInteract(PlayerManager target);
    }
    public interface IUseable
    {
        //사용 가능한 아이템에 달리는 인터페이스
        public void Use(GameObject target);
    }
    public enum layerMask
    {
        Player = 1 << 6,
        Monster = 1 << 8,
        MonsterDetector = 1 << 9,
        Item = 1 << 11,
        InteractionObject = 1 << 12
    }
}

