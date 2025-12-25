using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.InteractiveObject;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    /// <summary>
    /// 내부의 IInteractAction을 딱 한번 실행하는 액션
    /// </summary>
    public class DoorOpenAction : MonoBehaviour, IInteractAction
    {
        [SerializeField] Door target;
        //이전에 실행한적이 있었는지
        [SerializeField] bool wasActived = false;
        //한번만 실행해야하는지
        [SerializeField] bool isOnce = false;
        public void Execute(PlayerManager player, IInteractable source)
        {
            //이전에 실행했고, 한번만 실행해야 한다면 excute 막음
            if (wasActived && isOnce) return;
            if (target != null)
            {
                target.Open();
            }
            else
            {
                Debug.LogWarning("활성화 대상 없음!");
            }
        }
    }
}
