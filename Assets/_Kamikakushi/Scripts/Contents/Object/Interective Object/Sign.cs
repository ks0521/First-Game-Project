using _Kamikakushi.Contents.InteractAction;
using _Kamikakushi.Contents.Item;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.InteractiveObject
{

    public class Sign : InteractItems
    {
        [SerializeField]
        [Tooltip("이동하고 싶은 맵 선택")]
        Map maps;
        [SerializeField]
        [TextArea(2, 4)]
        [Tooltip("아이템 컨텍스트 교체필요시 작성")]
        string overrideDisplayName;
        [SerializeField]
        [TextArea(2, 4)]
        [Tooltip("결과 문구 변화시 사용")]
        string overrideResultText;
        protected override void Init()
        {
            //인터페이스의 배열이기때문에 GetComponents 사용
            interactType = InteractType.Event;
            context.promptKey = PromptKey.AcivateSwitch;
            context.displayName = MessageChoice(
                        overrideDisplayName,
                        "표지판");
        }
        public override InteractResult Interact(PlayerManager target)
        {
            if (CanInteract(target))
            {
                //이동하려는 씬 입력
                result.actions.Add(new SceneChangeAction((int)maps));
                result.success = true;
                result.message = MessageChoice(
                        overrideResultText,
                        "숲으로 이동한다...");
            }
            else
            {
                result.success = false;
                result.message = "지금은 갈 수 없다...";
            }

            return result;
        }

        private string MessageChoice(string overrideText, string defaultText)
        {
            return string.IsNullOrEmpty(overrideText) ? defaultText : overrideText;
        }
    }
}