using _Kamikakushi.Contents.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //[SerializeField] Inventory inventory; - 인벤토리 클래스
    [SerializeField] int sanity;
    [SerializeField] int playerCount;
    [SerializeField] public ItemScript handeditems;
    void Awake()
    {
        sanity = 100;
        Debug.Log(sanity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
