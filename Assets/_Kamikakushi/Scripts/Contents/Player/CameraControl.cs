using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class CameraControl : MonoBehaviour
    {
        PlayerEvents events;
        PlayerController controller;
        Vector3 normalizedDirect;

        // Start is called before the first frame update
        private void Awake()
        {
            events = GetComponentInParent<PlayerEvents>();
            controller = GetComponentInParent<PlayerController>();
        }

        private void OnEnable()
        {
            if(events == null) events = GetComponentInParent<PlayerEvents>();
            if(events != null) events.PlayerHitEvent += CameraHold;
        }
        private void OnDisable()
        {
            if(events != null) events.PlayerHitEvent -= CameraHold;
        }
        void CameraHold(Vector3 target, float damage, float time, HitType type)
        {
            normalizedDirect = (target - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(normalizedDirect);
            events.OnCameraHold(time);
        }
        
    }
}
