using StarterAssets;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.gameObject.name);

        if (!other.CompareTag("Player")) return;

        CharacterController controller = other.GetComponentInParent<CharacterController>();
        if (controller == null) return;

        controller.enabled = false;
        controller.transform.position = destination.position;
        controller.enabled = true;
        
        StarterAssetsInputs movement = controller.GetComponent<StarterAssetsInputs>();
        if (movement != null) movement.ResetVelocity();

        Debug.Log("Teleported!");
    }
}