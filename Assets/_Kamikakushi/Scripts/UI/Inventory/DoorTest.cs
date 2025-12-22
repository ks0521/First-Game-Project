using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Inventory;

public class DoorTest : MonoBehaviour
{
    public string doorKeyCode;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        Debug.Log("문을 열려면 E를 누르세요.");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        Debug.Log("문에서 멀어졌다.");
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpen();
        }
    }

    private void TryOpen()
    {
        var inv = InventoryController.Instance;
        if (inv == null)
        {
            Debug.Log("인벤토리를 찾을 수 없습니다.");
            return;
        }

        ItemData equipped = inv.EquippedItem;

        // 장착 아이템 없음
        if (equipped == null)
        {
            Debug.Log("열쇠를 가지고 있지 않습니다.");
            return;
        }

        // 키 코드 불일치
        if (equipped.keyCode != doorKeyCode)
        {
            Debug.Log("열쇠가 맞지 않는다.");
            return;
        }

        // 성공
        Debug.Log("문이 열렸다!");

        // 열쇠 소비
        //inv.OnItemConsumed(equipped);
    }
}
