using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Enums;

namespace _Kamikakushi.Contents.Item
{
    abstract public class PickUpItems : MonoBehaviour
    {
        public int KeyCode { get; protected set; }
        [SerializeField]protected string itemName;
        [SerializeField]protected string itemDescription;
        private void Awake()
        {
            Init();
        }
        abstract protected void Init(); 
    }

}

