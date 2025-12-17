using _Kamikakushi.Contents.Item;
using _Kamikakushi.Utills.Interfaces;
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
        [SerializeField] public int sanity;
        [SerializeField] int playerCount;
        [SerializeField] public string handeditems;
        [SerializeField] public GameObject flash;
        [SerializeField] public PlayerInventory inven;
        [SerializeField] private PlayerEvents events;
        [SerializeField] private PlayerHide hide;
        [SerializeField] public bool isHide;

        public HUDController hud;

        public float maxHP = 100f;
        public float currentHP = 100f;
        public float maxMP = 100f;
        public float currentMP = 100f;

        private float battery;

        public bool CanDetected => !isHide;

        void Awake()
        {
            battery = 100;
            handeditems = null;
            sanity = 100;
            // 추가
            inven = GetComponent<PlayerInventory>();
        }

        private void Start()
        {
            InventoryController.Instance.OnItemEquipped += SelectItem;
            // 추가
            InventoryController.Instance.GetInventoryItems = inven.GetItems;
           // hud.UpdateHP(currentHP, maxHP);
           // hud.UpdateMP(currentMP, maxMP);
           // InventoryController.Instance.OnItemEquipped+=SelectItem;
        }
        // 추가
        void SelectItem(ItemData item)
        {
            if (item == null) return;

            handeditems = item.keyCode;
        }
    }
}