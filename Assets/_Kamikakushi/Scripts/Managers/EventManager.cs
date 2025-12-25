using _Kamikakushi.Contents.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] PlayerEvents playerEvents;
    bool isEnable;
    private void Update()
    {
        if (target.activeSelf) isEnable = true;
        if(isEnable)
        {
            if (!target.activeSelf)
            {
                playerEvents.OnChangeObjective("휴식 취하기");
            }
        }
    }
}
