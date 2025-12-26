using _Kamikakushi.Contents.Player;
using _Kamikakushi.Contents.UI;
using _Kamikakushi.Utills.Structs;
using System.Data;
using UnityEngine;

public class InteractionUIEventReceiver : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    public PlayerEvents playerEvents;
    public CrosshairController interactionUIController;
    public HUDController HUDController;
    private InteractContext currentContext;

    private void OnEnable()
    {
        
        if (UIManager.Instance == null)
        {
            Debug.LogWarning("InteractionUIEventReceiver: UIManager 없음");
            return;
        }

        UIManager.Instance.OnResist += Bind;

        // 이미 PlayerResist가 끝난 상태일 수 있으니 즉시 한 번 시도
        Bind();

        //SubscribePlayerEvents();
    }
    private void Start()
    {
        //onenable단에서 안됐을때 start단에서 한번 더 시도
        if (UIManager.Instance == null)
        {
            Debug.LogWarning("InteractionUIEventReceiver: UIManager 없음");
            return;
        }

        UIManager.Instance.OnResist += Bind;
        Bind();
    }
    private void OnDisable()
    {
        uiManager.OnResist -= Bind;

        UnsubscribePlayerEvents();
    }
    void Bind()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogWarning("HUD컨트롤러에서 플레이어 이벤트 등록 실패!");
            return;
        }
        //씬이동이나 기다 재시작으로 인해 bind재호출시 기존 이벤트는 구독해제
        if (playerEvents != null) UnsubscribePlayerEvents();
        playerEvents = UIManager.Instance.playerEvents;
        Debug.Log("hud등록성공");
        //새롭게 이벤트 구독
        if (playerEvents != null) SubscribePlayerEvents();
    }
    private void OnChangeObjective(string text)
    {
        HUDController.ChangeObjective(text);
     }
    private void OnFound(InteractContext context)
    {
        currentContext = context;
        interactionUIController.ShowPrompt(context);
    }

    private void OnLost()
    {
        currentContext = default;
        interactionUIController.SetDefault();
    }

    private void OnInteractResult(InteractResult result)
    {
        if (currentContext.displayName == null) return;

        interactionUIController.ShowInteractResult(result, currentContext);
    }

    void SubscribePlayerEvents()
    {
        if (playerEvents == null) return;

        playerEvents.GetInteractContext += OnFound;
        playerEvents.RaycastOut += OnLost;
        playerEvents.GetInteractResult += OnInteractResult;
        playerEvents.ChangeObjective += OnChangeObjective;
    }
    void UnsubscribePlayerEvents()
    {
        if (playerEvents == null) return;

        playerEvents.GetInteractContext -= OnFound;
        playerEvents.RaycastOut -= OnLost;
        playerEvents.GetInteractResult -= OnInteractResult;
        playerEvents.ChangeObjective -= OnChangeObjective;

    }
}