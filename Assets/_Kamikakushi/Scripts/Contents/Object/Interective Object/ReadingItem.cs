using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

namespace _Kamikakushi.Contents.InteractiveObject
{
    public class ReadingItem : InteractItems
    {
        [SerializeField] ReadableData data;
        [SerializeField]
        [TextArea(2, 4)]
        [Tooltip("결과 메세지 작성")]
        string overrideMessage;
        [SerializeField]
        [Tooltip("아이템 컨텍스트 교체필요시 작성")]
        string overrideDisplayName;
        protected override void Init()
        {
            interactType = InteractType.Event;
            //아이템 타입 설정(읽는 아이템은 inspect 고정)
            context.promptKey = PromptKey.Read;
            //아이템 이름 설정
            context.displayName = MessageChoice(
                        overrideDisplayName,
                        "책");

            //책 읽는 연산 추가
            result.actions.Add(new ReadOSAction(data));
        }
      
        public override InteractResult Interact(PlayerManager target)
        {
            result.success = true;
            result.message = MessageChoice(
                        overrideMessage,
                        "책을 읽었다...");
            //여기에 특정 이벤트 추가 가능
            return result;
        }

        private string MessageChoice(string overrideText, string defaultText)
        {
            return string.IsNullOrEmpty(overrideText) ? defaultText : overrideText;
        }
    }

}
