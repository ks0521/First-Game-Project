using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using _Kamikakushi.Contents.Manager;
using _Kamikakushi.Utills.Enums;

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
       "지금까지의 수집한 증거들은, 이 괴이들이 인간이 묻어두었던 죄책감의 잔재임을 드러내고 있었다",
       "'신에게 돌려보낸다' 라는 말로 스스로를 속였던 선택은 끝내 기록과 피로 남아 이 땅에 스며들었다",
       "이 증거들을 경찰에게 신고했다. 인간의 죄는 신의 이름 뒤에 숨을 수 없다. 인간의 죄는 인간이 심판해야 할 것이다",
       "떠나기 전, 추악한 악습으로 희생된 이들을 위해 마지막 기도를 올린다.",
    };

    void Start()
    {
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