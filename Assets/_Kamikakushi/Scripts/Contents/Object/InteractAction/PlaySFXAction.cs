using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Audio;
using _Kamikakushi.Utills.Interfaces;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class PlaySFXAction : IInteractAction
    {
        PlayerAudio playerAudio;
        private readonly SFXType type;

        public PlaySFXAction(SFXType _type)
        {
            type = _type;
        }
        public void Execute(PlayerManager player, IInteractable obj)
        {
            playerAudio = player.gameObject.GetComponent<PlayerAudio>();
            if(playerAudio == null)
            {
                Debug.LogWarning("플레이어 오디오 불러오기 오류");
            }
            playerAudio.PlaySFX(type);
            Debug.Log("플레이어sfx");
        }
    }
}
