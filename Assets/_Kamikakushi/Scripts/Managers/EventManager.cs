using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Contents.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] PlayerEvents playerEvents;
    private void Update()
    {
        if(GameManagers.instance.NowStep() == ProgressStep.Tutorial_Hide)
        {
            if (!target.activeSelf)
            {
                Debug.Log("귀신 사라짐");
                GameManagers.instance.SetStep(ProgressStep.Tutorial_Break);
                gameObject.SetActive(false);
            }
        }
    }
}
