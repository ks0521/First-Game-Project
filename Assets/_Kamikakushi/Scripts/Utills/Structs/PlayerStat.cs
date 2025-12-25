using _Kamikakushi.Contents.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Utills.Structs
{
    public struct playerStat
    {
        private float hp;
        private float sanity;
        public float Hp
        {
            get {  return hp; } 
            set
            {
                hp = value;
                if(hp>MaxHp) hp = MaxHp;
                if (hp <= 0) GameOverControll.Instance.GameOver();
            }
        }
        public float Sanity
        {
            get { return sanity; }
            set
            {
                sanity = value;
                if(sanity > MaxSanity) sanity = MaxSanity;
                if (sanity <= 0) GameOverControll.Instance.GameOver();
            }
        }
        public readonly float MaxSanity => 100;
        public readonly float MaxHp => 100;
    }
}

