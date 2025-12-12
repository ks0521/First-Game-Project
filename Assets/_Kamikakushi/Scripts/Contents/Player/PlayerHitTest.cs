using _Kamikakushi.Utills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitTest : MonoBehaviour
{
    IHittable target;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 충돌");
        if(other.TryGetComponent<IHittable>(out target))
        {
            Debug.Log("플레이어 충돌");
            target.Hit(transform.position);
        }
    }
}
