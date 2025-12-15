using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Inventory;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public string equippedKeyCode;

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0f, v).normalized;

        if (dir.magnitude > 0f)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseEquippedItemTest();
        }
    }

    public void PickUp(ItemData item)
    {
        if (item == null)
        {
            return;
        }

        if (InventoryController.Instance == null)
        {
            return;
        }

        if (InventoryController.Instance.AddItem(item))
        {
        }
        else
        {
        }
    }

    private void UseEquippedItemTest()
    {
        var inv = InventoryController.Instance;
        if (inv == null) return;

        // 현재 장착된 아이템
        ItemData equipped = inv.EquippedItem;
        if (equipped == null)
        {
            Debug.Log("사용할 아이템이 없습니다.");
            return;
        }

        Debug.Log("[" + equipped.itemName + "] 아이템이 사용되었습니다.");

        // 인벤토리에서 삭제
        inv.RemoveItem(equipped);
        inv.OnItemConsumed(equipped);
    }

    public void ConsumeEquippedKey()
    {
        if (string.IsNullOrEmpty(equippedKeyCode))
            return;

        Debug.Log("열쇠 소비됨 : " + equippedKeyCode);

        var inv = InventoryController.Instance;
        if (inv != null && inv.EquippedItem != null)
        {
            ItemData usedItem = inv.EquippedItem;
            inv.RemoveItem(usedItem);
            inv.OnItemConsumed(usedItem);
        }
    }
}

