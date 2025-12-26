using _Kamikakushi.Contents.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills
{
    public static class ObjectiveTextTable
    {
        public static readonly Dictionary<ProgressStep, string> Text = new()
    {
        { ProgressStep.Tutorial_ReadLetter, "편지를 확인하기" },
        { ProgressStep.Tutorial_AcquireKey, "거실 열쇠를 찾아보기" },
        { ProgressStep.Tutorial_InvestigateLivingRoom, "거실을 조사하기" },
        { ProgressStep.Tutorial_Hide, "숨을 수 있는 장소 찾기"},
        { ProgressStep.Tutorial_Break, "휴식 후 마을을 탐색하기"},
        { ProgressStep.Village_FindClue, "마을에서 단서를 수집하기" },
        { ProgressStep.Village_GoForest, "숲으로 가기" },
        { ProgressStep.Forest_FindClue, "숲속에서 단서를 수집하기" },
        { ProgressStep.Forest_Middle, "마을로 돌아가" },
        { ProgressStep.Forest_Run, "괴이에게서 도망치기" },
        { ProgressStep.Finale_House, "저택의 비밀 찾기" },
    };
    }
}