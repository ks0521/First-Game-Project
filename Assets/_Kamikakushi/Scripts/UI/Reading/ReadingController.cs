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

        public void SetupData(ReadableData _data)
        {
            data = _data;
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
        }

    }

}
