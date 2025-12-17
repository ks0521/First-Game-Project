using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerHide : MonoBehaviour, IDetectorble
    {
        public bool CanDetected { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            CanDetected = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
