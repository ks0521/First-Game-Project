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
        [SerializeField] public UIManager reader;
        [SerializeField] public bool isHide;
        [SerializeField] public bool CanDetected => !isHide;
        [SerializeField] public playerStat stat;
        //public HUDController hud;
        public PlayerInventory inven;
        public PlayerController controller;
        public PlayerEvents events;
        public PlayerHide hide;
        PlayerHit hit;


        void Awake()
        {
            handeditems = null;
            inven = GetComponent<PlayerInventory>();
            events = GetComponent<PlayerEvents>();
            hit = GetComponent<PlayerHit>();
            hide = GetComponent<PlayerHide>();
            controller = GetComponent<PlayerController>();
            
            stat.Hp = stat.MaxHp;
            stat.Sanity = stat.MaxSanity;
            //초기값으로 hp창 변경

            // InventoryController.Instance.OnItemEquipped+=SelectItem;
        }
        void Start()
        {
            events.OnPlayerStatChange(stat);
            StartCoroutine(UIResist());
        }
        private IEnumerator UIResist()
        {
            // UIManager가 DontDestroy로 올라올 시간을 1프레임 줌
            yield return null;

            var ui = FindObjectOfType<UIManager>(true);
            if (ui == null)
            {
                Debug.LogWarning("PlayerManager: UIManager를 찾지 못함");
                yield break;
            }
            reader = ui;
            ui.PlayerResist(this);
        }
        private void OnEnable()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.GetInventoryItems += inven.GetItems;
                InventoryController.Instance.OnItemEquipped += SelectItem;
            }
            else Debug.LogWarning("인벤토리컨트롤러 싱글톤 부재! PlayerManager - OnEnable");
            events.PlayerHitEvent += Damaged;
        }
        private void OnDisable()
        {
            if (InventoryController.Instance != null)
            {
                InventoryController.Instance.GetInventoryItems -= inven.GetItems;
                InventoryController.Instance.OnItemEquipped -= SelectItem;
            }
            else Debug.LogWarning("인벤토리컨트롤러 싱글톤 부재! PlayerManager - OndDisable");
            events.PlayerHitEvent -= Damaged;
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
                stat.Hp -= damage;
            }
            else
            {
                stat.Sanity -= damage;
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
            if (inven.Remove(handeditems) && InventoryController.Instance != null)
            {
                InventoryController.Instance.OnItemConsumed();
            }
            else Debug.LogWarning("인벤토리컨트롤러 싱글톤 부재!");
            //inventory 인스턴트에 정보전달해주기
        }
        public void HpRecovery(float recovery)
        {
            stat.Hp += recovery;
            events.OnPlayerStatChange(stat);
        }
        public void SanityRecovery(float recovery)
        {
            stat.Sanity += recovery;
            events.OnPlayerStatChange(stat);
        }

    }
}