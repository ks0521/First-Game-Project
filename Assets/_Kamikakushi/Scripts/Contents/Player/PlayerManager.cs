using _Kamikakushi.Contents.Item;
using _Kamikakushi.Utills.Interfaces;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public struct playerStat
    {
        public int hp;
        public int sanity;
        public int maxHp => 100;
        public int maxSanity => 100;
    }
    /// <summary>
    /// 커서 및 자원, 숨기상태 관리
    /// </summary>
    public class PlayerManager : MonoBehaviour, IDetectorble
    {

        //[SerializeField] Inventory inventory; - 인벤토리 클래스
        [SerializeField] public playerStat stat;
        [SerializeField] int playerCount;
        [SerializeField] public string handeditems; //민재님이 만들어주시면 수정
        [SerializeField] public GameObject flash;
        [SerializeField] public PlayerInventory inven;
        [SerializeField] private PlayerEvents events;
        public HUDController hud;
        public float currentHP = 100f;

        public float currentMP = 100f;

        private float battery;
        [SerializeField] public bool isHide;

        public bool CanDetected => !isHide;

        void Awake()
        {
            battery = 100;
            //Cursor.lockState = CursorLockMode.Locked;
            handeditems = null;
            stat.hp = stat.sanity = 100;
            inven = GetComponent<PlayerInventory>();
            events = GetComponent<PlayerEvents>();
        }
        private void Start()
        {
            events.OnPlayerStatChange(stat);
            hud.UpdateHP(stat.hp, stat.maxHp);
            hud.UpdateMP(stat.sanity, stat.maxSanity);
            InventoryController.Instance.OnItemEquipped += SelectItem;
        }
        private void FixedUpdate()
        {
            if (flash.activeSelf)
            {
                battery -= 0.02f;
            }
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                flash.SetActive(!flash.activeSelf);
            }
        }
        void SelectItem(string keyCode)
        {
            handeditems = keyCode;
        }
    }

}
