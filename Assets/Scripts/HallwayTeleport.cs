using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallwayTeleport : MonoBehaviour
{
    public GameObject my_season;
    SeasonCheck my_season_script;

    public Transform destinationS2;
    public Transform destinationS3;
    public Transform destinationS4;


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
        if(my_season_script.season==2)
        {
            controller.transform.position = destinationS2.position;
        }
        else if(my_season_script.season==3)
        {
            controller.transform.position = destinationS3.position;
        }
        else if(my_season_script.season==4)
        {
            controller.transform.position = destinationS4.position;
        }


        my_season_script.HallWayDoorCollider.isTrigger = true;

        controller.enabled = true;

        Debug.Log("Teleported!");
    }
}