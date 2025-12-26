using _Kamikakushi.Contents.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcode : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameManagers.instance.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManagers.instance.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GameManagers.instance.LoadScene(3);
        }
    }
}
