using _Kamikakushi.Utills;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Interfaces;
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
            target.Hit(transform.position, 5, 2, HitType.Mental);
        }
    }
}
