using UnityEngine;
using _Kamikakushi.Utills.Interfaces;
using _Kamikakushi.Contents.Monster;

public class PlayerHide : MonoBehaviour
{
    private Detectorble detectorble;

    private void Awake()
    {
        detectorble = GetComponent<Detectorble>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) //Hide로 진입할 조건
        {
            detectorble.Hide();
            StopAllMonsters();
        }

        if (Input.GetKeyDown(KeyCode.J)) //unHide로 진입할 조건
        {
            {
                detectorble.UnHide();
            }
        }

        void StopAllMonsters()
        {
            Monster[] monsters = FindObjectsOfType<Monster>();
            foreach (var m in monsters)
            {
                m.ForceStopChase();
            }
        }
    }
}