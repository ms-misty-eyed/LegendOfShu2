using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfMemPickUp : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject BlockOnPlayer;
    public Transform tpTo;
    public bool isPickedUp = false;

    public GameObject NeedToPick;
    PickUpMem PickUp_script;

    void Start()
    {
        pickUpText.SetActive(false);
        PickUp_script = NeedToPick.GetComponent<PickUpMem>();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if(PickUp_script.isPickedUp)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            //Debug.Log("Cube grabbed");
        }
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