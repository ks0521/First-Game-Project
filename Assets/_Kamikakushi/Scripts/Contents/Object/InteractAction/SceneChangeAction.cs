using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Manager;
using Project.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace _Kamikakushi.Contents.InteractAction
{
    public class SceneChangeAction : IInteractAction
    {
        private readonly int sceneNum;
        public SceneChangeAction(int num)
        {
            sceneNum = num;
        }
        public void Execute(PlayerManager player, IInteractable source)
        {
            GameManagers.instance.LoadScene(sceneNum);
        }
    }
}
