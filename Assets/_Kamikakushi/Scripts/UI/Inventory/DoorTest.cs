using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public string doorKeyCode;

    private Player currentPlayer;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            currentPlayer = player;
            Debug.Log("문을 열려면 E를 누르세요.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            currentPlayer = null;
            Debug.Log("문에서 멀어졌다.");
        }
    }

    private void Update()
    {
        if (currentPlayer == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpen(currentPlayer);
        }
    }

    public void TryOpen(Player player)
    {
        // 열쇠가 없을 때
        if (string.IsNullOrEmpty(player.equippedKeyCode))
        {
            Debug.Log("열쇠를 가지고 있지 않습니다.");
            return;
        }

        // 열쇠는 있지만 코드가 다를 때
        if (player.equippedKeyCode != doorKeyCode)
        {
            Debug.Log("열쇠가 맞지 않는다.");
            return;
        }

        // 코드가 일치할 때
        Debug.Log("문이 열렸다!");

        player.ConsumeEquippedKey();
    }
}
