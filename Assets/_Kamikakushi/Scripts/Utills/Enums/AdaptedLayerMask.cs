using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Enums
{
    public enum AdaptedLayerMask
    {
        Player = 1 << 6,
        Monster = 1 << 8,
        MonsterDetector = 1 << 9,
        Item = 1 << 11,
        InteractionObject = 1 << 12
    }
}