using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StarterAssets
{
    

public class SeasonCheck : MonoBehaviour
{
    public int season = 1;
    public Collider HallWayDoorCollider;
    public Vector3 currentPosition;
    public Vector3 basePosition;

    private bool _waitingToReset = false;

    void Start()
    {
        HallWayDoorCollider = GetComponent<Collider>();
        basePosition = transform.position;
    }

    void Update()
    {
        if (_waitingToReset)
        {
            transform.position = basePosition;
            HallWayDoorCollider.enabled = true;
            _waitingToReset = false;
        }
    }
    
    public void ResetDoor()
    {
        _waitingToReset = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Debug.Log("Player detected");

        season += 1;
        Debug.Log("season is: " + season);
        
        currentPosition = transform.position;
        currentPosition.y = -50f;
        transform.position = currentPosition;

        HallWayDoorCollider.enabled = false;
    }
}
}
