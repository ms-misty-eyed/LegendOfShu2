using UnityEngine;

public class FogComing : MonoBehaviour
{
    public GameObject player;
    public Transform lookAtExit;
    public GameObject moveTowards;

    public float growSpeed = 5f;
    private Vector3 startPosition;
    public float stopPosition = 1.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       float targetScaleZ = Vector3.Distance(startPosition, moveTowards.transform.position);

        float newScaleZ = Mathf.MoveTowards(transform.localScale.z, targetScaleZ, growSpeed * Time.deltaTime);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newScaleZ);

        transform.position = startPosition + transform.forward * (newScaleZ / stopPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Vector3 currentRotation = other.transform.eulerAngles;
            //other.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + 180f, currentRotation.z);

            other.transform.LookAt(lookAtExit);
        }
    }
}
