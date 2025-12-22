using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using UnityEngine;

public class InteractionUIEventReceiver : MonoBehaviour
{
    public PlayerEvents playerEvents;
    public InteractionUIController ui;

    private InteractContext currentContext;

    private void OnEnable()
    {
        playerEvents.GetInteractContext += OnFound;
        playerEvents.RaycastOut += OnLost;
        playerEvents.GetInteractResult += OnInteractResult;
    }

    private void OnDisable()
    {
        if (playerEvents == null) return;

        playerEvents.GetInteractContext -= OnFound;
        playerEvents.RaycastOut -= OnLost;
        playerEvents.GetInteractResult -= OnInteractResult;
    }

    private void OnFound(InteractContext context)
    {
        currentContext = context;
        ui.ShowPrompt(context);
    }

    private void OnLost()
    {
        currentContext = default;
        ui.ShowNormal();
    }

    private void OnInteractResult(InteractResult result)
    {
        if (currentContext.displayName == null) return;

        ui.ShowInteractResult(result, currentContext);
    }
}