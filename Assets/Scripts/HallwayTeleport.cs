using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;

public class HallwayTeleport : MonoBehaviour
{
    public GameObject my_season;
    SeasonCheck my_season_script;

    public Transform destinationS2;
    public Transform destinationS3;
    public Transform destinationS4;

    public Transform player;
    
    void Start()
    {
        my_season_script = my_season.GetComponent<SeasonCheck>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.gameObject.name);

        if (!other.CompareTag("Player")) return;

        CharacterController controller = other.GetComponentInParent<CharacterController>();
        if (controller == null) return;

        controller.enabled = false;

        // tp based on season
        switch (my_season_script.season)
        {
            case 1: controller.transform.position = destinationS2.position;
                player.SetPositionAndRotation(destinationS2.position, destinationS2.rotation);
                break;
            case 2: controller.transform.position = destinationS3.position;
                player.SetPositionAndRotation(destinationS3.position, destinationS3.rotation);
                break;
            case 3: controller.transform.position = destinationS4.position;
                player.SetPositionAndRotation(destinationS4.position,destinationS4.rotation);
                break;
            
        }
        //Thicken the fog
        RenderSettings.fogDensity += 0.01f;
        Debug.Log("Fog increased to " + RenderSettings.fogDensity);
        
        my_season_script.ResetDoor();
        controller.enabled = true;
        
        StarterAssetsInputs movement = controller.GetComponent<StarterAssetsInputs>();
        if (movement != null) movement.ResetVelocity();
        //Debug.Log("Teleported!");
    }
}