using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Kamikakushi.Contents.Data
{
    [Serializable]
    public class SaveData
    {
        public Map map;
        public ProgressStep step;
        public int clueCount;
    }

}
