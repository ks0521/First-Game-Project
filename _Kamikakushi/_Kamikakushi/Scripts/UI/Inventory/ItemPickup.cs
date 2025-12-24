using _Kamikakushi.Contents.Player;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // 주울 아이템 데이터

    private bool playerInRange = false;
    private PlayerInventory inventoryRef;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inventoryRef = other.GetComponent<PlayerInventory>();
        playerInRange = true;

        Debug.Log("E 키로 [" + itemData.itemName + "] 줍기");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        inventoryRef = null;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        if (inventoryRef == null || itemData == null) return;

        inventoryRef.Add(itemData);

        Destroy(gameObject);
    }
}