using Project.Inventory;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] public int sanity;
        [SerializeField] int playerCount;
        [SerializeField] public string handeditems;
        [SerializeField] public GameObject flash;
        [SerializeField] public PlayerInventory inven;
        public HUDController hud;

        public float maxHP = 100f;
        public float currentHP = 100f;
        public float maxMP = 100f;
        public float currentMP = 100f;

        private float battery;

        void Awake()
        {
            battery = 100;
            handeditems = null;
            sanity = 100;
            // 추가
            inven = GetComponent<PlayerInventory>();
        }

        private void Start()
        {
            hud.UpdateHP(currentHP, maxHP);
            hud.UpdateMP(currentMP, maxMP);
            InventoryController.Instance.OnItemEquipped += SelectItem;
            // 추가
            InventoryController.Instance.GetInventoryItems = inven.GetItems;
        }
        // 추가
        void SelectItem(ItemData item)
        {
            if (item == null) return;

            handeditems = item.keyCode;
        }
    }
}