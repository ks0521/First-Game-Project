using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Kamikakushi.Utills.Enums;
using _Kamikakushi.Utills.Structs;

[System.Serializable]
public class PromptUIData
{
    public PromptKey key;
    public Sprite crosshair; // 크로스헤어 아이콘
}

public class InteractionUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public Image crosshairImage;
    public TextMeshProUGUI promptText;

    [Header("Default")]
    public Sprite defaultCrosshair;

    [Header("UI Table")]
    public List<PromptUIData> uiTable;

    private Dictionary<PromptKey, PromptUIData> table;
    private bool isBlocked;

    private void Awake()
    {
        table = new Dictionary<PromptKey, PromptUIData>();
        foreach (var data in uiTable)
            table[data.key] = data;

        ShowNormal();
    }

    public void SetBlocked(bool value)
    {
        isBlocked = value;
        if (isBlocked)
            Hide();
    }

    public void ShowNormal()
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
            crosshairImage.sprite = data.crosshair;

        promptText.text = result.message;
    }

    // 일반 상호작용 프롬프트
    public void ShowPrompt(InteractContext context)
    {
        if (isBlocked) return;

        crosshairImage.enabled = true;

        if (!table.TryGetValue(context.promptKey, out var data))
            crosshairImage.sprite = defaultCrosshair;
        else
            crosshairImage.sprite = data.crosshair;

        // displayName 기반으로 기본 프롬프트 표시
        promptText.text = $"E : {context.displayName}";
    }

    public void Hide()
    {
        crosshairImage.enabled = false;
        promptText.text = "";
    }
}