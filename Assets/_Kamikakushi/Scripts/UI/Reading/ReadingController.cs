using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Kamikakushi.Contents.UI
{
    public class ReadingController : MonoBehaviour
    {
        [Header("인스펙터에서 할당 필요")]
        [Tooltip("리딩캔버스의 Head 텍스트")]
        [SerializeField] TextMeshProUGUI head;
        [Tooltip("리딩캔버스의 Context 텍스트")]
        [SerializeField] TextMeshProUGUI context;
        [Tooltip("리딩캔버스의 Image 오브젝트")]
        [SerializeField] Image readingImage;
        [Tooltip("캔버스 자신")]
        [SerializeField] CanvasGroup group;
        [Header("자동할당")]
        [SerializeField] ReadableData data;

        bool isOpen;
        public bool IsOpen => isOpen;
        private void Awake()
        {
            SetUp(false);
            isOpen = false;
        }
        void Start()
        {

        }
        void SetUp(bool on)
        {
            //on이면 창 보임, off면 안보임
            group.alpha = on ? 1f : 0f;
            //on이면 상호작용 가능
            group.interactable = on;
            //on이면 마우스 클릭 가능
            group.blocksRaycasts = on;
        }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && IsOpen)
            {
                Close();
            }
        }
        public void Open(ReadableData _data)
        {
            data = _data;
            isOpen = true;
            SetUp(true);
            head.text = data.title;
            context.text = data.body.TrimEnd();

            if (readingImage == null)
            {
                Debug.LogWarning("Image 슬롯이 할당되지 않았습니다!");
                return;
            }
            if(data.images == null)
            {
                readingImage.sprite = null;
                readingImage.gameObject.SetActive(false);
            }
            else
            {
                readingImage.sprite = data.images;
                readingImage.gameObject.SetActive(true);
            }
            //게임 정지
            Time.timeScale = 0;
        }
        void Close()
        {
            isOpen = false;
            SetUp(false);
        }
    }

}
