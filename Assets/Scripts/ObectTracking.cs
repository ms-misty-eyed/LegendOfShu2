using UnityEngine;

public class ObjectTracking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CharacterController controller = other.GetComponentInParent<CharacterController>();
        if (controller == null) return;

        GameManager.Instance.RegisterCollected(gameObject.name);
        gameObject.SetActive(false);
        Debug.Log("Collected: " + gameObject.name);
    }
}