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
        [SerializeField] PlayerManager manager;
        [SerializeField] PlayerEvents events;
        [SerializeField] PlayerController controller;
        [SerializeField] Transform cam;

        private Transform prevParent;
        private Vector3 prevPosition;
        private Quaternion prevRotation;

        void Awake()
        {
            manager = GetComponent<PlayerManager>();
            events = GetComponent<PlayerEvents>();
            controller = GetComponent<PlayerController>();
            events.PlayerHideInEvent += HideEnter;
            events.PlayerHideOutEvent += HideExit;
        }
        public void HideEnter(Transform point)
        {
            if (manager.isHide) return;

            /*manager.isHide = true;
            Debug.Log("숨어들어가기");
            prevParent = cam.parent;
            prevPosition = cam.position;
            prevRotation = cam.rotation;
            cam.SetParent(null, true);
            cam.SetLocalPositionAndRotation(point.position, point.rotation);
            controller.enabled = false;*/
            StartCoroutine(CoEnter(point));
        }

        IEnumerator CoEnter(Transform viewPoint)
        {
            //isTransition = true;

            // 입력 먼저 잠그면 카메라 흔들림/추가 회전이 안 섞여서 안정적
            controller.enabled = false;

            // 카메라 원래 상태 저장
            prevParent = cam.parent;
            prevPosition = cam.position;
            prevRotation = cam.rotation;
            cam.SetParent(null, true);

            Vector3 startPos = cam.position;
            Quaternion startRot = cam.rotation;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / Mathf.Max(0.01f, 0.35f);

                // 부드러운 가속/감속 구현용
                float s = Mathf.SmoothStep(0f, 1f, t);

                cam.position = Vector3.Lerp(startPos, viewPoint.position, s);

                cam.rotation = Quaternion.Slerp(startRot, viewPoint.rotation, s);
                yield return null;
            }
            cam.SetPositionAndRotation(viewPoint.position, viewPoint.rotation);

            manager.isHide = true;
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

            Vector3 startPos = cam.position;
            Quaternion startRot = cam.rotation;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / Mathf.Max(0.01f, 0.35f);

                // 부드러운 가속/감속 구현용
                float s = Mathf.SmoothStep(0f, 1f, t);

                cam.position = Vector3.Lerp(startPos, prevPosition, s);

                cam.rotation = Quaternion.Slerp(startRot, prevRotation, s);
                yield return null;
            }
            cam.SetPositionAndRotation(prevPosition, prevRotation);
            Debug.Log("나옴");
            manager.isHide = false;
            controller.enabled = true;
        }
    }
}
