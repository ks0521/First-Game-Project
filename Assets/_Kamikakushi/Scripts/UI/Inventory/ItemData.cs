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

        public GameObject prefab;
        public ItemAction itemAction;
    }
}
