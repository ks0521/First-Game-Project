using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerHide : MonoBehaviour
    {
        PlayerManager manager;
        PlayerEvents events;
        PlayerController controller;
        [SerializeField] Transform cam;

        private Transform prevParent;
        private Vector3 prevPosition;
        private Quaternion prevRotation;

        void Awake()
        {
            manager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            controller = GetComponent<PlayerController>();
            
        }
        private void OnEnable()
        {
            events.PlayerHideInEvent += HideEnter;
            events.PlayerHideOutEvent += HideExit;
        }
        private void OnDisable()
        {
            events.PlayerHideInEvent -= HideEnter;
            events.PlayerHideOutEvent -= HideExit;
        }
        public void HideEnter(Transform point)
        {
            if (manager.isHide) return;
            StartCoroutine(CoEnter(point));
        }

        IEnumerator CoEnter(Transform viewPoint)
        {
            //isTransition = true;

            // 입력 먼저 잠그면 카메라 흔들림/추가 회전이 안 섞여서 안정적
            controller.enabled = false;

            // 카메라 원래 상태 저장
            prevParent = cam.parent;
            prevPosition = cam.localPosition;
            prevRotation = cam.localRotation;

            cam.SetParent(viewPoint, true);

            Vector3 startPos = cam.localPosition;
            Quaternion startRot = cam.localRotation;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / Mathf.Max(0.01f, 0.35f);

                // 부드러운 가속/감속 구현용
                float s = Mathf.SmoothStep(0f, 1f, t);

                cam.localPosition = Vector3.Lerp(startPos, Vector3.zero, s);

                cam.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, s);
                yield return null;
            }
            //cam.SetPositionAndRotation(viewPoint.position, viewPoint.rotation);
            cam.localPosition = Vector3.zero;
            cam.localRotation = Quaternion.identity;
            manager.isHide = true;
            Debug.Log(manager.CanDetected);
            controller.enabled = true;

            //isTransition = false;
        }

        public void HideExit()
        {
            if (!manager.isHide) return;
            Debug.Log("나오기");
            StartCoroutine(CoExit());
        }

        IEnumerator CoExit()
        {
            // 입력 먼저 잠그면 카메라 흔들림/추가 회전이 안 섞여서 안정적
            controller.enabled = false;


            cam.SetParent(prevParent, true);

            Vector3 startPos = cam.localPosition;
            Quaternion startRot = cam.localRotation;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / Mathf.Max(0.01f, 0.35f);

                // 부드러운 가속/감속 구현용
                float s = Mathf.SmoothStep(0f, 1f, t);

                cam.localPosition = Vector3.Lerp(startPos, prevPosition, s);

                cam.localRotation = Quaternion.Slerp(startRot, prevRotation, s);
                yield return null;
            }
            //cam.SetPositionAndRotation(prevPosition, prevRotation);
            cam.localPosition = prevPosition;
            cam.localRotation = prevRotation;
            Debug.Log("나옴");
            manager.isHide = false;
            Debug.Log(manager.CanDetected);

            controller.enabled = true;
        }
    }
}