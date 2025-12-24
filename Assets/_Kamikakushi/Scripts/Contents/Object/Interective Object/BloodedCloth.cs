using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class BloodedCloth : InteractItems
    {
        [Header("Ending Settings")]
        [SerializeField]
        [Tooltip("이동하고 싶은 엔딩 맵 선택")]
        private Map endingMap;

        [SerializeField]
        [Tooltip("씬 전환 전 대기 시간 (초)")]
        private float delayTime = 3.0f;

        [Header("Text Settings")]
        [SerializeField]
        [TextArea(2, 4)]
        [Tooltip("기본 문구 대신 사용할 텍스트 (비어있으면 기본값 사용)")]
        private string overrideResultText;

        private bool isTriggered = false;

        protected override void Init()
        {
            interactType = InteractType.Event;
            context.promptKey = PromptKey.Inspect;
            context.displayName = "피묻은 천";
        }

        public override InteractResult Interact(PlayerManager target)
        {
            result.message = string.IsNullOrEmpty(overrideResultText)
                ? "피가 말라붙어 있는 더러운 천이다... 정신이 아득해진다."
                : overrideResultText;

            result.success = true;

            // 2. 이벤트 중복 방지 및 씬 전환 시작
            if (!isTriggered)
            {
                isTriggered = true;
                // 코루틴을 통해 플레이어가 글을 읽을 시간을 줌
                StartCoroutine(DelayedEndingProcess(delayTime));
            }

            return result;
        }
        private IEnumerator DelayedEndingProcess(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (GameManagers.instance != null)
            {
                GameManagers.instance.LoadScene((int)endingMap);
            }
            else
            {
                Debug.LogError("GameManagers instance를 찾을 수 없습니다. 현재 씬에 매니저가 있는지 확인하세요.");
            }
        }
    }
}