using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform cameraRotation;
        [SerializeField] CharacterController characterController;
        [SerializeField] private float mouseSpeed = 5f;
        [SerializeField] private float gravity = -9.8f;

        private float mouseX = 0;
        private float mouseY = 0;
        float yaw;
        float pitch;

        float moveSpeed = 5f;
        float h;
        float v;
        //중력 관리용
        Vector3 velocity;
        Vector3 move;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            yaw = transform.rotation.eulerAngles.y;
            // 카메라의 현재 X각도(상하)
            pitch = transform.localEulerAngles.x;
            // 0~360 → -180~180 으로 보정
            if (pitch > 180f) pitch -= 360f;
        }
        void Update()
        {
            Rotation();
            Moving();
        }

        private void Moving()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // 카메라/플레이어 바라보는 방향 기준으로 입력 벡터 생성
            move = ((transform.right * h + transform.forward * v).normalized) * moveSpeed;

            // 땅에 붙어 있을 때는 y속도 리셋
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // 중력 적용
            velocity.y += gravity * Time.deltaTime;

            // 최종 이동 벡터 = 평면 이동 + y축 중력
            move += velocity;

            characterController.Move(move * Time.deltaTime);

        }
        private void Rotation()
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
            mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;

            yaw += mouseX;       // 좌우(플레이어)
            pitch -= mouseY;       // 위아래(카메라)

            // 위아래 제한
            pitch = Mathf.Clamp(pitch, -70f, 30f);

            // 실제 회전 적용
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);     // 좌우이동은 Player 몸통을 옮겨 종속된 카메라가 따라감
            cameraRotation.localRotation = Quaternion.Euler(pitch, 0f, 0f);   // 상하이동은 카메라만 옮긴다
        }
    }
}

