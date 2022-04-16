using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    //private GameMaster gm;
    void Start()
    {
        //gm = FindObjectOfType<GameMaster>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gm.lastCheckPointPos = transform.position;
        }
    }
}
