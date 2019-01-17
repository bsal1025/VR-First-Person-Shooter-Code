using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;


public class Enemy3 : MonoBehaviour {
    [SerializeField]
    public float health3 = 3.0f;                        // Set enemy health at 3
    [SerializeField] Transform destination;             // Create variable for AI target
    NavMeshAgent agent = null;                          // Assign navmesh agent to null
    Text text;                                          // Create text variable
    Animator myAnimator;                                // Create animator variable
 
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();                  // Get navmesh agent for enemy
        text = GameObject.Find("text").GetComponent<Text>();   // Get game over text
        myAnimator = GetComponent<Animator>();                 // Get animator
        myAnimator.SetFloat("VSpeed", 1.0f);                   // Make enemy walk at animation speed of 1
       

    }

    public float GetHealth()
    {
        return health3;
    }


    public void SetHealth(float value3)
    {
        health3 = value3;
        if (health3 == 0)                                               // Once health reaches zero                                       
        {
                Score_Manager.score += 3;                               // Add 3 to current score
                scoremanager2.score2 += 3;                              // Also adds 3 to current score, score not initialized to zero
                myAnimator.SetLayerWeight(1, 1f);                       // Set animator layer weight to 1
                agent.speed = 0.0f;                                     // Make enemy cease moving forward
                myAnimator.SetInteger("CurrentAction", 6);              // Run dying animation
                StartCoroutine(removecorpse());                         // Start a coroutine to remove the body
        }
    }

    IEnumerator removecorpse()
    {
        yield return new WaitForSeconds(2.5f);        // Wait 2.5 seconds
        Destroy(this.gameObject);                     // destroy gameobject   
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        // Collision with FPS controller
        RigidbodyFirstPersonController temp2 = collider.gameObject.GetComponent<RigidbodyFirstPersonController>(); 
        if (temp2 != null)                     // If enemy collides with FPS controller
        {
            StartCoroutine(exitgame());       // Start a coroutine to end the game
            text.enabled = true;              // Display Game Over on the screen 
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
        agent.destination = Camera.main.gameObject.transform.position; // Assign AI target to main camera position (should be same as FPS controller)

        if (text.enabled)                     // If Game Over text is displayed onscreen but no collision is occurring
        {
            StartCoroutine(exitgame());       // Start a coroutine to end the game regardless
        }
    }
}

