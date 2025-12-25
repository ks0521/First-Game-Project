using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System;
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
        public static UIManager Instance;

        [SerializeField] public ReadingController readingController;
        [SerializeField] public CrosshairController crosshairController;
        [SerializeField] public SettingController settingController;
        [SerializeField] public InventoryController inventoryController;
        [SerializeField] public HUDController hudController;
        //현재 어떤창이 열려있는 창의 상태 확인(없음/인벤/설정/리딩)
        [SerializeField] PlayerController playerController;
        [SerializeField] public PlayerEvents playerEvents;
        [SerializeField] GameObject inventoryCanvas;
        [SerializeField] GameObject settingCanvas;
        [SerializeField] GameObject crosshair;
        [SerializeField] GameObject readingCanvas;
        [SerializeField] GameObject ScreenFadedCanvas;
        [SerializeField] UIStatus curStatus;
        public UIStatus CurStatus => curStatus;
        [SerializeField] InteractContext currentContext;

        public event Action<UIStatus> OnClose;
        //매니저에서 플레이어의 정보를 받아왔음을 알림
        public event Action OnResist;
        private UIStatus prevStatus;
        ReadableData data;
        [SerializeField] private RectTransform crosshairTransform;

        public RectTransform CrosshairTransform => crosshairTransform;
        public PlayerManager PlayerManager { get; private set; }
        public PlayerManager PlayerController { get; private set; }
        public PlayerInventory PlayerInventory { get; private set; } // 필요하면
        public PlayerInteract PlayerInteract { get; private set; }   // 필요하면

        bool allowHotKey;
        public void SetAllowHotKey(bool value) => allowHotKey = value;
        bool isClosing;
        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            //instance 활성화 후 hudcontroller 활성화하여 연결을 보장
            //hudController.enabled = true;

            inventoryCanvas?.SetActive(false);
            settingCanvas?.SetActive(false);
            readingCanvas?.SetActive(false);
            curStatus = UIStatus.GamePlay;
        }
        private void OnEnable()
        {
            //플레이어 컨텍스트 구독
            SubscribePlayerEvents();
        }
        private void OnDisable()
        {
            UnsubscribePlayerEvents();
        }

        void Update()
        {
            if (PlayerManager == null || playerEvents == null || playerController == null)
                return;

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

        //시작할때 플레이어쪽에서 해당 함수 실행해서 등록해줘야함
        public void PlayerResist(PlayerManager player)
        {
            if(player == null)
            {
                Debug.LogWarning("UI창에서 플레이어 초기 등록 실패!");
                return;
            }
            //기존 연결된 playerevent가 있으면 이벤트 구독 해제(새 등록이 왔기 때문)
            UnsubscribePlayerEvents();
            PlayerManager = player;
            playerEvents = player.events;
            playerController = player.controller;
            PlayerInventory = player.inven;
            //새로 등록된 playerevent에 이벤트 등록
            SubscribePlayerEvents();
            OnResist?.Invoke();
        }
        void SubscribePlayerEvents()
        {
            if (playerEvents != null)
            {
                playerEvents.GetInteractContext += OnFound;
                playerEvents.RaycastOut += OnLost;
                playerEvents.GetInteractResult += OnInteractResult;
            }
        }
        void UnsubscribePlayerEvents()
        {
            if (playerEvents != null)
            {
                playerEvents.GetInteractContext -= OnFound;
                playerEvents.RaycastOut -= OnLost;
                playerEvents.GetInteractResult -= OnInteractResult;
            }
            
        }
        //창을 여닫기 전 공통으로 하는 작업
        void EnterUIMode()
        {
            if (playerController == null || playerEvents == null) return;

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
            if (playerController == null || playerEvents == null) return;
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
        public void OpenSettings()
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
            if (curStatus == UIStatus.GamePlay) return;
            if (isClosing) return;
            isClosing = true;
            //이벤트 발송용 UIStatus 저장
            prevStatus = curStatus;
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
            isClosing = false;
            //어떤 종류의 UI가 종료되었는지 발행
            OnClose?.Invoke(prevStatus);
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
            Debug.Log(currentContext.displayName);
            if (currentContext.displayName == null) return;
            crosshairController.ShowInteractResult(result, currentContext);
        }
    }

}
