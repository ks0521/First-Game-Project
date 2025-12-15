using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Inventory;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public string equippedKeyCode;

    public float maxHP = 100f;
    public float currentHP = 100f;

    public float maxMP = 100f;
    public float currentMP = 100f;

    public HUDController hud;

    private void Start()
    {
        hud.UpdateHP(currentHP, maxHP);
        hud.UpdateMP(currentMP, maxMP);
    }

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

        // 테스트용 H 누르면 체력 감소
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHP -= 10f;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
            hud.UpdateHP(currentHP, maxHP);

            Debug.Log("체력이 감소합니다.");
        }

        // 테스트용 J 누르면 정신력 감소
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentMP -= 10f;
            currentMP = Mathf.Clamp(currentMP, 0, maxMP);
            hud.UpdateMP(currentMP, maxMP);

            Debug.Log("정신력이 감소합니다.");
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

