using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMem : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject BlockOnPlayer;
    public Transform tpTo;
    public bool isPickedUp = false;

    void Start()
    {
        pickUpText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpText.SetActive(true);

            // Check if 'E' key is held down to pick up the Block
            if (Input.GetKey(KeyCode.E))
            {
                //gameObject.SetActive(false);
                transform.position = tpTo.position;
                //BlockOnPlayer.SetActive(true);
                pickUpText.SetActive(false);

                isPickedUp = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpText.SetActive(false);
        }
    }
}