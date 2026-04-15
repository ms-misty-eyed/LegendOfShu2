using UnityEngine;

public class PickUpMem : MonoBehaviour
{
    public GameObject pickUpText;
    public Transform tpTo;
    public Transform holdPoint;
    public bool isPickedUp;

    [Header("Inspect Settings")]
    public float rotateSpeed = 5f;

    private bool isInspecting = false;
    private bool justStartedInspecting = false;
    private bool playerNearby = false;
    private Camera cam;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        pickUpText.SetActive(false);
        cam = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (!isInspecting) return;

        transform.position = Vector3.Lerp(transform.position, holdPoint.position, Time.deltaTime * 10f);

        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;
        transform.Rotate(cam.transform.up, -mouseX, Space.World);
        transform.Rotate(cam.transform.right, mouseY, Space.World);

        if (justStartedInspecting)
        {
            justStartedInspecting = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
            SendToTree();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (isInspecting) return;

        playerNearby = true;
        pickUpText.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpText.SetActive(false);
            isInspecting = true;
            justStartedInspecting = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        playerNearby = false;
        pickUpText.SetActive(false);
    }

    void SendToTree()
    {
        isInspecting = false;
        isPickedUp = true;
        transform.position = tpTo.position;
        transform.rotation = originalRotation;
        GetComponent<Collider>().enabled = true;
    }
}