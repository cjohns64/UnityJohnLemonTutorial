using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    private float vision_range = 1.5f;
    private float vision_angle = 1.0f;

    /*
     * check if player is within targeting range
     * check if player is within targeting angle
     * check if player is visible (raycast)
     */

    void Update ()
    {
        // check if player is within vision range
        Vector3 player_reletive_position = player.position - transform.position;

        // don't check y since this game does not have any elevation
        // this isn't a circle, it is a square
        if (player_reletive_position.x < vision_range && player_reletive_position.z < vision_range) {
            // check if player is within vision angle
            // dot product of transform.forward and vector pointing at player
            if (Vector3.Dot(player_reletive_position, transform.forward) > vision_angle) {
                // check if player is unobstructed, same as in original Observer.cs script
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                
                if (Physics.Raycast (ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == player)
                    {
                        gameEnding.CaughtPlayer ();
                    }
                }

            }

        }
    }
}
