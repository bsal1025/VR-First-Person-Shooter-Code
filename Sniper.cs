using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Renderer sniper;                    // Create variable for sniper rifle renderer

    // Use this for initialization
    void Start()
    {
        sniper = GetComponent<Renderer>();    // Grab sniper rifle
        sniper.enabled = true;                // Enable sniper rifle on startup, sniper is default weapon
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))              // If player presses 1
        {
            sniper.enabled = true;              // Enable sniper rifle
        }
        else if (Input.GetKeyDown("2"))         // If player presses 2
        {
            sniper.enabled = false;             // Disable sniper rifle
        }
        else if (Input.GetKeyDown("3"))         // If player presses 3
        {
            sniper.enabled = false;             // Disable sniper rifle
        }

    }
}
