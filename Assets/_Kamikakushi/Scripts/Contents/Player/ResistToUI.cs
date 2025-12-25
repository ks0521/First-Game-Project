using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistToUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ui = FindObjectOfType<UIManager>(true);
        if (ui == null) return;

        var player = GetComponent<PlayerManager>();
        if (player == null) return;

        ui.PlayerResist(player);
    }

}
