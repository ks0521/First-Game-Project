using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace _Kamikakushi.Audio
{
    public class AudioSettingController : MonoBehaviour
    {

        [SerializeField] AudioMixer mixer;
        [SerializeField] Slider masterSlider;
        [SerializeField] Slider bgmSlider;
        [SerializeField] Slider sfxSlider;
        private const string Master = "MasterVol";
        private const string BGM = "BGMVol";
        private const string SFX = "SFXVol";

        private const string KEY_MASTER = "VOL_MASTER_01";
        private const string KEY_BGM = "VOL_BGM_01";
        private const string KEY_SFX = "VOL_SFX_01";

        private bool syncing; // UI 동기화 중 이벤트 재호출 방지

        private void Awake()
        {
            LoadApply();

            SyncSlidersFromSaved();

            masterSlider.onValueChanged.AddListener(value => OnSliderChanged(Master, value,KEY_MASTER));
            bgmSlider.onValueChanged.AddListener(value => OnSliderChanged(BGM, value,KEY_BGM));
            sfxSlider.onValueChanged.AddListener(value => OnSliderChanged(SFX, value,KEY_SFX));

        }
        private void OnEnable()
        {
            SyncSlidersFromSaved();
        }
        //음량조절
        void OnSliderChanged(string param, float value, string saveKey)
        {
            if (syncing) return;
            SetVolume(param, value);
            SaveVolume(saveKey,value);
        }
        void SetVolume(string param, float value)
        {

            //value가 0이면 log스케일할때 -inf로 가버림
            value = Mathf.Clamp(value, 0.0001f, 1f);

            //log_10(0.0001) = 4 , log_10(1) = 0 => -80db ~ 0db
            value = Mathf.Log10(value) * 20f;
            mixer.SetFloat(param, value);
        }
        void SaveVolume(string key, float value)
        {
            PlayerPrefs.SetFloat(key, Mathf.Clamp01(value));
            PlayerPrefs.Save();
        }
        float LoadVolume(string key, float defaultVolume = 1)
        {
            return Mathf.Clamp01(PlayerPrefs.GetFloat(key, defaultVolume));
        }
        public void SyncSlidersFromSaved()
        {
            syncing = true;

            float m = LoadVolume(KEY_MASTER, 1f);
            float b = LoadVolume(KEY_BGM, 1f);
            float s = LoadVolume(KEY_SFX, 1f);

            masterSlider.SetValueWithoutNotify(m);
            bgmSlider.SetValueWithoutNotify(b);
            sfxSlider.SetValueWithoutNotify(s);

            syncing = false;
        }
        public void LoadApply()
        {
            float m = LoadVolume(KEY_MASTER, 1f);
            float b = LoadVolume(KEY_BGM, 1f);
            float s = LoadVolume(KEY_SFX, 1f);

            SetVolume(Master, m);
            SetVolume(BGM, b);
            SetVolume(SFX, s);
        }
    }

}
