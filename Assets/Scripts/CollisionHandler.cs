using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionHandler : MonoBehaviour
{
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
        // checks if the player collided with door
        if(other.gameObject.CompareTag("Door"))
        {
            // sends a message for debug
            Debug.Log("Hit Door");
        }
    }
}
