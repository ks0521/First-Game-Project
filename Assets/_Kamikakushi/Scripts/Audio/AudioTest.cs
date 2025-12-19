using UnityEngine;
using _Kamikakushi.Audio;
using _Kamikakushi.Utills.Audio;

public class AudioTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlaySFX(SFXType.PickupItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlayLoop(SFXType.Heartbeat);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.StopLoop();
        }
    }
}
