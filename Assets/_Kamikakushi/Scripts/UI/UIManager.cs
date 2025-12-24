using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace _Kamikakushi.Contents.UI
{
    public enum UIStatus
    {
        GamePlay, //게임 플레이중
        Inventory, //인벤토리
        Setting, //설정창
        ObjectReader // 오브젝트 읽기 기능
    }
    /// <summary>
    /// UI창 메인 허브. 창을 여닫고 timescale 및 cusorlock 관리
    /// 나머지 세부 실행은 각 컨트롤러에 위임
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        static UIManager instance;

        [SerializeField]ReadingController readingController;
        [SerializeField] CrosshairController crosshairController;
        [SerializeField] SettingController settingController;
        [SerializeField] InventoryController inventoryController;
        //현재 어떤창이 열려있는 창의 상태 확인(없음/인벤/설정/리딩)
        [SerializeField] PlayerController playerController;
        [SerializeField] PlayerEvents playerEvents;
        [SerializeField] GameObject inventoryCanvas;
        [SerializeField] GameObject settingCanvas;
        [SerializeField] GameObject crosshair;
        [SerializeField] GameObject readingCanvas;
        [SerializeField] UIStatus curStatus;
        [SerializeField] InteractContext currentContext;

        ReadableData data;
        void Awake()
        {
            inventoryCanvas?.SetActive(false);
            settingCanvas?.SetActive(false);
            readingCanvas?.SetActive(false);
            curStatus = UIStatus.GamePlay;
        }
        private void OnEnable()
        {
            //플레이어 컨텍스트 구독
            playerEvents.GetInteractContext += OnFound;
            playerEvents.RaycastOut += OnLost;
            playerEvents.GetInteractResult += OnInteractResult;
        }
        private void OnDisable()
        {
            //비활성화시 구독해제
            if (playerEvents == null) return;
            playerEvents.GetInteractContext -= OnFound;
            playerEvents.RaycastOut -= OnLost;
            playerEvents.GetInteractResult -= OnInteractResult;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (curStatus == UIStatus.Inventory)
                {
                    ExitUIMode();
                    CloseCurrent();
                }
                else if (curStatus == UIStatus.GamePlay)
                {
                    EnterUIMode();
                    OpenInventory();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (curStatus == UIStatus.GamePlay)
                {
                    EnterUIMode();
                    OpenSettings();
                }
                else
                {
                    CloseCurrent();
                }
            }
        }

        //창을 여닫기 전 공통으로 하는 작업
        void EnterUIMode()
        {
            //플레이어 조작 비활성화
            playerController.enabled = false;
            //크로스헤어 비활성화
            crosshair?.SetActive(false);
            playerEvents.GetInteractContext -= OnFound;
            playerEvents.RaycastOut -= OnLost;
            playerEvents.GetInteractResult -= OnInteractResult;
            //커서 잠금해제
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //시간 정지
            Time.timeScale = 0;
        }
        void ExitUIMode()
        {
            //플레이어 조작 활성화
            playerController.enabled = true;
            //크로스헤어 활성화
            playerEvents.GetInteractContext += OnFound;
            playerEvents.RaycastOut += OnLost;
            playerEvents.GetInteractResult += OnInteractResult;
            crosshair?.SetActive(true);
            crosshairController.SetDefault();

            //커서 잠금해제
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //시간정지 해제
            Time.timeScale = 1;
        }
        void OpenInventory()
        {
            if (curStatus != UIStatus.GamePlay)
            {
                Debug.Log($"{curStatus}창 열려있음");
                return;
            }
            inventoryController.SyncInventory();
            inventoryCanvas.SetActive(true);
            curStatus = UIStatus.Inventory;
            Debug.Log($"{curStatus}창 열기");
        }
        void OpenSettings()
        {
            if (curStatus != UIStatus.GamePlay)
            {
                Debug.Log($"{curStatus}창 열려있음");
                return;
            }
            settingCanvas.SetActive(true);
            curStatus = UIStatus.Setting;
            Debug.Log($"{curStatus}창 열기");
        }

        public void OpenReading(ReadableData data)
        {
            if (curStatus != UIStatus.GamePlay)
            {
                Debug.Log($"{curStatus}창 열려있음");
                return;
            }
            EnterUIMode();
            readingCanvas.SetActive(true);
            readingController.SetupData(data);
            curStatus = UIStatus.ObjectReader;
            Debug.Log($"{curStatus}창 열기");
        }
        public void CloseCurrent()
        {
            switch (curStatus)
            {
                case UIStatus.Inventory:
                    inventoryCanvas.SetActive(false);
                    break;
                case UIStatus.Setting:
                    settingCanvas.SetActive(false);
                    break;
                case UIStatus.ObjectReader:
                    readingCanvas.SetActive(false);
                    break;
            }
            ExitUIMode();
            curStatus = UIStatus.GamePlay;
            Debug.Log($"창 종료, 현재 {curStatus}상태");
        }

        private void OnFound(InteractContext context)
        {
            currentContext = context;
            crosshairController.ShowPrompt(context);
        }
        private void OnLost()
        {
            currentContext = default;
            crosshairController.SetDefault();
        }
        private void OnInteractResult(InteractResult result)
        {
            if (currentContext.displayName == null) return;
            crosshairController.ShowInteractResult(result, currentContext);
        }
    }

}
