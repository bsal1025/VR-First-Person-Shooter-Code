using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Enemy2 : MonoBehaviour {
    [SerializeField]
    public float health2 = 2.0f;                        // Set enemy health at 2
    [SerializeField] Transform destination;             // Create variable for AI target
    NavMeshAgent agent = null;                          // Assign navmesh agent to null
    Text text;                                          // Create a text variable 
    Animator myAnimator;                                // Create animator variable

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();                 // Get navmesh agent for enemy
        text = GameObject.Find("text").GetComponent<Text>();  // Get Game Over text
        myAnimator = GetComponent<Animator>();                // Get animator
        myAnimator.SetFloat("VSpeed", 1.0f);                  // Make enemy walk at animation speed of 1
      
    }

    public float GetHealth()
    {
        return health2;
    }


    public void SetHealth(float value2)
    {
        health2 = value2;
        if (health2 == 0)                                     // Once health reaches zero                                       
        {
                Score_Manager.score += 2;                     // Add 2 to current score
                scoremanager2.score2 += 2;                    // Also adds 2 to current score, score not initialized to zero
                myAnimator.SetLayerWeight(1, 1f);             // Set animator layer weight to 1
                agent.speed = 0.0f;                           // Make enemy cease moving forward
                myAnimator.SetInteger("CurrentAction", 6);    // Run dying animation
                StartCoroutine(removecorpse());               // Start a coroutine to remove the body

        }
    }

    IEnumerator removecorpse()
    {
        yield return new WaitForSeconds(2.5f);                 // Wait 2.5 seconds
        Destroy(this.gameObject);                              // destroy gameobject   
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        // Collision with FPS controller
        RigidbodyFirstPersonController temp2 = collider.gameObject.GetComponent<RigidbodyFirstPersonController>(); 
        if (temp2 != null)               // If the enemy collides with the FPS controller
        {
            StartCoroutine(exitgame());  // Start a coroutine that ends the game
            text.enabled = true;         // Display Game Over on the screen 
        }
    }
    
    IEnumerator exitgame()
    {
        yield return new WaitForSeconds(2f);   // Wait 2 seconds
        EditorApplication.isPlaying = false;   // Exit out of game view (end the game)
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = Camera.main.gameObject.transform.position; // Assign AI target to main camera position (should be same as FPS controller

        if (text.enabled)                  // If Game Over text is displayed onscreen but no collision is occurring
        {
            StartCoroutine(exitgame());    // Start a coroutine to end the game regardless
        }
    }
}

