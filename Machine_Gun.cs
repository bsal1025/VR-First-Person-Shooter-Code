using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Gun : MonoBehaviour {
    public Renderer machine_gun;                  // Create variable for machine gun renderer

	// Use this for initialization
	void Start () {
        machine_gun = GetComponent<Renderer>();   // Grab machine gun
        machine_gun.enabled = false;              // Disable machine gun on startup
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("3"))           // If player presses 3
        {
            machine_gun.enabled = true;     // Enable machine gun
        }
        else if(Input.GetKeyDown("1"))      // If player presses 1
        {
            machine_gun.enabled = false;    // Disable machine gun
        }
        else if(Input.GetKeyDown("2"))      // If player presses 2
        {
            machine_gun.enabled = false;    // Disable machine gun
        }  
	}
}
