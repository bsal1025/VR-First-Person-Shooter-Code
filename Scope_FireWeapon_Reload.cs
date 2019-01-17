using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Scope_FireWeapon_Reload : MonoBehaviour {
    public Animator animator;              // Create animator variable
    private bool isScoped = false;         // Initialize isScoped to false
    public GameObject scopeOverlay;        // Create gameobject for scope overlay
     public GameObject WeaponCamera;       // Create gameobject for the weapon camera
     public Renderer sniperrend;           // Create variable for sniper renderer
     public Renderer shotgunrend;          // Create variable for shotgun renderer
     public Renderer machinegunrend;       // Create variable for machine gun renderer
     public Camera mainCamera;             // Create variable for the main camera
     public GameObject bulletPrefab;       // Create gameobject for the bullet prefab
     public Transform bulletSpawn;         // Create transform for the bullet's spawn point
     
    public float scopedFOV = 15f;         // Set the scoped field of view to 15
    private float prevFOV;                // Create a variable to store the previous field of view
    AudioSource audio1;                   // Create audiosource for source 1, sniper rifle shot
    AudioSource audio2;                   // Create audiosource for source 2, shotgun shot
    AudioSource audio3;                   // Create audiosource for source 3, machine gun shot
    AudioSource audio4;                   // Create audiosource for source 4, reload sound
    Text Ammo1;                           // Create text for sniper rifle ammo display
    Text Ammo2;                           // Create text for shotgun ammo display
    Text Ammo3;                           // Create text for machine gun ammo display
    Text reload;                          // Create text for reload display
    public static int sniperammo = 10;      // Initialize sniper ammo to 10
    public static int shotgunammo = 10;     // Initialize shotgun ammo to 10
    public static int machinegunammo = 50;  // Initialize machine gun ammo to 50

    void Start()
    {
        var aSources = GetComponents<AudioSource>();                // Create an array of all audio sources
        audio1 = aSources[0];                                       // First element is sniper rifle sound
        audio2 = aSources[1];                                       // Second element is shotgun sound
        audio3 = aSources[2];                                       // Third element is machine gun sound
        audio4 = aSources[3];                                       // Fourth element is reload sound
        Ammo1 = GameObject.Find("Ammo1").GetComponent<Text>();      // Grab Ammo1 text
        Ammo2 = GameObject.Find("Ammo2").GetComponent<Text>();      // Grab Ammo2 text
        Ammo3 = GameObject.Find("Ammo3").GetComponent<Text>();      // Grab Ammo3 text
        reload = GameObject.Find("reload").GetComponent<Text>();    // Grab reload text

    }
    // Update is called once per frame
    void Update () {
               
         Ammo1.text = "Sniper Ammo: " + sniperammo;             // Display amount of ammo sniper rifle has
         Ammo2.text = "Shotgun Ammo: " + shotgunammo;           // Display amount of ammo shotgun has
         Ammo3.text = "Machine Gun Ammo:" + machinegunammo;     // Display amount of ammo machine gun has

        if (sniperammo == 0)           // If sniper runs out of bullets
        {
            reload.enabled = true;     // Display reload text onscreen
        }
        else if(shotgunammo == 0)      // If shotgun runs out of bullets
        {
            reload.enabled = true;     // Display reload text onscreen
        }
        else if(machinegunammo == 0)   // If machinegun runs out of bullets
        {
            reload.enabled = true;     // Display reload text onscreen
        }
        else
        {
            reload.enabled = false;    // Otherwise hide reload text 
           
        }

        if (Input.GetKeyDown("r") && sniperammo == 0)             // If sniper is out of bullets and player hits r
        {
            sniperammo = 10;    // Reset sniper ammo back to 10
            audio4.Play();      // Play reload sound
                      
        }
        else if(Input.GetKeyDown("r") && shotgunammo == 0)       // If shotgun is out of bullets and player hits r
        {
            shotgunammo = 10;  // Reset shotgun ammo back to 10
            audio4.Play();     // Play reload sound
        }
        else if(Input.GetKeyDown("r") && machinegunammo == 0)    // If machine gun is out of bullets and player hits r
        {
           machinegunammo = 50;   // Reset machine gun ammo back to 50
            audio4.Play();        // Play reload sound
        }
        if (Input.GetMouseButtonDown(1))              // If player hits right mouse button
        {
            isScoped = !isScoped;                     // Invert the value of isScoped (should now be true)
            animator.SetBool("Is_Scoped", isScoped);  // Tell the animator to run the isScoped animation (moves gun to center of screen)
            
            if (isScoped && sniperrend.enabled)       // If we are zoomed in and the sniper is our weapon
            {
                StartCoroutine(OnScoped());           // Start coroutine to bring up the sniper scope
                
            }
            else if (isScoped && (shotgunrend.enabled || machinegunrend.enabled))  // If we are zoomed in and the sniper is not our weapon
            {
                prevFOV = mainCamera.fieldOfView;      // Set the variable prevFOV to the main camera's field of view
            }  
           else
            {
                UnScoped();   // Otherwise run unscoped function
            }
        }
          
        if (Input.GetMouseButtonDown(0) && sniperrend.enabled)   // If the left mouse button is pressed and sniper is the weapon
        {
            if (sniperammo > 0)   // If sniper has bullets in it
            {
                audio1.Play();    // Play sniper shot sound
                Fire();           // Run function to fire the weapon
                sniperammo -= 1;  // Reduce the sniper ammo variable by 1
            }
        }
        else if(Input.GetMouseButtonDown(0) && shotgunrend.enabled) // If the left mouse button is pressed and shotgun is the weapon
        {
            if (shotgunammo > 0) // If the shotgun has bullets in it 
            {
                audio2.Play();   // Play shotgun sound
                Fire();          // Run function to fire the weapon
                shotgunammo -= 1; // Reduce the shotgun ammo variable by 1
            }
        }

        if(Input.GetMouseButton(0)&& machinegunrend.enabled) // If the left mouse button is pressed and machine gun is the weapon
        {
            if (machinegunammo > 0) // If the machine gun has bullets in it 
            {
                Fire();               // Continuously fire bullets every frame until player releases mouse button or ammo hits zero
                machinegunammo -= 1;  // Reduce machine gun ammo variable by 1 for every bullet instantiated
            }
        }
        if (Input.GetMouseButtonDown(0) && machinegunrend.enabled && machinegunammo > 0)  // If left mouse button is pressed, machine gun is weapon, and machine gun has bullets
        {
            audio3.Play();  // Play machine gun sound once (set to loop in inspector)
        }
        else if(Input.GetMouseButtonUp(0) && machinegunrend.enabled && machinegunammo > 0) // If the left mouse button is released, machine gun is weapon, and machine gun has bullets
        {
            audio3.Stop();  // Stop playing the machine gun sound
        }
        
    }

    IEnumerator OnScoped ()
    {
        yield return new WaitForSeconds(0.15f);      // Wait 0.15 seconds
        scopeOverlay.SetActive(true);                // Display the scope overlay (sniper scope)
        WeaponCamera.SetActive(false);               // Turn off the weapon camera (sniper rifle becomes invisible)
        animator.SetBool("Is_Scoped", !isScoped);    // Tell the animator not to run the zoom in animation

        prevFOV = mainCamera.fieldOfView;            // Set the prevFOV variable to the main camera's point of view
        mainCamera.fieldOfView = scopedFOV;          // Set the main camera's field of view to the scoped field of view
    }

   void UnScoped()
    {
        scopeOverlay.SetActive(false);        // Disable sniper scope overlay
        WeaponCamera.SetActive(true);         // Turn on the weapon camera (make weapons visible)
        mainCamera.fieldOfView = prevFOV;     // Set the main camera's field of view to normal field of view
    }


    void Fire()
    {
        // Create the Bullet from the Bullet Prefab at bulletSpawn's position (set so that bullet travels where user is aiming)
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
              

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        bullet.GetComponent<Rigidbody>().AddForce(10000*(Camera.main.transform.forward));
        // Destroy the bullet after 3 seconds
        Destroy(bullet, 3.0f);
    }


}



