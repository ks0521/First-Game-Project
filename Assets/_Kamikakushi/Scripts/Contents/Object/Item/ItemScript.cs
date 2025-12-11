using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Item
{
    abstract public class ItemScript : MonoBehaviour
    {
        public int KeyCode { get; protected set; }
        protected string itemName;
        protected string itemDescription;
        private void Start()
        {
            Init();
        }
        abstract protected void Init(); 
    }

}

