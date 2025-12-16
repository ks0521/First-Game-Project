using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Inventory;

public class HUDController : MonoBehaviour
{
    [Header("Equipped Item")]
    public Image equippedItemIcon;

    [Header("Status Bars")]
    public Image hpBar;
    public Image mpBar;

    [Range(1f, 20f)]
    public float smoothSpeed = 8f;

    float targetHPFill = 1f;
    float targetMPFill = 1f;

    private void Update()
    {
        hpBar.fillAmount = Mathf.Lerp(
            hpBar.fillAmount,
            targetHPFill,
            Time.deltaTime * smoothSpeed
        );

        mpBar.fillAmount = Mathf.Lerp(
            mpBar.fillAmount,
            targetMPFill,
            Time.deltaTime * smoothSpeed
        );
    }

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

    public void UpdateHP(float current, float max)
    {
        targetHPFill = current / max;
    }

    public void UpdateMP(float current, float max)
    {
        targetMPFill = current / max;
    }
}
