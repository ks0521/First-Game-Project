using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using _Kamikakushi.Contents.Player;

namespace Project.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController Instance; // 싱글턴

        [Header("Panel")]
        public GameObject inventoryCanvas;

        [Header("Left Area")]
        public Transform leftArea;

        [Header("Right Area")]
        public Image selectedItemIcon;
        public TMP_Text selectedItemName;
        public TMP_Text selectedItemEx;

        public Button selectButton;
        public Button exitButton;

        [Header("Initial items")]
        public List<ItemData> initialItems = new List<ItemData>();

        // 델리게이트 타입
        public delegate List<ItemData> InventoryDelegate();
        public InventoryDelegate GetInventoryItems; // 외부에서 할당 가능

        private List<SlotUI> slotUIs = new List<SlotUI>();
        [SerializeField] private List<ItemData> currentItems = new List<ItemData>();
        private ItemData currentSelected = null;

        public bool isOpen = false;
        [SerializeField] PlayerController playerController;

        public ItemData equippedItem;
        public ItemData EquippedItem
        {
            get => equippedItem;
            set => equippedItem = value;
        }

        public HUDController hudController;

        public event Action<ItemData> OnItemEquipped;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            // 인벤토리 패널은 시작 시 비활성화
            if (inventoryCanvas != null) inventoryCanvas.SetActive(false);

            // leftArea의 SlotUI를 자동 수집
            slotUIs.Clear();
            if (leftArea != null)
            {
                foreach (Transform child in leftArea)
                {
                    var s = child.GetComponent<SlotUI>();
                    if (s != null)
                        slotUIs.Add(s);
                }
            }

            // select, exit 버튼 연결
            if (selectButton != null) selectButton.onClick.AddListener(OnSelectButton);
            if (exitButton != null) exitButton.onClick.AddListener(CloseInventory);

            // 초기 아이템 불러오기 (델리게이트가 있으면 델리게이트, 없으면 inspector에 넣은 initialItems)
            if (GetInventoryItems != null)
                currentItems = GetInventoryItems();
            else
                currentItems = new List<ItemData>(initialItems);

            RefreshSlots();
            ClearRightPanel();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }
        //item장착
        public void EquipItem(ItemData item)
        {
            if (item == null) return;

            EquippedItem = item;

            // 게임플레이 HUD에 표시
            hudController?.SetEquippedItem(item);

            OnItemEquipped?.Invoke(item);
            Debug.Log("키 코드 장착됨 : " + item.keyCode);
        }

        //장착중인 아이템 사용시 HUD창 초기화
        public void OnItemConsumed()
        {
            EquippedItem = null;
            hudController?.SetEquippedItem(null);
        }

        // 인벤토리 전체 가져오기
        public List<ItemData> GetCurrentItems()
        {
            return new List<ItemData>(currentItems);
        }


        //인벤토리 여닫기
        public void ToggleInventory()
        {
            if (!isOpen) OpenInventory();
            else CloseInventory();
        }
        public void OpenInventory()
        {
            // 델리게이트가 있으면 최신 데이터 받기
            if (GetInventoryItems != null)
                currentItems = GetInventoryItems();

            playerController.enabled = false;
            RefreshSlots();
            //인벤토리 열때 아이템 설명창 싹 초기화
            ClearRightPanel();
            if (inventoryCanvas != null) inventoryCanvas.SetActive(true);
            isOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        public void CloseInventory()
        {
            if (inventoryCanvas != null) inventoryCanvas.SetActive(false);
            isOpen = false;

            playerController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }


        // SlotUI에서 클릭 시 호출됨
        public void OnSlotClicked(SlotUI slot, ItemData item)
        {
            if (item == null)
            {
                ClearRightPanel();
                return;
            }
            Debug.Log($"{item.icon}");
            currentSelected = item;

            selectedItemIcon.sprite = item.icon;
            selectedItemIcon.color = Color.white;
            selectedItemName.text = item.itemName;
            selectedItemEx.text = item.explain;
        }

        // Select 버튼 눌렀을 때
        private void OnSelectButton()
        {
            if (currentSelected != null)
            {
                EquipItem(currentSelected);
                Debug.Log("[" + currentSelected.itemName + "] 가 장착되었습니다.");
            }
            else
            {
                Debug.Log("장착할 아이템이 없습니다.");
            }

        }
        void RefreshSlots()
        {
            // 슬롯 수 만큼 채움, 없으면 Clear
            for (int i = 0; i < slotUIs.Count; i++)
            {
                if (i < currentItems.Count)
                    slotUIs[i].SetItem(currentItems[i]);
                else
                    slotUIs[i].Clear();
            }
        }
        void ClearRightPanel()
        {
            currentSelected = null;
            if (selectedItemIcon != null) { selectedItemIcon.sprite = null; selectedItemIcon.color = new Color(1, 1, 1, 0); }
            if (selectedItemName != null) selectedItemName.text = "";
            if (selectedItemEx != null) selectedItemEx.text = "";
        }
    }
}