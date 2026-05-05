using UnityEngine;
using StarterAssets; 
using System.Collections;
using System.Collections.Generic;

public class TreeGrow : MonoBehaviour
{
    public GameObject my_season;
    SeasonCheck my_season_script;

    public Vector3 currentPosition;
    public Vector3 basePosition;

    public GameObject babyTree;
    public GameObject kidTree;
    public GameObject teenTree;
    public GameObject uncTree;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform.position;

        currentPosition = transform.position;
        currentPosition.y = -500f;


        babyTree.transform.position = currentPosition;
        kidTree.transform.position = currentPosition;
        teenTree.transform.position = currentPosition;
        uncTree.transform.position = currentPosition;

        my_season_script = my_season.GetComponent<SeasonCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if(my_season_script.season==1)
        {
            babyTree.transform.position = basePosition;
        }
        else if(my_season_script.season==2)
        {
            kidTree.transform.position = basePosition;
            babyTree.transform.position = currentPosition;
        }
        else if(my_season_script.season==3)
        {
            teenTree.transform.position = basePosition;
            kidTree.transform.position = currentPosition;
        }
        else if(my_season_script.season==4)
        {
            uncTree.transform.position = basePosition;
            teenTree.transform.position = currentPosition;
        }
        else if(my_season_script.season ==5)
        {
            uncTree.transform.position = currentPosition;
            Debug.Log("no more seasons");
        }
    }
}
