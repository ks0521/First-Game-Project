using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject settingPanel;

    private bool isOpen = false;

    private void Start()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSetting();
        }
    }

    public void ToggleSetting()
    {
        isOpen = !isOpen;
        settingPanel.SetActive(isOpen);

        // 게임 일시정지 / 재개
        Time.timeScale = isOpen ? 0f : 1f;
    }

    public void CloseSetting()
    {
        isOpen = false;
        settingPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickSave()
    {
        Debug.Log("게임이 저장되었습니다.");
    }
}
