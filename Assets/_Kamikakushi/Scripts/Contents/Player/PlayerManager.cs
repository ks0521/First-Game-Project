using _Kamikakushi.Contents.Item;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 커서 및 자원, 숨기상태 관리
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {

        //[SerializeField] Inventory inventory; - 인벤토리 클래스
        [SerializeField] public int sanity;
        [SerializeField] int playerCount;
        [SerializeField] public ItemScript handeditems;
        [SerializeField] public bool IsHide {  get; private set; }
        void Awake()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            sanity = 100;
            Debug.Log(sanity);
        }

        // Update is called once per frame
        void Update()
        {
            //테스트용 코드
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            
        }
    }

}
