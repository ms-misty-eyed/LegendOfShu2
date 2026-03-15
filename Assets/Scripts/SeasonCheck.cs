using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeasonCheck : MonoBehaviour
{
    public int season = 1;
    public Collider HallWayDoorCollider;
    public Vector3 currentPosition;
    public Vector3 basePosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HallWayDoorCollider = GetComponent<Collider>();
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(HallWayDoorCollider.isTrigger == true)
        {
            transform.position = basePosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // checks if the player collided with door
        if(other.gameObject.CompareTag("Player"))
        {
            // sends a message for debug
            Debug.Log("Player detected");

            // changes the season
            season += 1;
            Debug.Log("season is: " + season);

            // set the collider to false to not trigger the season adding again
            HallWayDoorCollider.isTrigger = false;
            Debug.Log("isTrigger set to: " + HallWayDoorCollider.isTrigger);

            // make the door disappear after you tp
            currentPosition = transform.position;
            currentPosition.y = -50f;
            transform.position = currentPosition;

        }
    }
}
