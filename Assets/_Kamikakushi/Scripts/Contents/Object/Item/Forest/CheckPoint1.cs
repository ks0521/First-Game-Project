using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.UI;
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerEvents>(out playerEvents))
        {
            Debug.Log("등장");
            GameManagers.instance.SetStep(ProgressStep.Forest_Middle);
            obj.SetActive(true);
            UIManager.Instance.crosshairController.ShowText("이곳에는 더 찾을 수 있는 정보가 없는것 같다...\n마을에서 더 증거를 찾아보자");
            gameObject.SetActive(false);
        }
    }
}
