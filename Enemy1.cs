using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class Enemy1 : MonoBehaviour {
    
    [SerializeField]
    public float health1 = 1.0f;                        // Set enemy health at 1
    [SerializeField] Transform destination;             // Create variable for AI target
    NavMeshAgent agent = null;                          // Assign navmesh agent to null
    Text text;                                          // Create text variable
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
        return health1;
    }
   
    
    private void OnTriggerEnter(Collider collider)
    {
        RigidbodyFirstPersonController temp = collider.gameObject.GetComponent<RigidbodyFirstPersonController>(); // Collision with FPS controller
        if(temp != null)                        // If enemy collides with FPS controller
        {

            text.enabled = true;                // Display Game Over on screen 
            StartCoroutine(exitgame());         // Start Coroutine to end the game            
        }
           
       
    }
    
  IEnumerator exitgame()
        {
            yield return new WaitForSeconds(2f);  // Wait 2 seconds
            EditorApplication.isPlaying = false;  // Exit out of game view (end the game)
    }

    public void SetHealth(float value1)
    {
        health1 = value1;
        if (health1 == 0)                                               // Once health reaches zero                                       
        {
                Score_Manager.score += 1;                               // Add 1 to current score
                scoremanager2.score2 += 1;                              // Also adds 1 to current score, score not initialized to zero
                agent.speed = 0.0f;                                     // Make enemy cease moving forward
                myAnimator.SetLayerWeight(1, 1f);                       // Set animator layer weight to 1
                myAnimator.SetInteger("CurrentAction", 6);              // Run dying animation
                StartCoroutine(removecorpse());                         // Start coroutine to remove the body
                                                      

        }
    }
    IEnumerator removecorpse()
    {
        yield return new WaitForSeconds(2.5f);                          // Wait 2.5 seconds
        Destroy(this.gameObject);                                       // destroy gameobject   
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = Camera.main.gameObject.transform.position; // Assign AI target to main camera position (should be the same as FPS controller)

        if (text.enabled)                    // If Game Over text is displayed onscreen but no collision is occurring
        {
            StartCoroutine(exitgame());      // Start a coroutine to end the game regardless
        }
    }
}
