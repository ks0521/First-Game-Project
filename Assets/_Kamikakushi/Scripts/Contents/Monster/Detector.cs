using _Kamikakushi.Contents.Monster;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Monster owner;
    private bool isEnabled = true;

    public void Init(Monster owner)
    {
        this.owner = owner;
    }

    public void SetEnable(bool value)
    {
        isEnabled = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnabled) return;
        if (!other.CompareTag("Player")) return;

        owner.OnPlayerDetected(other.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isEnabled) return;
        if (!other.CompareTag("Player")) return;

        owner.OnPlayerDetected(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isEnabled) return;
        if (!other.CompareTag("Player")) return;

        owner.OnPlayerLost();
    }
}
