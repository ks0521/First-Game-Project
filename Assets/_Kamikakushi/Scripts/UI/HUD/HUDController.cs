using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project.Inventory;
using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using TMPro;
using _Kamikakushi.Contents.UI;

public class HUDController : MonoBehaviour
{
    [Header("Equipped Item")]
    public Image equippedItemIcon;

    [Header("Status Bars")]
    public Image hpBar;
    public Image sanityBar;

    [Range(1f, 20f)]
    public float smoothSpeed = 8f;

    string curtext;
    [SerializeField] PlayerEvents events;
    float targetHPFill = 1f;
    float targetSanityFill = 1f;

    [SerializeField] TextMeshProUGUI objective;
    [SerializeField] UIManager uiManager;

    private void OnEnable()
    {
        if (uiManager == null)
            uiManager = GetComponentInParent<UIManager>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>(true);

        uiManager.OnResist += Bind;

        // 이미 플레이어가 등록된 상태일 수 있으니 바로 한 번 바인드 시도(중요)
        Bind();
    }
    private void OnDisable()
    {
        if (uiManager != null)
        uiManager.OnResist -= Bind;
    }
    void Bind()
    {
        if (uiManager == null || uiManager.playerEvents == null)
        {
            Debug.LogWarning("HUD컨트롤러에서 플레이어 이벤트 등록 실패!");
            return;
        }
        //씬이동이나 기다 재시작으로 인해 bing재호출시 기존 이벤트는 구독해제
        if(events != null) events.PlayerStatChange -= UpdateState;
        events = uiManager.playerEvents;
        //새롭게 이벤트 구독
        if (events != null) events.PlayerStatChange += UpdateState;
    }
    private void Update()
    {
        hpBar.fillAmount = Mathf.Lerp(
            hpBar.fillAmount,
            targetHPFill,
            Time.deltaTime * smoothSpeed
        );

        sanityBar.fillAmount = Mathf.Lerp(
            sanityBar.fillAmount,
            targetSanityFill,
            Time.deltaTime * smoothSpeed
        );
    }

    public void SetEquippedItem(ItemData item)
    {
        if (item == null)
        {
            equippedItemIcon.sprite = null;
            equippedItemIcon.color = new Color(1, 1, 1, 0); // 투명
            return;
        }

        equippedItemIcon.sprite = item.icon;
        equippedItemIcon.color = Color.white;
    }
    void UpdateState(playerStat stat)
    {
        Debug.Log($"{stat.Hp},{stat.Sanity}");
        targetHPFill = stat.Hp / stat.MaxHp;
        targetSanityFill = stat.Sanity / stat.MaxSanity;
    }

    public void ChangeObjective(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        //중복방지
        if (curtext == text) return;
        curtext = text;
        objective.text = text;
    }
}
