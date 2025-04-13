using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    private const float VISION_RANGE = 4.5f;
    private const float SQR_VISION_RANGE = VISION_RANGE * VISION_RANGE;
    private const float INV_SQR_VISION_RANGE = 1.0f / SQR_VISION_RANGE;
    private const float VISION_ANGLE = 0.1f;
    private const float MAX_MULT = 50.0f;
    private const float MIN_MULT = 0.0f;

    /*
     * check if player is within targeting range
     * check if player is within targeting angle
     * check if player is visible (raycast)
     */

    void Update ()
    {
        // check if player is within vision range
        Vector3 player_relative_position = player.position - transform.position;

        // check player position square magnitude vs square vision range
        if (player_relative_position.sqrMagnitude < SQR_VISION_RANGE) {
            // check if player is within vision angle
            // dot product of transform.forward and vector pointing at player
            if (Vector3.Dot(player_relative_position, transform.forward) > VISION_ANGLE) {
                // check if player is unobstructed, same as in original Observer.cs script
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                
                if (Physics.Raycast (ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == player)
                    {
                        // detection multiplier is a linear interpolation between min value at player_relative_position >= VISION_RANGE
                        // and max value at player_relative_position == 0.0f
                        // y = (MIN_MULT * (x - 0.0f) - MAX_MULT * (x - SQR_VISION_RANGE)) * 1/(SQR_VISION_RANGE)
                        // with MIN_MULT = 0.0f
                        // y = -1 * MAX_MULT * (player_relative_position.sqrMagnitude - SQR_VISION_RANGE) * INV_SQR_VISION_RANGE
                        float multiplier = INV_SQR_VISION_RANGE * MAX_MULT * (SQR_VISION_RANGE - player_relative_position.sqrMagnitude);
                        gameEnding.DetectingPlayer (multiplier);
                    }
                }

            }

        }
    }
}
