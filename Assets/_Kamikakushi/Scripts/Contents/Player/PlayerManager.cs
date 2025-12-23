using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.UI;
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

        [SerializeField] public GameObject flash;
        //[SerializeField] InventoryController invenController;
        [SerializeField] public ItemData handeditems;
        [SerializeField] public ReadingController reader;
        [SerializeField] public bool isHide;
        [SerializeField] public bool CanDetected => !isHide;
        [SerializeField] public playerStat stat;
        //public HUDController hud;
        public PlayerInventory inven;
        public PlayerEvents events;
        public PlayerHide hide;
        PlayerHit hit;

        private float battery;

        void Awake()
        {
            battery = 100;
            handeditems = null;
            inven = GetComponent<PlayerInventory>();
            events = GetComponent<PlayerEvents>();
            hit = GetComponent<PlayerHit>();
            hide = GetComponent<PlayerHide>();
            stat.hp = stat.MaxHp;
            stat.sanity = stat.MaxSanity;
            //초기값으로 hp창 변경

            // InventoryController.Instance.OnItemEquipped+=SelectItem;
        }
        void Start()
        {
            events.OnPlayerStatChange(stat);
            InventoryController.Instance.GetInventoryItems = inven.GetItems;
            InventoryController.Instance.OnItemEquipped += SelectItem;
            events.PlayerHitEvent += Damaged;
        }

        private void OnDisable()
        {
            events.PlayerHitEvent -= Damaged;
            InventoryController.Instance.OnItemEquipped -= SelectItem;
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
        void Damaged(Vector3 target, float damage, float time, HitType type)
        {
            if (type == HitType.Physical)
            {
                stat.hp -= damage;
            }
            else
            {
                stat.sanity -= damage;
            }
            events.OnPlayerStatChange(stat);
            StartCoroutine(NoHit(time));
        }
        IEnumerator NoHit(float time)
        {
            hit.enabled = false;
            yield return new WaitForSeconds(time);
            hit.enabled = true;
        }

        // 추가
        void SelectItem(ItemData item)
        {
            if (item == null) return;
            handeditems = item;
        }
        public void UseHandedItem()
        {
            if (inven.Remove(handeditems))
            {
                InventoryController.Instance.OnItemConsumed();
            }
            //inventory 인스턴트에 정보전달해주기
        }

        public void Read(ReadableData data)
        {

        }
    }
}