using _Kamikakushi.Contents.Item;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 커서 및 자원, 숨기상태 관리
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {

        //[SerializeField] Inventory inventory; - 인벤토리 클래스
        [SerializeField] public int sanity;
        [SerializeField] int playerCount;
        [SerializeField] public string handeditems; //민재님이 만들어주시면 수정
        [SerializeField] public GameObject flash;
        [SerializeField] public PlayerInventory inven;
        public HUDController hud;
        public float maxHP = 100f;
        public float currentHP = 100f;

        public float maxMP = 100f;
        public float currentMP = 100f;

        private float battery;
        [SerializeField] public bool IsHide {  get; private set; }
        void Awake()
        {
            battery = 100;
            //Cursor.lockState = CursorLockMode.Locked;
            handeditems = null;
            sanity = 100;
            Debug.Log(sanity);
            inven = GetComponent<PlayerInventory>();
        }
        private void Start()
        {
            hud.UpdateHP(currentHP, maxHP);
            hud.UpdateMP(currentMP, maxMP);
            InventoryController.Instance.OnItemEquipped+=SelectItem;
        }
        private void FixedUpdate()
        {
            if (flash.activeSelf)
            {
                battery -= 0.02f;
            }
        }
        // Update is called once per frame
        void Update()
        {
            //테스트용 코드
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                flash.SetActive(!flash.activeSelf);
            }
        }
        void SelectItem(string keyCode)
        {
            handeditems = keyCode;
        }
    }

}
