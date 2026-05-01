using UnityEngine;
using StarterAssets;
// Add this for the Input System
using UnityEngine.InputSystem; 

public class PickUpMem : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject turnText;
    public Transform tpTo;
    public bool isPickedUp;
    public Rigidbody rb;

    [Header("Inspect Settings")]
    public float rotateSpeed = 0.5f; // Lowered for Input System delta values
    public float distanceInFrontOfCamera = 1.0f;

    [Header("Camera")]
    public GameObject cinemachineCameraTarget;

    private bool _isInspecting;
    private bool _justStartedInspecting;
    private bool _centeringCamera;
    private Camera _cam;
    private Quaternion _originalRotation;
    private FirstPersonController _playerController;

    void Start()
    {
        pickUpText.SetActive(false);
        turnText.SetActive(false);
        _cam = Camera.main;
        _originalRotation = transform.rotation;

        _playerController = FindObjectOfType<FirstPersonController>();
        if (_playerController == null)
            Debug.LogError("FirstPersonController not found!");
    }

    void Update()
    {
        if (!_isInspecting) return;

        // Keep object in front of camera
        transform.position = _cam.transform.position + (_cam.transform.forward * distanceInFrontOfCamera);

        if (_centeringCamera)
        {
            Quaternion current = cinemachineCameraTarget.transform.localRotation;
            Quaternion target = Quaternion.Euler(0f, 0f, 0f);
            cinemachineCameraTarget.transform.localRotation = Quaternion.Slerp(current, target, Time.deltaTime * 10f);

            if (Quaternion.Angle(current, target) < 0.1f)
                _centeringCamera = false;
            
            // Allow rotation even while centering for a smoother feel
        }

        // --- NEW INPUT SYSTEM MOUSE DETECTION ---
        // Mouse.current.delta reads the raw movement even if the controller is off
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        float rotateX = mouseDelta.x * rotateSpeed;
        float rotateY = mouseDelta.y * rotateSpeed;

        if (mouseDelta.sqrMagnitude > 0.01f)
        {
            transform.Rotate(_cam.transform.up, -rotateX, Space.World);
            transform.Rotate(_cam.transform.right, rotateY, Space.World);
        }

        if (_justStartedInspecting)
        {
            _justStartedInspecting = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)){
            turnText.SetActive(false);
            SendToTree();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (_isInspecting) return;

        pickUpText.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpText.SetActive(false);
            turnText.SetActive(true);
            _isInspecting = true;
            _justStartedInspecting = true;
            _centeringCamera = true;

            if (rb != null) rb.isKinematic = true;

            foreach (Collider col in GetComponents<Collider>())
                col.enabled = false;

            if (_playerController != null)
                _playerController.enabled = false;

            // Important: Keep the cursor locked to capture Delta movement
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        pickUpText.SetActive(false);
    }

    public void SendToTree()
    {
        _isInspecting = false;
        _centeringCamera = false;
        isPickedUp = true;
        
        transform.position = tpTo.position;
        transform.rotation = _originalRotation;

        if (rb != null) rb.isKinematic = false;

        foreach (Collider col in GetComponents<Collider>())
            col.enabled = true;

        if (_playerController != null)
            _playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
    }
}