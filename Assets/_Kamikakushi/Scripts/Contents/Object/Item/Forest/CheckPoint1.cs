using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    PlayerEvents playerEvents;
    [SerializeField] GameObject obj;
    private void OnDrawGizmos()
    {
        var box = GetComponent<BoxCollider>();
        if (box == null) return;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(box.center, box.size);

    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerEvents>(out playerEvents))
        {
            Debug.Log("등장");
            playerEvents.OnChangeObjective("마을로 돌아가기");
            obj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
