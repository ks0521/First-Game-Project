using _Kamikakushi.Contents.Item;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 커서 및 자원, 숨기상태 관리
    /// </summary>
    public class PlayerManager : MonoBehaviour, IDetectable
    {

        //[SerializeField] Inventory inventory; - 인벤토리 클래스
        [SerializeField] public GameObject flash;
        [SerializeField] public PlayerInventory inven;
        [SerializeField] private PlayerEvents events;
        [SerializeField] private PlayerHide hide;
        [SerializeField] int playerCount;
        [SerializeField] public string handeditems; //민재님이 만들어주시면 수정
        [SerializeField] public bool isHide;
        [SerializeField] public playerStat stat;
        public HUDController hud;

        private float battery;
        public bool CanDetected => !isHide;

        void Awake()
        {
            battery = 100;
            //Cursor.lockState = CursorLockMode.Locked;
            handeditems = null;
            inven = GetComponent<PlayerInventory>();
            events = GetComponent<PlayerEvents>();
            stat.hp = stat.MaxHp;
            stat.sanity = stat.MaxSanity;
            //초기값으로 hp창 변경

           // InventoryController.Instance.OnItemEquipped+=SelectItem;
        }
        void Start()
        {
            events.OnPlayerStatChange(stat);
            events.PlayerHitEvent += Damaged;
            events.PlayerStatChange += HitTest;
        }
        private void FixedUpdate()
        {
            if (flash.activeSelf)
            {
                battery -= 0.02f;
            }
        }
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                flash.SetActive(!flash.activeSelf);
            }
        }
        void Damaged(float damage, float time, HitType type)
        {
            if(type == HitType.Physical)
            {
                stat.hp -= damage;
            }
            else
            {
                stat.sanity -= damage;
            }
            events.OnPlayerStatChange(stat);
        }
        void HitTest(playerStat stat)
        {
            Debug.Log($"{stat.hp}, {stat.sanity}");
        }
    }

}
