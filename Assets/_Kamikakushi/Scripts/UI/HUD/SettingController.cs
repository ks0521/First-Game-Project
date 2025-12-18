using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public GameObject settingPanel;
    public InteractionUIController ui;

    private bool isOpen = false;

    public Image brightnessOverlay;

    public Toggle windowedToggle;
    public Toggle fullscreenToggle;

    int windowWidth;
    int windowHeight;

    private void Start()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);

        SetBrightness(1f);

        windowWidth = Screen.width;
        windowHeight = Screen.height;

        if (Screen.fullScreen)
            fullscreenToggle.isOn = true;
        else
            windowedToggle.isOn = true;
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

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ui.SetBlocked(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ui.SetBlocked(false);
            ui.ShowNormal();
        }
    }

    public void CloseSetting()
    {
        isOpen = false;
        settingPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ui.SetBlocked(false);
        ui.ShowNormal();
    }

    public void OnClickSave()
    {
        Debug.Log("게임이 저장되었습니다.");
    }

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetBrightness(float value)
    {
        float maxDarkness = 0.6f;
        float curvedValue = value * value;
        float alpha = Mathf.Lerp(maxDarkness, 0f, curvedValue);

        brightnessOverlay.color = new Color(0f, 0f, 0f, alpha);
    }

    public void SetWindowMode(bool isOn)
    {
        if (!isOn) return;

        Screen.SetResolution(windowWidth, windowHeight, FullScreenMode.Windowed);
    }

    public void SetFullscreenMode(bool isOn)
    {
        if (!isOn) return;

        // 전체화면 들어가기 전에 창모드 해상도 저장
        windowWidth = Screen.width;
        windowHeight = Screen.height;

        Screen.SetResolution(
            Screen.currentResolution.width,
            Screen.currentResolution.height,
            FullScreenMode.FullScreenWindow
        );
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
