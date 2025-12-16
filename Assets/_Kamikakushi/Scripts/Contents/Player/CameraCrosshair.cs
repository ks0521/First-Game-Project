using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
using UnityEngine;

struct RaycastInfo
{
    
}
namespace _Kamikakushi.Contents.Player
{
    /// <summary>
    /// 플레이어가 레이캐스트하는 센서역할
    /// </summary>
    public class CameraCrosshair : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private RectTransform crosshair;
        [SerializeField] private PlayerEvents events;
        [SerializeField] private float maxDistance = 5f;
        private IInteractable interactObj;
        private bool wasHit;
        private bool isHit;
        private Ray ray;
        private RaycastHit hit;
        private LayerMask mask = (int)AdaptedLayerMask.InteractionObject;
        private Vector3 defaultScreen = new Vector3(
            Screen.width / 2f, Screen.height / 2f, 0
            );
        // Start is called before the first frame update
        void Start()
        {
            events = GetComponentInParent<PlayerEvents>();
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            wasHit = isHit;
            ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
            if (Physics.Raycast(ray, out hit, maxDistance, mask))
            {
                //카메라 위치기준 Raycast해서 충돌지점에 크로스헤어 보정
                crosshair.position = cam.WorldToScreenPoint(hit.point);
                isHit = true;
            }
            else
            {
                //Raycast결과가 없을 때 (상호작용 오브젝트 없음)  
                crosshair.position = defaultScreen;
                isHit = false;
            }
            if (isHit != wasHit)
            {
                //상호작용 오브젝트를 처음 raycast했을때
                if (isHit == true)
                {
                    interactObj = hit.collider.gameObject.GetComponent<IInteractable>();
                    events.OnRaycastEnter(hit);
                }
                //상호작용 오브젝트에 raycast하지 못했을 때
                else events.OnRaycastOut();
            }
        }
    }

}
