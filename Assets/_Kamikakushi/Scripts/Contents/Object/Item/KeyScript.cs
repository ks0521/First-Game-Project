using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    public class KeyScript : ItemScript
    {
        protected override void Init()
        {
            itemName = "거실 열쇠";
            itemDescription = "거실의 문을 열 수 있을 것 같다. ";
            KeyCode = 3;
        }
    }
}
