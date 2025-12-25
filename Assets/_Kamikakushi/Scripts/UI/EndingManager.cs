using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;
using UnityEngine.SocialPlatforms;

public class EndingManager : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI endingText;
    [SerializeField] private Image fadeImage;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.08f;
    [SerializeField] private float delayBetweenLines = 2.5f;
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private Map mainMenuMap;

    // --- 스크립트에서 직접 텍스트 정의 ---
    private List<string> endingMessages = new List<string>()
    {
       "괴이가 왜 나타났는지 끝내 깨닫지 못한 채",
       "나는 마을을 떠났다",
    };

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        // 초기화: 텍스트는 비우고 화면은 검게 시작
        endingText.text = "";
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1);

        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        // 1. 페이드 인 (검은 화면 -> 밝아짐)
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timer / fadeDuration));
            yield return null;
        }

        // 2. 스크립트에 저장된 텍스트들을 순차적으로 출력
        foreach (string message in endingMessages)
        {
            endingText.text = ""; // 이전 글자 지우기

            // 한 글자씩 타이핑
            foreach (char letter in message.ToCharArray())
            {
                endingText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            // 플레이어가 읽을 시간 대기
            yield return new WaitForSeconds(delayBetweenLines);
        }

        // 3. 페이드 아웃 (다시 검게 변함)
        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timer / fadeDuration));
            yield return null;
        }

        // 4. 메인 화면으로 이동
        if (GameManagers.instance != null)
        {
            GameManagers.instance.LoadScene((int)mainMenuMap);
        }
    }
}