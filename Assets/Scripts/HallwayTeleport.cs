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
    public Transform target;
    
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
                break;
            case 2: controller.transform.position = destinationS3.position;
                break;
            case 3: controller.transform.position = destinationS4.position;
                break;
            
        }

        my_season_script.ResetDoor();

        controller.enabled = true;
        
        StarterAssetsInputs movement = controller.GetComponent<StarterAssetsInputs>();
        if (movement != null) movement.ResetVelocity();
        player.LookAt(target);
        Debug.Log("Teleported!");
    }
}