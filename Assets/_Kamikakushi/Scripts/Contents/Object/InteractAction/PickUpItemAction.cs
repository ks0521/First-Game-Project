using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class PickUpItemAction : IInteractAction
    {
        private readonly ItemData data;
        public PickUpItemAction(ItemData _date)
        {
            data = _date;
        }
        public void Execute(PlayerManager player, IInteractable source)
        {
            //OS의 참조를 플레이어에게 제공
            if (player.inven.Add(data))
            {
                //IInteractable은 gameObject가 아니니까 destroy불가, 그래서 Monobehavior인지 확인 
                if (source is MonoBehaviour)
                {
                    //맞으면 MonoBehavior타입으로 형변환 후 오브젝트 삭제
                    MonoBehaviour mb = (MonoBehaviour)source;
                    if (mb != null) Object.Destroy(mb.gameObject);
                }
            }
        }
    }
}
