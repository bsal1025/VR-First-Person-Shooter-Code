using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float damagerate = -1.0f;                    // Set bullet damage rate to 1 HP
    public Renderer bulletrend;                  // Create variable for bullet renderer
    private void Start()
    {
        bulletrend = GetComponent<Renderer>();   // Grab bullet renderer
        bulletrend.enabled = false;              // Disable bullet on startup
    }

    private void OnTriggerEnter(Collider other)  // Collision between a bullet and an enemy
    {
        Enemy1 temp = other.gameObject.GetComponent<Enemy1>();  // Collision with enemy1 set to temp
        Enemy2 temp2 = other.gameObject.GetComponent<Enemy2>(); // Collision with enemy2 set to temp2
        Enemy3 temp3 = other.gameObject.GetComponent<Enemy3>(); // Collision with enemy3 set to temp3

        if (temp != null)                                       // If the bullet collides with enemy1
            temp.SetHealth(temp.GetHealth() + damagerate);      // Remove 1 HP from enemy1   

        if (temp2 != null)                                       // If the bullet collides with enemy2
            temp2.SetHealth(temp2.GetHealth() + damagerate);     // Remove 1 HP from enemy2

        if (temp3 != null)                                       // If the bullet collides with enemy3
            temp3.SetHealth(temp3.GetHealth() + damagerate);     // Remove 1 HP from enemy3
    }
}
