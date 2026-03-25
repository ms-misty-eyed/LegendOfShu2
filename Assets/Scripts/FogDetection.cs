using UnityEngine;

public class FogDetection : MonoBehaviour
{
    public Transform lookAtExit;
    
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
