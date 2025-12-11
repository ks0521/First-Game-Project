using _Kamikakushi.Contents.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class WaterScript : ItemScript
{
        protected override void Init()
        {
            itemName = "물";
            itemDescription = "차갑다. ";
            KeyCode = 2;
        }
}
