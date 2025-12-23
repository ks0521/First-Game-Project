using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Inventory
{
    [CreateAssetMenu(fileName = "NewReading", menuName = "Project/ReadableData")]
    public class ReadableData : ScriptableObject
    {
        public string title;
        [TextArea(2, 4)]
        public string body;
        public Sprite images;
    }
}