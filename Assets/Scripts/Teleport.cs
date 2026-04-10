using StarterAssets;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public Transform player;
    public Transform target;

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
        player.LookAt(target);

        Debug.Log("Teleported!");
    }
}