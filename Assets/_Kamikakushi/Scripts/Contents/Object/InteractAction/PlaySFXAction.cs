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
    public class PlaySFXAction : MonoBehaviour, IInteractAction
    {
        [SerializeField] PlayerAudio playerAudio;
        [SerializeField] SFXType type;

        public void Execute(PlayerManager player, IInteractable obj)
        {
            playerAudio ??= player.gameObject.GetComponent<PlayerAudio>();
            if(playerAudio == null)
            {
                Debug.LogWarning("플레이어 오디오 불러오기 오류");
                return;
            }
            playerAudio.PlaySFX(type);
        }
    }
}
