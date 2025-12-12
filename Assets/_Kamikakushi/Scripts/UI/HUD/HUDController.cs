using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Inventory;

public class HUDController : MonoBehaviour
{
    public Image equippedItemIcon;

    public void SetEquippedItem(ItemData item)
    {
        if (item == null)
        {
            equippedItemIcon.sprite = null;
            equippedItemIcon.color = new Color(1, 1, 1, 0); // ≈ı∏Ì
            return;
        }

        equippedItemIcon.sprite = item.icon;
        equippedItemIcon.color = Color.white;
    }
}
