using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager5 : MonoBehaviour {
    [SerializeField] GameObject prefabEnemy1;         // Grab Enemy 1 Prefab
    [SerializeField] GameObject prefabEnemy2;         // Grab Enemy 2 Prefab
    [SerializeField] GameObject prefabEnemy3;         // Grab Enemy 3 Prefab
    public GameObject newEnemy1;                      // Create variable for new instances of Enemy1
    public GameObject newEnemy2;                      // Create variable for new instances of Enemy2
    public GameObject newEnemy3;                      // Create variable for new instances of Enemy3
    int j;                                            // Loop variable used later in the script
    Text wave5end;                                    // Create text variable for end of wave text
    Text wave5start;                                  // Create text variable for beginning of wave text 
    // Use this for initialization
    void Start()
    {

        StartCoroutine(GenNewSet());                                       // Start a coroutine that spawns a new set of enemies
        wave5end = GameObject.Find("wave5end").GetComponent<Text>();       // Get Wave 5 Over text
        wave5start = GameObject.Find("wave5start").GetComponent<Text>();   // Get Begin Wave 5 text
        wave5start.enabled = true;                                         // Display Begin Wave 5 at start of level
        StartCoroutine(removetext());                                      // Start a coroutine to clear level start text 


    }
    IEnumerator removetext()
    {
        yield return new WaitForSeconds(1.5f);                             // Wait 1.5 seconds
        Destroy(wave5start);                                               // Remove Begin Wave 5 text
    }

    IEnumerator GenNewSet()
    {
        while (true)
        {

            System.Random random = new System.Random();                    // Create a new random variable called random
            for (j = 0; j < 10; j++)
            {
                // Create 10 instances of Enemy1 and place them in random locations
                newEnemy1 = Instantiate(prefabEnemy1, new Vector3(random.Next(-43, 115), 0, random.Next(-251, -40)), Quaternion.identity);
                newEnemy1.GetComponent<NavMeshAgent>().speed = 6f; // Increase Enemy1 Speed to 6
            }

            for (j = 0; j < 7; j++)
            {
                // Create 7 instances of Enemy2 and place them in random locations
                newEnemy2 = Instantiate(prefabEnemy2, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy2.GetComponent<NavMeshAgent>().speed = 4.0f; // Increase Enemy2 Speed to 4
            }

            for (j = 0; j < 7; j++)
            {
                // Create 7 instances of Enemy3 and place them in random locations
                newEnemy3 = Instantiate(prefabEnemy3, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy3.GetComponent<NavMeshAgent>().speed = 2.5f; // Set Enemy3 Speed to 2.5
            }

            if (Time.timeSinceLevelLoad >= 120f)  // If 2 minutes or more has elapsed
            {
                StopAllCoroutines();              // Stop generating enemies

            }

            yield return new WaitForSeconds(30f);  // Wait 30 seconds, then create a new set of enemies

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Score_Manager.score >= 795)    // If the player score reaches 795 or higher
        {
            wave5end.enabled = true;
        }
        else
        {
            wave5end.enabled = false;
        }
    }
}
