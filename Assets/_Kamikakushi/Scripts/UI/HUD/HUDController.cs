using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Inventory;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;

public class HUDController : MonoBehaviour
{
    [Header("Equipped Item")]
    public Image equippedItemIcon;

    [Header("Status Bars")]
    public Image hpBar;
    public Image sanityBar;

    [Range(1f, 20f)]
    public float smoothSpeed = 8f;

    [SerializeField] PlayerEvents events;
    float targetHPFill = 1f;
    float targetSanityFill = 1f;

    private void Start()
    {

        events.PlayerStatChange += UpdateState;
    }
    private void Update()
    {
        hpBar.fillAmount = Mathf.Lerp(
            hpBar.fillAmount,
            targetHPFill,
            Time.deltaTime * smoothSpeed
        );

        sanityBar.fillAmount = Mathf.Lerp(
            sanityBar.fillAmount,
            targetSanityFill,
            Time.deltaTime * smoothSpeed
        );
    }

    public void SetEquippedItem(ItemData item)
    {
        if (item == null)
        {
            equippedItemIcon.sprite = null;
            equippedItemIcon.color = new Color(1, 1, 1, 0); // 투명
            return;
        }

        equippedItemIcon.sprite = item.icon;
        equippedItemIcon.color = Color.white;
    }
    void UpdateState(playerStat stat)
    {
        Debug.Log($"{stat.Hp},{stat.Sanity}");
        targetHPFill = stat.Hp / stat.MaxHp;
        targetSanityFill = stat.Sanity / stat.MaxSanity;
    }
}
