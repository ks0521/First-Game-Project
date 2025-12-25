using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
public class PromptUIData
{
    public PromptKey key;
    public Sprite crosshair; // 크로스헤어 아이콘
    public string context;
}

public class CrosshairController : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.Image crosshairImage;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI resultText;

    [Header("Default")]
    public Sprite defaultCrosshair;

    [Header("UI Table")]
    public List<PromptUIData> uiTable;


    private Dictionary<PromptKey, PromptUIData> table;
    private string crosshairContext;
    private bool isBlocked;

    private void Awake()
    {
        table = new Dictionary<PromptKey, PromptUIData>();
        foreach (var data in uiTable)
            table[data.key] = data;
        SetDefault();
    }
    private void OnEnable()
    {
        SetDefault();
    }
    public void SetDefault()
    {
        if (isBlocked) return;

        crosshairImage.enabled = true;
        crosshairImage.sprite = defaultCrosshair;
        promptText.text = "";
    }

    // 실제 상호작용 메시지를 UI에 표시
    public void ShowInteractResult(InteractResult result, InteractContext context)
    {
        if (isBlocked) return;

        crosshairImage.enabled = true;

        // UI Table에서 crosshair 가져오기
        if (!table.TryGetValue(context.promptKey, out var data))
            crosshairImage.sprite = defaultCrosshair;
        else
        {
            crosshairImage.sprite = data.crosshair;
        }

        StartCoroutine(PrintResult(result.message));
    }
    IEnumerator PrintResult(string text)
    {

        Debug.Log(text);
        resultText.text = text;
        yield return new WaitForSeconds(2);

        resultText.text = "";
    }
    // 일반 상호작용 프롬프트
    public void ShowPrompt(InteractContext context)
    {
        if (isBlocked) return;

        crosshairImage.enabled = true;


        if (!table.TryGetValue(context.promptKey, out var data))
            crosshairImage.sprite = defaultCrosshair;
        else
        {
            crosshairImage.sprite = data.crosshair;
            crosshairContext = data.context;
        }


        // displayName 기반으로 기본 프롬프트 표시
        promptText.text = $"E : {context.displayName} {crosshairContext}";
    }
}
