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

    UIManager uIManager;
    [SerializeField] GameObject Inventory;

    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    private const string Master = "MasterVol";
    private const string BGM = "BGMVol";
    private const string SFX = "SFXVol";
    private void Awake()
    {
        //각 슬라이더 값 변화할때마다 변화한 value를 받아서 SetVolume 함수에 전달
        masterSlider.onValueChanged.AddListener(value => SetVolume(Master, value));
        bgmSlider.onValueChanged.AddListener(value => SetVolume(BGM, value));
        sfxSlider.onValueChanged.AddListener(value => SetVolume(SFX, value));
    }

    private void Start()
    {
        if (settingPanel != null)
            settingPanel.SetActive(false);
        uIManager = GetComponentInParent<UIManager>();
        SetBrightness(1f);

        windowWidth = Screen.width;
        windowHeight = Screen.height;

        if (Screen.fullScreen)
            fullscreenToggle.isOn = true;
        else
            windowedToggle.isOn = true;
    }
    //음량조절
    void SetVolume(string param, float value)
    {
        //value가 0이면 log스케일할때 -inf로 가버림
        value = Mathf.Clamp(value, 0.0001f, 1f);

        //log_10(0.0001) = 4 , log_10(1) = 0 => -80db ~ 0db
        value = Mathf.Log10(value) * 20f;
        mixer.SetFloat(param, value);
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
    //전체화면 -> 윈도우 화면
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
    //세팅창 닫기
    public void CloseSetting() { uIManager.CloseCurrent(); }
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
