using UnityEngine;

public class FogComing : MonoBehaviour
{
    public GameObject player;
    public Transform lookAtExit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
