using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public GameObject settingPanel;
    public CrosshairController ui;

    public Image brightnessOverlay;

    public Toggle windowedToggle;
    public Toggle fullscreenToggle;

    int windowWidth;
    int windowHeight;

    [SerializeField]UIManager uIManager;
    [SerializeField] GameObject Inventory;

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

    public void OnClickSave()
    {
        Debug.Log("게임이 저장되었습니다.");
    }

    public void SetBrightness(float value)
    {
        float maxDarkness = 0.6f;
        float curvedValue = value * value;
        float alpha = Mathf.Lerp(maxDarkness, 0f, curvedValue);

        brightnessOverlay.color = new Color(0f, 0f, 0f, alpha);
    }
    //전체화면 -> 윈도우 화면
    public void SetWindowMode(bool isOn)
    {
        if (!isOn) return;
        Debug.Log("창모드 전환");
        Screen.SetResolution(windowWidth, windowHeight, FullScreenMode.Windowed);
    }

    public void SetFullscreenMode(bool isOn)
    {
        if (!isOn) return;
        Debug.Log("전체화면 전환");
        // 전체화면 들어가기 전에 창모드 해상도 저장
        windowWidth = Screen.width;
        windowHeight = Screen.height;

        Screen.SetResolution(
            Screen.currentResolution.width,
            Screen.currentResolution.height,
            FullScreenMode.FullScreenWindow
        );
    }
    //세팅창 닫기
    public void CloseSetting() 
    {
        Debug.Log("종료버튼 눌림");
        uIManager.CloseCurrent();
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        CloseSetting();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
