using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Inventory
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Project/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [TextArea(2, 4)]
        public string explain;

        // 프리팹으로 들어갈 수 있음
        public GameObject prefab;
    }
}
