using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem; 

public class PickUpMem : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject turnText;
    public Transform tpTo;
    public bool isPickedUp;
    public Rigidbody rb;

    [Header("Inspect Settings")]
    public float rotateSpeed = 0.5f;
    public float distanceInFrontOfCamera = 1.0f;
    public float heightOffset = 0f;

    [Header("Camera")]
    public GameObject cinemachineCameraTarget;

    private bool _isInspecting;
    private bool _playerInRange = false;
    private float _inspectCooldown = 0f;
    private float _inspectCooldownDuration = 0.3f;
    private bool _centeringCamera;
    private Vector3 _inspectWorldPosition;
    private Camera _cam;
    private Quaternion _originalRotation;
    private FirstPersonController _playerController;

    private FirstPersonController _fpc;

    void Start()
    {
        pickUpText.SetActive(false);
        turnText.SetActive(false);
        _cam = Camera.main;
        _originalRotation = transform.rotation;

        _playerController = FindObjectOfType<FirstPersonController>();
        if (_playerController == null)
            Debug.LogError("FirstPersonController not found!");

        _fpc = FindObjectOfType<FirstPersonController>();
    }

    void Update()
    {
        // Handle pickup input in Update instead of OnTriggerStay
        if (_playerInRange && !_isInspecting && Input.GetKeyDown(KeyCode.E))
{
    pickUpText.SetActive(false);
    turnText.SetActive(true);
    _isInspecting = true;
    _inspectCooldown = _inspectCooldownDuration;

    _inspectWorldPosition = _cam.transform.position + 
                            (_cam.transform.forward * distanceInFrontOfCamera) + 
                            (Vector3.up * heightOffset);

    // Calculate and apply pitch BEFORE disabling the controller
    Vector3 directionToObject = _inspectWorldPosition - _cam.transform.position;
    Quaternion targetRotation = Quaternion.LookRotation(directionToObject);
    float targetPitch = targetRotation.eulerAngles.x;
    if (targetPitch > 180f) targetPitch -= 360f;
    _fpc.SetCameraPitch(targetPitch);

    if (rb != null) rb.isKinematic = true;

    foreach (Collider col in GetComponents<Collider>())
        col.enabled = false;

    if (_playerController != null)
        _playerController.enabled = false;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

        if (!_isInspecting) return;

        // Keep object at its locked world position
        transform.position = _inspectWorldPosition;

        if (_centeringCamera)
{
    Vector3 directionToObject = transform.position - _cam.transform.position;
    Quaternion targetRotation = Quaternion.LookRotation(directionToObject);

    float targetPitch = targetRotation.eulerAngles.x;
    if (targetPitch > 180f) targetPitch -= 360f;

    // Snap directly instead of lerping
    _fpc._cinemachineTargetPitch = targetPitch;
    cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(targetPitch, 0f, 0f);

    _centeringCamera = false;
}

        // Rotate object with mouse
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float rotateX = mouseDelta.x * rotateSpeed;
        float rotateY = mouseDelta.y * rotateSpeed;

        if (mouseDelta.sqrMagnitude > 0.01f)
        {
            transform.Rotate(_cam.transform.up, -rotateX, Space.World);
            transform.Rotate(_cam.transform.right, rotateY, Space.World);
        }

        // Cooldown before allowing E to exit inspect
        _inspectCooldown -= Time.deltaTime;
        if (_inspectCooldown > 0f) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            turnText.SetActive(false);
            SendToTree();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _playerInRange = true;
        pickUpText.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _playerInRange = true;
        pickUpText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _playerInRange = false;
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
        Cursor.visible = false;
    }
}