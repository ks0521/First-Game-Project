using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        public Image itemImage;
        public Image outlineImage;
        public Button slotButton;

        private ItemData myItem;

        private void Awake()
        {
            // 버튼이 할당되어 있지 않다면 같은 오브젝트의 Button 시도
            if (slotButton == null)
                slotButton = GetComponent<Button>();

            if (slotButton != null)
                slotButton.onClick.AddListener(OnClickSlot);
        }

        public void SetItem(ItemData item)
        {
            myItem = item;
            if (item != null && item.icon != null)
            {
                itemImage.sprite = item.icon;
                itemImage.color = Color.white;
            }
            else
            {
                itemImage.sprite = null;
                itemImage.color = new Color(1, 1, 1, 0); // 투명
            }
        }

        public void Clear()
        {
            myItem = null;
            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0);
        }

        void OnClickSlot()
        {
            // 클릭 시 InventoryController에게 알리기
            InventoryController.Instance?.OnSlotClicked(this, myItem);
        }

        // 외부에서 필요하면 현재 아이템을 가져올 수 있게
        public ItemData GetItem() => myItem;
    }
}
