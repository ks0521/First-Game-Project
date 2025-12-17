using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerHide : MonoBehaviour
    {
        [SerializeField] PlayerManager manager;
        [SerializeField] Transform curPos;
        [SerializeField] Transform prevPos;
        [SerializeField] Camera cam;
        void Awake()
        {
            manager = GetComponent<PlayerManager>();
        }
        public void Hide(Transform point)
        {
            manager.isHide = true;
            prevPos = transform;
        }
    }
}
