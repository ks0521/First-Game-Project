using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Structs
{
    public struct playerStat
    {
        public float hp;
        public float sanity;
        public readonly float MaxSanity => 100;
        public readonly float MaxHp => 100;
    }
}

