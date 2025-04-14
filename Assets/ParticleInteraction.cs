using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInteraction : MonoBehaviour
{
    public GameEnding gameEnding;
    // partical system colliders
    public ParticleSystem ps;
    private static float hit_scale = 25.0f;
    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }
    
    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        // each partical hit adds to the detection bar
        gameEnding.DetectingPlayer((float) numEnter * hit_scale);
    }
}
