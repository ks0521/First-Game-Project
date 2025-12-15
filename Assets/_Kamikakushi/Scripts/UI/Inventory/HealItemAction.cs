using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/ItemAction/Heal")]
public class HealItemAction : ItemAction
{
    public override void OnEquip(Player player)
    {
        Debug.Log("회복 아이템 장착됨");
    }

    public override void OnUse(Player player)
    {
        Debug.Log("체력이 회복되었습니다!");
    }
}
