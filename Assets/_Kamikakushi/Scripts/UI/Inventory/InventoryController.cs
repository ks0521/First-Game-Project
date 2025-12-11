using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
        private List<ItemData> currentItems = new List<ItemData>();
        private ItemData currentSelected = null;

        private bool isOpen = false;

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

            RefreshSlots();
            if (inventoryCanvas != null) inventoryCanvas.SetActive(true);
            isOpen = true;
        }

        public void CloseInventory()
        {
            if (inventoryCanvas != null) inventoryCanvas.SetActive(false);
            isOpen = false;
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

        // SlotUI에서 클릭 시 호출됨
        public void OnSlotClicked(SlotUI slot, ItemData item)
        {
            if (item == null)
            {
                // 빈 슬롯 클릭하면 우측 클리어 되도록
                ClearRightPanel();
                return;
            }

            currentSelected = item;

            if (selectedItemIcon != null && item.icon != null)
            {
                selectedItemIcon.sprite = item.icon;
                selectedItemIcon.color = Color.white;
            }

            if (selectedItemName != null) selectedItemName.text = item.itemName;
            if (selectedItemEx != null) selectedItemEx.text = item.explain;
        }

        // Select 버튼 눌렀을 때 디버그 출력
        private void OnSelectButton()
        {
            if (currentSelected != null)
            {
                Debug.Log("아이템이 선택되었습니다.");
            }
            else
            {
                Debug.Log("장착할 아이템이 없습니다.");
            }
        }
    }
}
