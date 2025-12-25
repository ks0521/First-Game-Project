using _Kamikakushi.Contents.Player;
using _Kamikakushi.Utills.Structs;
using UnityEngine;

public class InteractionUIEventReceiver : MonoBehaviour
{
    public PlayerEvents playerEvents;
    public CrosshairController interactionUIController;
    public HUDController HUDController;
    private InteractContext currentContext;

    private void OnEnable()
    {
        playerEvents.GetInteractContext += OnFound;
        playerEvents.RaycastOut += OnLost;
        playerEvents.GetInteractResult += OnInteractResult;
        playerEvents.ChangeObjective += OnChangeObjective;
    }

    private void OnDisable()
    {
        if (playerEvents == null) return;

        playerEvents.GetInteractContext -= OnFound;
        playerEvents.RaycastOut -= OnLost;
        playerEvents.GetInteractResult -= OnInteractResult;
        playerEvents.ChangeObjective -= OnChangeObjective;

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
}