using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // checks if the player collided with door
        if(other.gameObject.CompareTag("Door"))
        {
            // sends a message for debug
            Debug.Log("Hit Door");
        }
    }
}
