using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Kamikakushi.Contents.UI
{
    public class GameOverControll : MonoBehaviour
    {
        public static GameOverControll Instance;
        [SerializeField] private CanvasGroup fadeGroup; // CanvasGroup 붙은 오브젝트
        [SerializeField] private float fadeDuration = 3.0f;
        [SerializeField] private string mainSceneName = "MainMenu";

        private bool isGameOver;
        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if(fadeGroup != null)
            {
                fadeGroup.alpha = 0f;
                fadeGroup.blocksRaycasts = false;
                fadeGroup.interactable = false;
            }
        }
        public void GameOver()
        {
            Debug.Log("게임종료");
            if (isGameOver) return;
            isGameOver = true;

            StartCoroutine(CoGameOver());
        }

        private IEnumerator CoGameOver()
        {
            // 입력 잠글거면 PlayerController 비활성화
            // Time.timeScale을 0으로 하고 싶으면 WaitForSecondsRealtime 써야 함
            // 클릭 막기
            if (fadeGroup != null) fadeGroup.blocksRaycasts = true;

            yield return FadeTo(1f, fadeDuration);

            GameManagers.instance.LoadScene((int)Map.Main);
            //깜빡임 방지
            Cursor.lockState = CursorLockMode.None;
            yield return null;
            yield return FadeTo(0, 0.5f);
            if (fadeGroup != null) fadeGroup.blocksRaycasts = false;
            isGameOver = false;
        }

        private IEnumerator FadeTo(float targetAlpha, float duration)
        {
            if(fadeGroup == null) yield break;

            float startAlpha = fadeGroup.alpha;
            float t = 0f;

            //천천히 targetAlpha까지 어두워짐
            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / Mathf.Max(0.0001f, duration);
                fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            fadeGroup.alpha = targetAlpha;
        }
    }

}
